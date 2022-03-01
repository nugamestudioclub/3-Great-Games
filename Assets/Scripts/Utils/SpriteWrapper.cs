using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "SpriteWrapper", menuName = "ScriptableObjects/SpriteWrapper")]
public class SpriteWrapper : ScriptableObject
{
    [SerializeField]
    [SerializeProperty("OriginalSprite")]
    private Sprite originalSprite;
    public Sprite OriginalSprite
    {
        get => originalSprite;
        set
        {
            originalSprite = value;
            GreySprite = TryAssignGrey(originalSprite);
        }
    }
    
    [field: ReadOnly]
    [field: SerializeField]
    public Sprite GreySprite { get; private set; }

    private static Sprite TryAssignGrey(Sprite sprite)
    {
        Sprite gSprite;
        #if UNITY_EDITOR
        gSprite = AssetDatabase.LoadAssetAtPath<Sprite>(GreyPath(AssetDatabase.GetAssetPath(sprite)));
        #endif
        return gSprite == null ? sprite : gSprite;
    }

    private static string GreyPath(string path)
    {
        int pos = path.LastIndexOf('.');

        return path.Substring(0, pos) + "_grey" + path.Substring(pos, path.Length - pos);
    }

}