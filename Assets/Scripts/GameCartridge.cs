using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(
    fileName = nameof(GameCartridge),
    menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(GameCartridge))
]
public class GameCartridge : ScriptableObject
{
    [SerializeField]
    private GameId id;
    public GameId Id => id;

    [SerializeField]
    private Entity player;
    public Entity Player { 
        get => player; 
        private set {
            player = value;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        } 
    }

    //[field: SerializeField]
    [SerializeField]
    private ReadOnlyPalette<Entity> entities; //make into entities
    public ReadOnlyPalette<Entity> EntitiesPalette
    {
        get => entities;
        private set
        {
            entities = value;
            for (int i = 0; i < entities.Count; i++)
            {
                //entityDataPalette.Add(entities[i].template);
            }
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }

    //[SerializeField]
    //private Palette<EntityData> entityDataPalette = new Palette<EntityData>();

   // public IReadOnlyPalette<EntityData> EntityDataPalette => entityDataPalette;

    [SerializeField]
    private ReadOnlyPalette<Color> colors;
    public IReadOnlyPalette<Color> ColorPalette => colors;

    public Sprite ColorPaletteSprite
    {
        get
        {
            const int WIDTH = 4;
            Texture2D texture = new Texture2D(WIDTH, WIDTH, TextureFormat.ARGB32, false);
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < WIDTH; y++)
                {
                    texture.SetPixel(x, y, colors[x * WIDTH + y]);
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.Apply();

            return Sprite.Create(texture, new Rect(0, 0, WIDTH, WIDTH), Vector2.zero);
        }
    }

    [SerializeField]
    private ReadOnlyPalette<AudioClip> sounds;

    public static GameCartridge FromHex(string hex)
    {
        return GameCollection.Instance.Cartridge(GameMemory.HexToInt(hex.Substring(2, 1)));
    }
}