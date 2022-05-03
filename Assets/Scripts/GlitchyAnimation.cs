using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyAnimation : MonoBehaviour
{
    [SerializeField]
    private GlitchySprite glitchySprite;

    private int index;
    
    public int Index { 
        get => index; 
        set {
            index = value;
            glitchySprite.Draw(Index);
            //print($"Glitchy sprite is: {glitchySprite.SpriteSheet.OriginalAt(index).name}");
        }
    } 
}
