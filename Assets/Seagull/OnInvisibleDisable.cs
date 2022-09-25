using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInvisibleDisable : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    void Update()
    {
        if (!meshRenderer.isVisible)
            gameObject.SetActive(false);
    }
}
