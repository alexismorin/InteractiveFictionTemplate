using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yarn.Unity
{
    public class PrizrakOptionsViewer : DialogueViewBase
    {

        [SerializeField]
        Color unlockedLineColor;
        [SerializeField]
        OptionGreyout optionGreyout;

        [SerializeField]
        GameObject continueButton;
        [SerializeField]
        Text lineText;

        [SerializeField] CanvasGroup canvasGroup;

        [SerializeField] OptionView optionViewPrefab;

        [SerializeField] float fadeTime = 0.1f;

        [SerializeField] bool showUnavailableOptions = false;

        // A cached pool of OptionView objects so that we can reuse them
        List<OptionView> optionViews = new List<OptionView>();

        // The method we should call when an option has been selected.
        Action<int> OnOptionSelected;

        // The line we saw most recently.
        LocalizedLine lastSeenLine;

        public void Start()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Reset()
        {
            canvasGroup = GetComponentInParent<CanvasGroup>();
        }


        public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
        {

            // Trim edge newlines. gross
            //   lineText.text = lineText.text.TrimEnd('\n');

            continueButton.SetActive(false);

            // Hide all existing option views
            foreach (var optionView in optionViews)
            {
                optionView.gameObject.SetActive(false);
            }

            // If we don't already have enough option views, create more
            while (dialogueOptions.Length > optionViews.Count)
            {
                var optionView = CreateNewOptionView();
                optionView.gameObject.SetActive(false);
            }

            // Set up all of the option views
            int optionViewsCreated = 0;

            for (int i = 0; i < dialogueOptions.Length; i++)
            {
                var optionView = optionViews[i];
                var option = dialogueOptions[i];

                if (option.IsAvailable == false && showUnavailableOptions == false)
                {
                    // Don't show this option.
                    continue;
                }

                optionView.gameObject.SetActive(true);

                optionView.Option = option;


                // Greyout if we alread
                if (optionGreyout.TryLine(option.Line.Text.Text))
                {
                    optionView.gameObject.GetComponent<TextMeshProUGUI>().color = Color.HSVToRGB(1f, 0f, 0.5f);
                }
                else
                {
                    optionView.gameObject.GetComponent<TextMeshProUGUI>().color = Color.white;
                }

                // metadata
                List<string> metaDataList = new List<string>();
                if (option.Line.Metadata != null)
                {
                    foreach (var metadata in option.Line.Metadata)
                    {
                        metaDataList.Add(metadata);
                    }
                }

                if (metaDataList.Contains("unlockable"))
                {
                    optionView.gameObject.GetComponent<TextMeshProUGUI>().color = unlockedLineColor;
                }


                // The first available option is selected by default
                if (optionViewsCreated == 0)
                {
                    optionView.Select();
                }

                optionViewsCreated += 1;
            }

            // Note the delegate to call when an option is selected
            OnOptionSelected = onOptionSelected;

            // Fade it all in
            StartCoroutine(Effects.FadeAlpha(canvasGroup, 0, 1, fadeTime));

            /// <summary>
            /// Creates and configures a new <see cref="OptionView"/>, and adds
            /// it to <see cref="optionViews"/>.
            /// </summary>
            OptionView CreateNewOptionView()
            {
                var optionView = Instantiate(optionViewPrefab);
                optionView.transform.SetParent(transform, false);
                optionView.transform.SetAsLastSibling();

                optionView.OnOptionSelected = OptionViewWasSelected;
                optionViews.Add(optionView);

                return optionView;
            }

            /// <summary>
            /// Called by <see cref="OptionView"/> objects.
            /// </summary>
            void OptionViewWasSelected(DialogueOption option)
            {
                StartCoroutine(OptionViewWasSelectedInternal(option));

                IEnumerator OptionViewWasSelectedInternal(DialogueOption selectedOption)
                {
                    yield return StartCoroutine(Effects.FadeAlpha(canvasGroup, 1, 0, fadeTime));
                    continueButton.SetActive(true);
                    foreach (var optionView in optionViews)
                    {
                        optionView.gameObject.SetActive(false);
                    }
                    OnOptionSelected(selectedOption.DialogueOptionID);
                }
            }
        }
    }
}
