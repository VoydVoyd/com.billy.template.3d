using UnityEngine;

public static class ColourExtensions
{
    public static Color SetAlpha(this Color colour, float a)
    {
        colour.a = a;
        return colour;
    }
}