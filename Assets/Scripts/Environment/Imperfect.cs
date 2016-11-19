using UnityEngine;
using System.Collections;

public class Imperfect : MonoBehaviour
{
    public float RotationRange = 2;

    void Start()
    {
        var meshRenderer = GetComponentInChildren<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.transform.Rotate(Random.Range(-RotationRange, RotationRange), Random.Range(-RotationRange, RotationRange), Random.Range(-RotationRange, RotationRange) + Random.Range(0, 4) * 90);
        }
    }

    void Update()
    {

    }
}
