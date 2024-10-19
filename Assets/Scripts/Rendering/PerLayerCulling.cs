using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerLayerCulling : MonoBehaviour
{
    [SerializeField]
    float[] distances = new float[32];


    void Start()
    {
        Camera camera = GetComponent<Camera>();
        camera.layerCullDistances = distances;

        Destroy(this);
    }
}
