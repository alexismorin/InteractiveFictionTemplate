using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Prizrak/Character")]
public class DialogueCharacter : ScriptableObject
{
    public enum nameDisplayType { colourOnlyName, colourEntireText, doNotColour }

    public bool displayName = true;

    [Space(10)]

    public Sprite portrait;
    public Color colour;
    public nameDisplayType colorDisplayType;
}
