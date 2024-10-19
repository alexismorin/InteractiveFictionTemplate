using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Perlin Noise", menuName = "Perlin Noise")]
public class PerlinNoise : ScriptableObject
{

    public float size = 0.01f;
    public float strength = 1f;
    public AnimationCurve falloffRemapCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    public Color color = new Vector4(0f, 0f, 0f, 0f);

    public float ReturnNoise(Vector3 samplePos)
    {
        return falloffRemapCurve.Evaluate(Mathf.PerlinNoise((samplePos.x) * size, (samplePos.z) * size)) * strength;
    }

    public Color ReturnNoiseColor(Vector3 samplePos)
    {
        return falloffRemapCurve.Evaluate(Mathf.PerlinNoise((samplePos.x) * size, (samplePos.z) * size)) * color;
    }

}