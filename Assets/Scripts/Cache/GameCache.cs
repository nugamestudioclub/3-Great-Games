using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCache
{
    private CacheItem[] items;
    /// <summary>
    /// The number of items in the cache.
    /// </summary>
    public int Length { get { return this.length; } }
    private int length = 0;
    /// <summary>
    /// The size of the cache.
    /// </summary>
    public int Size { get { return this.size; } }
    private int size;
    /// <summary>
    /// Creates a new Game Cache of a given size with the following initial items.
    /// </summary>
    /// <param name="size">The size of the cache.</param>
    /// <param name="items">The initial items in the gamecache.</param>
    public GameCache(int size, params CacheItem[] items)
    {
        this.size = size;
        this.items = new CacheItem[size];
        foreach(CacheItem item in items)
        {
            this.set(item);
            
        }
    }

    public CacheItem get(string index)
    {
        int i = int.Parse(index, System.Globalization.NumberStyles.HexNumber);
        return items[i];
    }
    public void set(string index,CacheItem item)
    {
        int i = int.Parse(index, System.Globalization.NumberStyles.HexNumber);
        if (this.items[i] == null)
        {
            this.length++;
        }
        this.items[i] = item;
    }
    public void set(CacheItem item)
    {
        int i = int.Parse(item.Register, System.Globalization.NumberStyles.HexNumber);
        if (this.items[i] == null)
        {
            this.length++;
        }

        this.items[i] = item;
    }
    /// <summary>
    /// Checks if this item and the item at that index are matching, or equal objects or null.
    /// </summary>
    /// <param name="item">The item being checked</param>
    /// <returns></returns>
    public bool contains(CacheItem item)
    {
        int i = int.Parse(item.Register, System.Globalization.NumberStyles.HexNumber);
        if (this.items[i] == null)
        {
            return false;
        }
        return this.items[i].Equals(item)||(this.items[i].Register==item.Register&&this.items[i].Img==item.Img);
    }
    public void flush()
    {
        this.items = new CacheItem[this.size];
        this.length = 0;
    }
    
    


}
