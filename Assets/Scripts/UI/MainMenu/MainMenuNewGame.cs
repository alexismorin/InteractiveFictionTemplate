using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuNewGame : MonoBehaviour
{
    [SerializeField]
    MainMenuBlackFade fade;

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameCoroutine());

    }

    IEnumerator StartNewGameCoroutine()
    {
        fade.FadeToBlack();
        yield return new WaitForSeconds(3f);

        PlayerPrefs.DeleteKey("lastLoadedSaveFile");
        SceneManager.LoadScene("Loading", LoadSceneMode.Single);
    }


}
