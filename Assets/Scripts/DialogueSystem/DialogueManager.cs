using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI dialogueArena;

    private Queue<DialogueLine> lines;
    public bool isDialogueActive = false;
    public float tyingspeed = 0.2f;
    public Animator animator;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        if (lines == null)
        { lines = new Queue<DialogueLine>(); }
 
    }

    public void StartDialogue(Dialogue dialoge)
    {
        
        isDialogueActive = true;
        animator.Play("show");
        lines.Clear();
        foreach (DialogueLine dialogeline in dialoge.dialogueLines)
        {
            lines.Enqueue(dialogeline);
        }
        DisplayNextDialogueLine();

    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        currentLine.onLineStart?.Invoke();

        charName.text = currentLine.character.name;
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArena.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArena.text += letter;
            yield return new WaitForSeconds(tyingspeed); 
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
        DialogueTrigger.IsInDialogue = false;

    }
}
