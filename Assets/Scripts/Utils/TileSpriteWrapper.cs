using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public enum TileType
{
    //standard
    Block, //4 edges
    Center, //no edges
    
    //edges
    EdgeLeft,
    EdgeRight,
    EdgeTop,
    EdgeTopLeft,
    EdgeTopRight,
    EdgeBottom,
    EdgeBottomLeft,
    EdgeBottomRight,

    //inner corners
    InnerTopLeft,
    InnerTopRight,
    InnerBottomLeft,
    InnerBottomRight,

    //joint (one thick, two adjacnet exits)
    JointTopLeft,
    JointTopRight,
    JointBottomLeft,
    JointBottomRight,

    //pipes (one thick, two non adjacent exists)
    PipeHorizontal,
    PipeVertical,

    //T (one thick, three exits)
    TLeft,
    TRight,
    TTop,
    TBottom,

    //cross (one thick, 4 exits)
    Cross,
}

[Serializable]
[CreateAssetMenu(fileName = "TileSpriteWrapper", menuName = "ScriptableObjects/TileSpriteWrapper")]
public class TileSpriteWrapper : ScriptableObject
{

    [SerializeField]
    [SerializeProperty("Block")]
    private Sprite block;
    public Sprite Block
    {
        get => block;
        set
        {
            block = value;
            for(int i = 0; i < wrappers.Length; i++)
            {
                wrappers[i] = TryAssignTileType(i, block);
            }
        }
    }
    [ReadOnly]
    [SerializeField]
    private  SpriteWrapper [] wrappers = new SpriteWrapper[Enum.GetValues(typeof(TileType)).Length];

    

    private SpriteWrapper TryAssignTileType(int i, Sprite sprite)
    {
        Sprite blockTypeSprite;
        SpriteWrapper wrapper = CreateInstance<SpriteWrapper>();
        #if UNITY_EDITOR
        string path = BlockTypePath(i, AssetDatabase.GetAssetPath(sprite));
        blockTypeSprite = AssetDatabase.LoadAssetAtPath<Sprite>(BlockTypePath(i, AssetDatabase.GetAssetPath(sprite)));
        AssetDatabase.CreateAsset(wrapper, SpriteWrapperPath(path));
        #endif
        wrapper.OriginalSprite = blockTypeSprite == null ? sprite : blockTypeSprite;
        return wrapper;
    }

    private static string BlockTypePath(int i, string path)
    {
        int pos = path.LastIndexOf('_');

        return path.Substring(0, pos) + "_" + Enum.GetName(typeof(TileType), i) + path.Substring(pos, path.Length - pos);
    }

    private static string SpriteWrapperPath(string path)
    {
        string folderName = "Sprites/";
        int folderPos = path.IndexOf(folderName) + folderName.Length;
        int extPos = path.IndexOf('.');
        return path.Substring(0, folderPos) + path.Substring(folderPos, extPos - folderPos) + ".asset";
    }
}