using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class YarnLineViewer : DialogueViewBase
{


    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    BoolDataAsset inDialogue;
    [SerializeField]
    GameObjectDataAsset currentInteractionTarget;
    [SerializeField]
    FetchingList fetchingList;

    [Space(10)]

    [SerializeField]
    GameObject continueButton;

    [SerializeField]
    DialogueRunner dialogueRunner;
    [SerializeField]
    OptionGreyout optionGreyout;

    [Header("References")]

    [SerializeField]
    FloatEvent passTimeEvent;
    [SerializeField]
    FloatDataAsset defaultTimePassedForNode;


    // Internals

    Vector3DataAsset playerPosition;
    Action advanceHandler = null;

    void Awake()
    {
        dialogueRunner.AddCommandHandler("EndSection", EndSection);
        dialogueRunner.AddCommandHandler<string, bool>("SendAnimation", SendAnimation);
    }

    public void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public void StartDialogue()
    {
        text.text = "";

    }

    public void EndDialogue()
    {
        text.text = "";
    }



    void SendAnimation(string inputAnimation, bool rotate = false)
    {
        currentInteractionTarget.value.GetComponent<NPCAnimation>().Animate(inputAnimation, rotate);
    }

    void EndSection()
    {
        text.text += "\n_____________________________________";
    }

    // Run line, then wait for interruption signal
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {

        if (gameObject.activeInHierarchy == false)
        {
            onDialogueLineFinished();
            return;
        }

        optionGreyout.AddLine(dialogueLine.Text.Text);

        // Line break between dialogue
        text.text += "\n\n ";

        // Actual text
        string characterName;
        if (dialogueLine.CharacterName == null)
        {
            characterName = "Narrator";
        }
        else
        {
            characterName = dialogueLine.CharacterName;
        }

        foreach (var character in fetchingList.characters)
        {
            if (character.name == characterName)
            {

                StartCoroutine(animateLineAppearing(character, character.colorDisplayType, dialogueLine));
            }
        }



    }

    void FinishTreatingText(LocalizedLine dialogueLine)
    {
        // Gather metadata
        List<string> metaDataList = new List<string>();
        if (dialogueLine.Metadata != null)
        {
            foreach (var metadata in dialogueLine.Metadata)
            {
                metaDataList.Add(metadata);
            }
        }

        // Pass time
        passTimeEvent.Raise(defaultTimePassedForNode.value);

        // Line spoken by you or preceding a choice
        if (dialogueLine.CharacterName == "You" || metaDataList.Contains("lastline"))
        {
            UserRequestedViewAdvancement();
        }
    }

    IEnumerator animateLineAppearing(DialogueCharacter character, DialogueCharacter.nameDisplayType nameDisplayType, LocalizedLine dialogueLine)
    {
        requestInterruptMethod();

        string currentText = text.text;

        Color startNameColour = new Color(character.colour.r, character.colour.g, character.colour.b, 0f);
        Color endNameColour = new Color(character.colour.r, character.colour.g, character.colour.b, 1f);

        Color startTestColour = new Color(text.color.r, text.color.g, text.color.b, 0f);
        Color endTextColour = new Color(text.color.r, text.color.g, text.color.b, 1f);

        float length = 0.25f;
        float elapsed = 0.0f;

        MakeTextAppear(startNameColour, startTestColour, currentText, character, dialogueLine);
        while (elapsed < length)
        {
            float progress = Mathf.SmoothStep(0f, 1f, elapsed / length);
            Color computedNameColor = new Color(character.colour.r, character.colour.g, character.colour.b, progress);
            Color computedTextColor = new Color(text.color.r, text.color.g, text.color.b, progress);

            MakeTextAppear(computedNameColor, computedTextColor, currentText, character, dialogueLine);

            elapsed += Time.deltaTime;
            yield return null;
        }
        MakeTextAppear(endNameColour, endTextColour, currentText, character, dialogueLine);

        FinishTreatingText(dialogueLine);
    }

    void MakeTextAppear(Color nameColor, Color textColor, string currentText, DialogueCharacter character, LocalizedLine dialogueLine)
    {
        text.text = currentText;

        string htmlNameColour = "<color=#" + ColorUtility.ToHtmlStringRGBA(nameColor) + ">";
        string htmlTextColour = "<color=#" + ColorUtility.ToHtmlStringRGBA(textColor) + ">";

        if (character.displayName)
        {
            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.colourOnlyName)
            {
                text.text += htmlNameColour + dialogueLine.CharacterName + "</color> - " + htmlTextColour + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }
            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.colourEntireText)
            {
                text.text += htmlNameColour + dialogueLine.CharacterName + " - " + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }
            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.doNotColour)
            {
                text.text += htmlTextColour + dialogueLine.CharacterName + " - " + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }

        }
        else
        {

            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.colourOnlyName) // This should not be actually happening since we aren't displaying the name
            {
                text.text += htmlNameColour + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }
            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.colourEntireText)
            {
                text.text += htmlNameColour + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }
            if (character.colorDisplayType == DialogueCharacter.nameDisplayType.doNotColour)
            {
                text.text += htmlTextColour + dialogueLine.TextWithoutCharacterName.Text + "</color>";
            }


        }
    }

    void requestInterruptMethod()
    {
        advanceHandler = requestInterrupt;
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (gameObject.activeInHierarchy == false)
        {
            onDialogueLineFinished();
            return;
        }

        advanceHandler = null;
        onDialogueLineFinished();
    }

    public override void DismissLine(Action onDismissalComplete)
    {

        if (gameObject.activeInHierarchy == false)
        {
            onDismissalComplete();
            return;
        }

        advanceHandler = null;
        onDismissalComplete();
    }

    public override void UserRequestedViewAdvancement()
    {
        advanceHandler?.Invoke();
    }



}
