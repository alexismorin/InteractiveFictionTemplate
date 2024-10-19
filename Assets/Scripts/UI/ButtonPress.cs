using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    Button button;
    bool canPress = true;
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        button = GetComponent<Button>();
    }

    public void ToggleButtonDisplay(bool state)
    {
        if (state)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
    }


    void Update()
    {
        if (canvasGroup.interactable)
        {

            if (Input.GetKeyDown(KeyCode.Return) && canPress)
            {
                button.onClick.Invoke();
            }
        }
    }

    public void Click()
    {
        canPress = false;
        Invoke("Reset", 0.5f);
    }

    public void StartDialogueLock()
    {
        canPress = false;
        Invoke("Reset", 0.5f);
    }

    void Reset()
    {
        canPress = true;
    }
}
