using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Data Asset", menuName = "Data Asset Scripting/Float Data Asset")]
public class FloatDataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [SerializeField]
    float initialValue;

    [NonSerialized]
    public float value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}