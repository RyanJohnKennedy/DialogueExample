using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueController : MonoBehaviour
{
    public Image dialogueBox;
    public Text conversationName;
    public Text conversationText;
    public Button nextButton;
    public Button choiceTemp;
    public GameObject canvas;
    public Color playerColour;
    private Color npcColour;
    private string npcName;

    private int[] currentLines;

    private bool talked = false;
    private LinkList dialogues;
    private List<Button> buttons = new List<Button>();

    public void StartConversation(string _name, TextAsset _dialogue, Color _npcColour)
    {
        currentLines = new int[1] { 1 };
        LinkList dialogues = new LinkList();
        dialogues = SplitText(_dialogue.text);

        dialogueBox.gameObject.SetActive(true);
        dialogueBox.color = _npcColour;
        npcColour = _npcColour;
        //conversationName.text = _name;
        npcName = _name;

        Speak();
    }

    void LeaveConversation()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    LinkList SplitText(string _dialogue)
    {
        string[] lines = _dialogue.Split('|');

        dialogues = new LinkList();

        for (int i = 0; i < lines.Length; i++)
        {
            Dialogue tempDialogue = JsonUtility.FromJson<Dialogue>(lines[i]);
            dialogues.Add(tempDialogue);
        }
        return dialogues;
    } 

    void Speak()
    {
        conversationText.text = "";

        foreach (Button b in buttons)
        {
            Destroy(b.gameObject);
        }
        buttons.Clear();
        int i = 0;
        foreach (int line in currentLines)
        {
            if (currentLines[0] != 0)
            {
                if (CheckInventory(dialogues.Find(line).m_data.reqInv))
                {
                    if (dialogues.Find(line).m_data.dialoguePlayer == "" || talked == true)
                    {
                        conversationName.text = npcName;
                        dialogueBox.color = npcColour;
                        conversationText.text = dialogues.Find(line).m_data.dialogueText;
                        nextButton.gameObject.SetActive(true);
                        talked = false;
                    }
                    else
                    {
                        conversationName.text = "You";
                        nextButton.gameObject.SetActive(false);
                        dialogueBox.color = playerColour;
                        SpawnButtons(line, i);
                    }
                    i++;
                }
            }
            else
            {
                LeaveConversation();
            }
        }
    }

    public void NextLineButton()
    {
        string[] nextLines = dialogues.Find(currentLines[0]).m_data.nextDialogues.Split('/');
        currentLines = new int[nextLines.Length];

        for (int i = 0; i < nextLines.Length; i++)
        {
            currentLines[i] = Convert.ToInt32(nextLines[i]);
        }
        
        Speak();
    }

    public void SpawnButtons(int option, int buttonNum)
    {
        Button temp = Instantiate(choiceTemp, canvas.transform);
        temp.gameObject.SetActive(true);
        temp.transform.position = new Vector3(200 + (buttonNum * 400), choiceTemp.transform.position.y, choiceTemp.transform.position.z);
        temp.onClick.AddListener(delegate { Choice(option); });
        temp.GetComponentInChildren<Text>().text = dialogues.Find(option).m_data.dialoguePlayer;
        buttons.Add(temp);
    }

    public void Choice(int option)
    {
        currentLines = new int[1];
        currentLines[0] = option;
        talked = true;
        Speak();
    }

    bool CheckInventory(string _item)
    {
        if(_item == "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
