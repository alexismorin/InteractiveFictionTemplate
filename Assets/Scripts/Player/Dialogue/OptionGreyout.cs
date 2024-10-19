using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionGreyout : MonoBehaviour
{

    List<string> lines = new List<string>();

    public void StartDialogue()
    {
        lines = new List<string>();
    }

    public void AddLine(string line)
    {
        lines.Add(line);
    }


    public bool TryLine(string line)
    {
        if (lines.Contains(line))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
