using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue
{
    public int dialogueID;
    public string dialoguePlayer;
    public string dialogueText;
    public string reqInv;
    public string nextDialogues;

    public Dialogue(int _dialogueID, string _dialoguePlayer, string _dialogueText, string _reqInv, string _nextDialogues)
    {
        dialogueID = _dialogueID;
        dialoguePlayer = _dialoguePlayer;
        dialogueText = _dialogueText;
        reqInv = _reqInv;
        nextDialogues = _nextDialogues;
    }
}
