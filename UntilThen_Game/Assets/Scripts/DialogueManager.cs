using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Pola Singleton biar gampang dipanggil

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    // MATERI WORKSHOP: Data Structure (Queue)
    private Queue<string> sentences; 

    void Awake()
    {
        Instance = this;
        sentences = new Queue<string>();
    }

    public void StartDialogue(string[] lines)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line); // Masukkan kalimat ke dalam antrian
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Ambil dan hapus kalimat pertama dari antrian
        string sentence = sentences.Dequeue(); 
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}