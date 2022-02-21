using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheItem
{
    public string Register { get { return register; } }
    private string register;
    public string[] Code { get { return codes; } }
    private string[] codes;

    public Sprite Img { get { return img;} }
    private Sprite img;

    /// <summary>
    /// Creates a new CacheItem object.
    /// </summary>
    /// <param name="register">The register location in cache in hex of the item.</param>
    /// <param name="codes">Every hex code reference in the item. Use (-1) hex if code is null</param>
    /// <param name="img">The Sprite image used to reference this item.</param>
    public CacheItem(string register,string[] codes,Sprite img)
    {
        this.register = register;
        this.codes = codes;
        this.img = img;
    }

}
