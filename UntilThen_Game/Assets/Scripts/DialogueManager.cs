using System.Collections;
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
    // Variabel untuk mengatur efek pengetikan
    private Coroutine typingCoroutine; 
    private string currentSentence; 
    private bool isTyping = false;
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
        // Kalau teks sedang berjalan dan pemain klik/enter, langsung munculkan teks utuh (Skip)
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue(); 
        
        // Hentikan efek ngetik sebelumnya (jika ada) untuk mencegah teks bertumpuk
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        
        // Mulai efek ngetik huruf demi huruf
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }
    // Typewriter Effect
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = ""; // Kosongkan layar dulu
        
        // Pecah kalimat menjadi susunan huruf dan tampilkan satu per satu
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f); // Kecepatan ngetik (makin kecil makin cepat)
        }
        isTyping = false; // Tandai selesai ngetik
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}