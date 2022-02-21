using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheManager : MonoBehaviour
{
    [Tooltip("When creating items, If the item is not a color, leave it blank.\n if it is, then" +
        " leave img empty.")]
    [SerializeField]
    private GameCacheBuilder[] shortTermCaches;
    [Tooltip("When creating items, If the item is not a color, leave it blank.\n if it is, then" +
        " leave img empty.")]
    [SerializeField]
    private GameCacheBuilder longTermCache;
   /// <summary>
   /// Our long term caches
   /// </summary>
    private GameCache[] _shortTermCaches;
    private GameCache _longTermCache;

    private void Awake()
    {
        _shortTermCaches = new GameCache[shortTermCaches.Length];
        _longTermCache = longTermCache.build();
        for(int i = 0;i < shortTermCaches.Length; i++)
        {
            _shortTermCaches[i] = shortTermCaches[i].build();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class GameCacheBuilder
{
    public static int cacheSize { get { return 256; } }
    [SerializeField]
    private string name;
    [SerializeField]
    private CacheItemBuilder[] cache;

    public GameCache build()
    {
        CacheItem[] _items = new CacheItem[this.cache.Length];
        for(int i = 0; i < cache.Length; i++)
        {
            _items[i] = cache[i].build();
        }

        return new GameCache(name,cacheSize,_items);
    }

   
}

[System.Serializable]
public class CacheItemBuilder
{
    [SerializeField]
    private string register;
    [SerializeField]
    private string code;
    [SerializeField]
    private CacheItemObjectType type;
    [SerializeField]
    private Color color;
    [SerializeField]
    private Sprite img;
    [SerializeField]
    private Object obj;

    public CacheItem build()
    {
        CacheItem item = new CacheItem(register, code, type,obj,color, img);
        return item;
    }

}
