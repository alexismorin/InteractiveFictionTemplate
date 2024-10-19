using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalShaderValues : MonoBehaviour
{

    [Header("Textures")]

    [SerializeField]
    Texture[] textures;

    [SerializeField]
    string[] textureNames;

    void Start()
    {
        for (int i = 0; i < textureNames.Length; i++)
        {
            Shader.SetGlobalTexture(textureNames[i], null);
        }

        Generate();
    }

    void Generate()
    {

        for (int i = 0; i < textureNames.Length; i++)
        {
            Shader.SetGlobalTexture(textureNames[i], textures[i]);
        }
    }



}