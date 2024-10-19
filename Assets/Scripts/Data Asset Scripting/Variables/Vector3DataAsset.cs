using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3 Asset", menuName = "Data Asset Scripting/Vector3 Data Asset")]
public class Vector3DataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    Vector3 initialValue;

    [NonSerialized]
    public Vector3 value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}