using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyAnimation : MonoBehaviour
{
    [SerializeField]
    private SingleSpriteSheet[] sheets;
    [SerializeField]
    private NewGlitchySprite glitchySprite;

    private int index;
    
    public int Index { 
        get => index; 
        set {
            index = value;
            if(name.Contains("Ladybug"))
                Debug.Log($"{name}:Index changing to {index}");
            glitchySprite.SpriteSheet = sheets[index];
  
        }
    } 
}
