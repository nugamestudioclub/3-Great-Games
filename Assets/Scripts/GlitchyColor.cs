using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlitchyHue
{
    White,
    Blue,
    Indigo,
    Violet,
    Purple,
    Pink,
    Orange,
    Amber,
    Yellow,
    LimeGreen,
    Green,
    Teal,
    Cyan,
    Black,
}

public enum GlitchyLuma
{
    VeryDark,
    Dark,
    Medium,
    Light,
}

[Serializable]
public struct GlitchyColor
{
    public GlitchyHue hue;
    public GlitchyLuma luma;
}

