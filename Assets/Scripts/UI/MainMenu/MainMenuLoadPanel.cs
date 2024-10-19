using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuLoadPanel : MonoBehaviour
{
    [SerializeField]
    GameObject loadFilePrefab;

    [SerializeField]
    Transform container;

    [SerializeField]
    MainMenuBlackFade fade;

    void OnEnable()
    {

        // Remove old files
        for (int i = container.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }

        foreach (string file in System.IO.Directory.GetFiles(PlayerPrefs.GetString("saveFileFolder")))
        {

            var button = Instantiate(loadFilePrefab, container.transform.position, container.transform.rotation, container);

            string[] splitStrings = file.Split("Prizrak/");
            string finalString = splitStrings[splitStrings.Length - 1].Split(".json")[0];

            button.GetComponentInChildren<TextMeshProUGUI>().text = finalString;

            button.GetComponent<MainMenuLoadButton>().saveFileToLoad = file;
            button.GetComponent<MainMenuLoadButton>().fade = fade;
        }

    }

}
