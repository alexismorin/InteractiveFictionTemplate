using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMenu : MonoBehaviour
{

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    TextMeshProUGUI framerateText;


    bool enabled;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (canvasGroup.alpha == 1f)
            {
                canvasGroup.alpha = 0f;
                enabled = false;
            }
            else
            {
                canvasGroup.alpha = 1f;
                enabled = true;
            }
        }


        if (enabled)
        {
            framerateText.text = (1.0f / Time.smoothDeltaTime).ToString() + " FPS";
        }
    }
}
