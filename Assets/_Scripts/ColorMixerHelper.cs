using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorMixerHelper
{
    public static (float, Color) GetConformityDegreeInPercent(List<Color> colors, Color targetColor) {
        float r = 0, g = 0, b = 0;
        foreach (Color color in colors) {
            r += color.r;
            g += color.g;
            b += color.b;
        }
        Color result = new Color(r / colors.Count, g / colors.Count, b / colors.Count);

        float conformity = 1 - Mathf.Abs(targetColor.r - result.r);
        conformity *= 1 - Mathf.Abs(targetColor.g - result.g);
        conformity *= 1 - Mathf.Abs(targetColor.b - result.b);
        conformity *= 100;

        if (conformity > 99) {
            conformity = 100;
        }
        if (conformity < 0) {
            conformity = 0;
        }

        return (conformity, result);
    }
}
