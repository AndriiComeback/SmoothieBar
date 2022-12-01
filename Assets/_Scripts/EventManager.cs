using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager {
    public delegate void TapAction(GameObject gameObject);
    public static event TapAction OnFruitTapped;

    public static void FruitTapped(GameObject gameObject) {
        if (OnFruitTapped != null) {
            OnFruitTapped(gameObject);
        }
    }
}
