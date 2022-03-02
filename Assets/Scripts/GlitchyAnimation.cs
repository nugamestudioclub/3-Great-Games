using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyAnimation : MonoBehaviour
{
    [SerializeField]
    private SingleSpriteSheet[] sheets;
    [SerializeField]
    private GlitchySprite glitchySprite;

    private int index;
    
    public int Index { 
        get => index; 
        set {
            index = value;
            Debug.Log($"Index changing to{index}");
            glitchySprite.SpriteSheet = sheets[index];
        }
    } 
}
