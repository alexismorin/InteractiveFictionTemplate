using UnityEditor;

public class SaveAssetsUtility : UnityEditor.AssetModificationProcessor
{


    static string[] OnWillSaveAssets(string[] paths)
    {

        FetchingList fetchingList = (FetchingList)AssetDatabase.LoadAssetAtPath("Assets/Data Assets/Fetching List.asset", typeof(FetchingList));

        // Facts
        string[] factsGUIDs = AssetDatabase.FindAssets("t:Fact", new string[] { "Assets/Data Assets/Facts" });
        Fact[] facts = new Fact[factsGUIDs.Length];
        for (int i = 0; i < facts.Length; i++)
        {
            facts[i] = (Fact)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(factsGUIDs[i]), typeof(Fact));
        }
        fetchingList.facts = facts;

        // Characters
        string[] characterGUIDs = AssetDatabase.FindAssets("t:DialogueCharacter", new string[] { "Assets/Data Assets/Characters" });
        DialogueCharacter[] characters = new DialogueCharacter[characterGUIDs.Length];
        for (int i = 0; i < characterGUIDs.Length; i++)
        {
            characters[i] = (DialogueCharacter)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(characterGUIDs[i]), typeof(DialogueCharacter));
        }
        fetchingList.characters = characters;

        EditorUtility.SetDirty(fetchingList);
        //    AssetDatabase.Refresh();

        // Boilerplate
        foreach (string path in paths)
        {

        }
        return paths;


    }
}