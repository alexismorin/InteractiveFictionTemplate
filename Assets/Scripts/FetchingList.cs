using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fetching List", menuName = "Prizrak/Utilities/Fetching List")]
public class FetchingList : ScriptableObject
{

    [Header("Facts & Dialogue")]

    public Fact[] facts;
    public DialogueCharacter[] characters;

}
