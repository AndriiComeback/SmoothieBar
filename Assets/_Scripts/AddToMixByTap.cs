using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AddToMixByTap : MonoBehaviour {
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            HandleInput(ray);
            //Color newColor = new Color(Random.Range(.0f, 1f), Random.Range(.0f, 1f), Random.Range(.0f, 1f));
            //hit.collider.GetComponent<MeshRenderer>().material.color = newColor;
        }

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            HandleInput(ray);
        }

#endif
    }

    private void HandleInput(Ray ray) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Mixable")) {
                EventManager.FruitTapped(hit.collider.gameObject);
            }
        }
    }
}
