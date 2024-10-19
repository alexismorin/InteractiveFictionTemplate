using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    GameObject resumeButton;

    [SerializeField]
    Button loadButton;

    Button[] buttons;

    void Start()
    {
        ToggleResume();

        buttons = gameObject.GetComponentsInChildren<Button>();

        ToggleLoad();
    }

    public void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        ToggleLoad();
    }

    public void DisableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
    }

    void ToggleResume()
    {
        if (PlayerPrefs.GetString("lastLoadedSaveFile", "none") == "none")
        {
            resumeButton.SetActive(false);
        }
        else
        {
            resumeButton.SetActive(true);
        }
    }

    void ToggleLoad()
    {

        PlayerPrefs.SetString("saveFileFolder", System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Replace("\\", "/") + "/Prizrak/");
        string saveGamesPath = PlayerPrefs.GetString("saveFileFolder");

        // Create save folder if it doesn't exist
        if (!System.IO.Directory.Exists(saveGamesPath))
        {
            System.IO.Directory.CreateDirectory(saveGamesPath);
        }

        // Make load button available only if save files are present
        if (System.IO.Directory.Exists(saveGamesPath) && Directory.EnumerateFiles(saveGamesPath).Any())
        {
            loadButton.interactable = true;
        }
        else
        {
            loadButton.interactable = false;
        }
    }

}
