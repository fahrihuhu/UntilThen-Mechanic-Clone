using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Pola Singleton biar gampang dipanggil

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public TextMeshProUGUI nameText;
    public Image portraitImage;
    
    [Header("Layar Tamat")]
    public GameObject panelTamat;
    // MATERI WORKSHOP: Data Structure (Queue)
    private Queue<DialogueLine> sentences; 
    // Variabel untuk mengatur efek pengetikan
    private Coroutine typingCoroutine; 
    private DialogueLine currentLine; 
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;
        sentences = new Queue<DialogueLine>();
    }

    public void StartDialogue(DialogueLine[] lines)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();

        foreach (DialogueLine line in lines)
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
            dialogueText.text = currentLine.sentence; // Tampilkan kalimat lengkap
            isTyping = false;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = sentences.Dequeue();
        nameText.text = currentLine.characterName;
        
        // Update UI Wajah (kalau gambarnya dikosongin di Inspector, hilangkan kotak fotonya)
        if (currentLine.characterPortrait != null)
        {
            portraitImage.sprite = currentLine.characterPortrait;
            portraitImage.gameObject.SetActive(true);
        }
        else
        {
            portraitImage.gameObject.SetActive(false);
        }

        // Hentikan efek ngetik sebelumnya (jika ada) untuk mencegah teks bertumpuk
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        
        // Mulai efek ngetik huruf demi huruf
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
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
        
        // Cek kalau yang ngomong Salma, munculin layar hitam
        if (currentLine != null && currentLine.characterName == "Salma")
        {
            panelTamat.SetActive(true); 
        }
    }
}