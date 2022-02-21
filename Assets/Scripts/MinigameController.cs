using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour
{
    //023935 -1
    //120499 -2
    //393933 -3

    //coin -4

    /// <summary>
    /// Appends all objects that this game uses to the cache
    /// </summary>
    public abstract void WriteToCache(GameCache cache);

    /// <summary>
    /// Reads 
    /// </summary>
    /// <param name="cache"></param>
    public abstract void ReadCache(GameCache cache);

    /// <summary>
    /// Instantiate all objects at their coordinates
    /// </summary>
    public abstract void LoadGame();
}
