using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchyAnimation : MonoBehaviour
{
    [SerializeField]
    private GlitchySprite glitchySprite;
    //private int currentSpriteSheet;

    private int index;
    public int Index
    {
        get => index;
        set
        {
            index = NextFrame(value);
            glitchySprite.Draw(index);
            //print($"Glitchy sprite is: {glitchySprite.SpriteSheet.OriginalAt(index).name}");
        }
    }

    private int NextFrame(int index)
    {
        if (gameObject.name == "PlatformerPlayer")
        {
            Debug.Log($"Animation frame attempt {index}");
        }
        if (index == this.index || glitchySprite.SpriteSheet.OriginalAt(index) != glitchySprite.SpriteSheet.OriginalAt(this.index))
        {

            return index;
        }
        int nextIndex = index;
        do
        {
            nextIndex = (nextIndex + 1) % glitchySprite.SpriteSheet.Count;
        }
        while (nextIndex != index &&
            glitchySprite.SpriteSheet.OriginalAt(nextIndex) == glitchySprite.SpriteSheet.OriginalAt(this.index));

        if (gameObject.name == "PlatformerPlayer")
        {
            Debug.Log($"Animation frame {nextIndex}");
        }
        return nextIndex;
    }
}
