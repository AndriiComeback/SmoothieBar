using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Smoothie Bar/LevelParameter")]
public class LevelParameter : ScriptableObject
{
    public List<Fruit> fruits;
    public Color targetColor;
    public int targetPercent = 90;
}