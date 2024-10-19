using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDialog : MonoBehaviour
{

    [SerializeField]
    TextAsset dialog;
    [SerializeField]
    TextAssetEvent callDialogRunnerEvent;

    public void DialogInteract()
    {


        callDialogRunnerEvent.Raise(dialog);



    }

}
