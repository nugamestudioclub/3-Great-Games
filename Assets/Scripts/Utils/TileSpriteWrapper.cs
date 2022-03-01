using System;
using System.Collections.Generic;
using System.IO;
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
            for (int i = 0; i < wrappers.Length; i++)
            {
                wrappers[i] = TryAssignTileType(i, block);
            }
        }
    }
    [ReadOnly]
    [SerializeField]
    private SpriteWrapper[] wrappers = new SpriteWrapper[Enum.GetValues(typeof(TileType)).Length];



    private SpriteWrapper TryAssignTileType(int i, Sprite sprite)
    {
        Sprite blockTypeSprite = null;
        SpriteWrapper wrapper = CreateInstance<SpriteWrapper>();
#if UNITY_EDITOR
        string tileType = Enum.GetName(typeof(TileType), i);
        string spritePath = AssetDatabase.GetAssetPath(sprite);
        string folderPath = spritePath.Substring(0, spritePath.LastIndexOf('/'));
        string tilename = Tilename(spritePath);
        int extPos = spritePath.LastIndexOf('.');
        string ext = spritePath.Substring(extPos, spritePath.Length - extPos);
        string tilepath = Tilepath(folderPath, tilename, tileType, ext);


        string spriteWrapperPath = SpriteWrapperPath(tilename, tileType);
        //Debug.Log($"Block Tile path : {tilepath }");
        //Debug.Log($"Sprite Wrapper Path : {spriteWrapperPath}");
        blockTypeSprite = AssetDatabase.LoadAssetAtPath<Sprite>(tilepath);
        AssetDatabase.CreateAsset(wrapper, spriteWrapperPath);

#endif
        wrapper.OriginalSprite = blockTypeSprite == null ? sprite : blockTypeSprite;
        return wrapper;
    }

    private static string Tilename(string path)
    {
        int prefixPos = path.LastIndexOf('_');
        int extPos = path.LastIndexOf('.');
        int finalPos = prefixPos < 0 ? extPos : prefixPos;

        int tilenamePos = path.LastIndexOf('/');

        string tilename = path.Substring(tilenamePos + 1, finalPos - tilenamePos - 1);
        return tilename;
    }

    private static string Tilepath(string folderPath, string tilename, string tiletype, string ext)
    {
        return $"{folderPath}/{tilename}_{tiletype}{ext}";
    }

    private static string SpriteWrapperPath(string tilename, string tiletype)
    {
        string folderPath = $"Sprites/SpriteWrappers/TileSpriteWrappers/{tilename}";
        string fullPath = $"{Application.dataPath}/{folderPath}";
        
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }

        return $"Assets/{folderPath}/{tilename}_{tiletype}.asset";
    }
}