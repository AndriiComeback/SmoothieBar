using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        //int layerMask = 1 << LayerMask.NameToLayer("Mixable");
        if (Equals(other.gameObject.layer, LayerMask.NameToLayer("Mixable"))) {
            float time = .25f;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(gameObject.transform.DOLocalMove(new Vector3(5, 5, 5), time));
            //mySequence.Append();
            //mySequence.Join(fruitGameobject.transform.DORotate(new Vector3(90, 0, 0) + GetRandomFlick() * 25, jumpTime));
        }
    }
    private Vector3 GetRandomFlick() {
        return new Vector3(Random.Range(-.5f, .5f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
    }
}
