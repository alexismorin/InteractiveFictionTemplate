using System;
using UnityEngine;

[CreateAssetMenu(fileName = "String Data Asset", menuName = "Data Asset Scripting/String Data Asset")]
public class StringDataAsset : ScriptableObject, ISerializationCallbackReceiver
{

    [TextArea]
    [SerializeField]
    string initialValue;

    [NonSerialized]
    public string value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize() { }
}