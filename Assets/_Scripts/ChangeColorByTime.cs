using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorByTime : MonoBehaviour
{
    [SerializeField] float changeDelay = 1f;
    Renderer r;
    void Awake()
    {
        r = gameObject.GetComponent<Renderer>();
        StartCoroutine(ChangeColor());
    }

    IEnumerator ChangeColor() {
        while (true) {
            r.material.color = new Color(.4f, .8f, 0);
            yield return new WaitForSeconds(changeDelay);
            r.material.color = new Color(.4f, 1f, 0);
            yield return new WaitForSeconds(changeDelay);
        }
    }
}
