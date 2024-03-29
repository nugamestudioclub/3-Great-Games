﻿using UnityEngine;

[CreateAssetMenu(
    fileName = nameof(MultiSpriteSheet),
    menuName = Paths.SCRIPTABLE_SPRITE_SHEETS + "/" + nameof(MultiSpriteSheet))
]
public class MultiSpriteSheet : SpriteSheet
{
    [SerializeField]
    private Palette<SpriteSheet> spriteSheets;

    public override Sprite Original => OriginalAt(0);

    public override Sprite Grey => GreyAt(0);

    public override Sprite OriginalAt(int index) => spriteSheets[index].Original;

    public override Sprite GreyAt(int index) => spriteSheets[index].Grey;

    public override Sprite FindUniqueOriginal(int start) => OriginalAt(start);

    public override Sprite FindUniqueGrey(int start) => GreyAt(start);
}