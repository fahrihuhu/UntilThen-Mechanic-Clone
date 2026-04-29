using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;

    public GameObject phonePanel;
    public Transform chatContentArea; 
    
    [Header("Prefabs")]
    public GameObject npcBubblePrefab;
    public GameObject playerBubblePrefab;

    [Header("Reply UI")]
    public GameObject replyPanel; // Panel penampung tombol di bawah
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;

    private void Awake() => Instance = this;

    private void Start()
    {
        replyPanel.SetActive(false);
    }

    // Fungsi ini yang dipanggil oleh Trigger di jalan nanti
    public void StartChatSequence(ChatSequence sequence)
    {
        if (!phonePanel.activeSelf) phonePanel.SetActive(true);
        replyPanel.SetActive(false); // Sembunyikan tombol saat chat masuk
        StartCoroutine(PlayChatSequence(sequence));
    }

    IEnumerator PlayChatSequence(ChatSequence sequence)
    {
        // Munculkan chat satu per satu dengan jeda
        foreach (ChatMessage chat in sequence.messages)
        {
            SpawnBubble(chat);
            yield return new WaitForSeconds(1.5f); // Jeda tiap chat masuk (bisa disesuaikan)
        }

        // Kalau ada pilihan jawaban, munculkan tombolnya
        if (sequence.requirePlayerReply)
        {
            choice1Text.text = sequence.choice1Text;
            choice2Text.text = sequence.choice2Text;
            replyPanel.SetActive(true);
        }
    }

    private void SpawnBubble(ChatMessage chat)
    {
        GameObject bubblePrefab = chat.isPlayer ? playerBubblePrefab : npcBubblePrefab;
        GameObject newBubble = Instantiate(bubblePrefab, chatContentArea);

        // Cari semua teks di dalam bubble yang baru di-spawn
        TextMeshProUGUI[] texts = newBubble.GetComponentsInChildren<TextMeshProUGUI>();
        
        if (chat.isPlayer)
        {
            // Player cuma punya 1 teks (Message)
            texts[0].text = chat.message;
        }
        else
        {
            // NPC punya 2 teks (Name dan Message)
            texts[0].text = chat.senderName;
            texts[1].text = chat.message;
        }

        Canvas.ForceUpdateCanvases();
    }

    // Fungsi untuk tombol pilihan 1
    public void ChooseOption1()
    {
        replyPanel.SetActive(false);
        // Spawn chat balasan kita sendiri warna biru
        SpawnBubble(new ChatMessage { isPlayer = true, message = choice1Text.text });
    }

    // Fungsi untuk tombol pilihan 2
    public void ChooseOption2()
    {
        replyPanel.SetActive(false);
        SpawnBubble(new ChatMessage { isPlayer = true, message = choice2Text.text });
    }
}