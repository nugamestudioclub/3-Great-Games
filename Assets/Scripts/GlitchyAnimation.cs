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
            glitchySprite.SpriteSheet = sheets[index];
  
        }
    } 
}
