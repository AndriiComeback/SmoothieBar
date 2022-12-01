using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Smoothie Bar/LevelParameter")]
public class LevelParameter : ScriptableObject
{
    public Dictionary<Fruit, Transform> fruitSetup;
    public Color targetColor;
}