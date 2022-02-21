using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheItem
{
    public string Register { get { return register; } }
    private string register;
    public string Code { get { return code; } }
    private string code;

    public CacheItemObjectType Type { get { return type; } }
    private CacheItemObjectType type;

    public Color Color { get { return clr; } }
    private Color clr;
    public Sprite Img { get { return img;} }
    private Sprite img;

    public Object Object { get { return this.obj; } }
    private Object obj;

    /// <summary>
    /// Creates a new CacheItem object.
    /// </summary>
    /// <param name="register">The register location in cache in hex of the item.</param>
    /// <param name="codes">Every hex code reference in the item. Use (-1) hex if code is null</param>
    /// <param name="img">The Sprite image used to reference this item.</param>
    public CacheItem(string register,string code,CacheItemObjectType type, Object obj, Color clr,Sprite img)
    {
        this.register = register;
        this.code = code;
        this.img = img;
        this.type = type;
        this.clr = clr;
        this.obj = obj;
    }

}

public enum CacheItemObjectType
{
    Color,Sprite,Item
}
