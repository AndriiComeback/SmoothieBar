using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorMixerHelper
{
    public static float GetConformityDegreeInPercent(List<Color> colors, Color targetColor) {
        float r = 0, g = 0, b = 0;
        foreach (Color color in colors) {
            r += color.r;
            g += color.g;
            b += color.b;
        }
        Color result = new Color(r / colors.Count, g / colors.Count, b / colors.Count);
        float targetSum = targetColor.r + targetColor.g + targetColor.b;
        float resultSum = result.r + result.g + result.b;
        float conformity = 100 - Mathf.Abs(targetSum - resultSum) / 255 * 3 * 100;
        return conformity;
    }
}
