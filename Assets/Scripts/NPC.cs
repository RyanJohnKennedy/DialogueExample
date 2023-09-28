using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public TextAsset dialogue;
    public string npcName;
    public Color npcColour;

    private DialogueController DC;

    // Start is called before the first frame update
    void Start()
    {
        DC = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        DC.StartConversation(npcName, dialogue, npcColour);
    }
}
