using UnityEditor;
using UnityEngine;


public class ClothCoefficientsAssigner : MonoBehaviour
{

    [MenuItem("Tools/Bind Green Vertex Colours to Cloth Coefficients")]
    static void Bind()
    {

        float scale = 0.1f;

        Mesh bodyMesh = Selection.activeGameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        Color[] bodyColors = bodyMesh.colors;
        Vector3[] bodyMeshVertices = bodyMesh.vertices;
        Cloth cloth = Selection.activeGameObject.GetComponent<Cloth>();
        ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[cloth.coefficients.Length];

        // Host Gameobect

        GameObject host = new GameObject("coefficient host", typeof(MeshFilter));
        host.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

        MeshFilter meshFilter = host.GetComponent<MeshFilter>();
        meshFilter.mesh = bodyMesh;
        host.AddComponent<MeshRenderer>();

        Cloth hostCloth = host.AddComponent<Cloth>();
        Vector3[] hostClothVertices = hostCloth.vertices;

        for (int j = 0; j < hostClothVertices.Length; j++)
        {
            coefficients[j].collisionSphereDistance = 0f;
            coefficients[j].maxDistance = 0f;

            for (int i = 0; i < bodyMeshVertices.Length; i++)
            {
                if (Vector3.Distance(bodyMeshVertices[i], hostClothVertices[j]) < 0.001f)
                {
                    Debug.DrawLine(hostClothVertices[j], hostClothVertices[j] + Vector3.back * 0.01f, new Color(0f, bodyColors[i].g, 0f, 1f), 3f);
                    coefficients[j].maxDistance = bodyColors[i].g * scale;
                }
            }
        }

        cloth.coefficients = coefficients;
        DestroyImmediate(host);

    }


}
