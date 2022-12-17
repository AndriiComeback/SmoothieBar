using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushOnSpawn : MonoBehaviour
{
    [SerializeField] float pushStrength = 1;
    private Rigidbody rb;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable() {
        rb.AddForce(GetRandomFlick() * pushStrength, ForceMode.Impulse);
    }
    private Vector3 GetRandomFlick() {
        return new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
    }
}
