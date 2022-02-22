using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Palette<T> : ScriptableObject
{
    [SerializeField]
    private List<T> items;

    public T this[int index]
    {
        get => items[index % items.Count];

        set => items[index] = value;
    }
    
   
}

[CreateAssetMenu(fileName = "ColorPalette", menuName = "ScriptableObjects/Palette/Color", order = 1)] 
public class ColorPalette : Palette<Color>
{

}

[CreateAssetMenu(fileName = "AudioPalette", menuName = "ScriptableObjects/Palette/Audio", order = 1)]
public class AudioPalette : Palette<AudioClip>
{

}

[CreateAssetMenu(fileName = "GameObjectPalette", menuName = "ScriptableObjects/Palette/GameObject", order = 1)]
public class GameObjectPalette : Palette<GameObject>
{

}