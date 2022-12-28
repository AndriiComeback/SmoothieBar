using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderRandomMeshOnEnable : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] m_renderer;
    private void OnEnable() {
        foreach (SkinnedMeshRenderer item in m_renderer) {
            item.enabled = false;
        }
        int i = Random.Range(0, m_renderer.Length);
        m_renderer[i].enabled = true;
    }
}
