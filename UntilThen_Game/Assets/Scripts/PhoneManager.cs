using System.Collections;
using UnityEngine;
using TMPro;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;

    [Header("UI Utama")]
    public GameObject phonePanel;
    public Transform chatContentArea; 
    public TextMeshProUGUI contactNameDisplay; 
    
    [Header("Prefabs")]
    public GameObject npcBubblePrefab;
    public GameObject playerBubblePrefab;

    [Header("Reply UI (Panel Bawah)")]
    public GameObject replyPanel; 
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;

    private ChatSequence[] currentAllSequences;
    private int currentIndex = 0; // Buat ngelacak rute mana yang lagi aktif

    private void Awake() => Instance = this;

    private void Start()
    {
        phonePanel.SetActive(false);
        replyPanel.SetActive(false);
    }

    public void StartChatSequence(ChatSequence[] sequences)
    {
        currentAllSequences = sequences;
        currentIndex = 0; // Selalu mulai dari rute index 0
        
        phonePanel.SetActive(true);
        replyPanel.SetActive(false); 

        foreach (Transform child in chatContentArea)
        {
            Destroy(child.gameObject);
        }

        StartCoroutine(PlayChatSequence(currentAllSequences[currentIndex]));
    }

    IEnumerator PlayChatSequence(ChatSequence sequence)
    {
        // Cari nama buat di Header
        foreach (ChatMessage chat in sequence.messages)
        {
            if (!chat.isPlayer)
            {
                contactNameDisplay.text = chat.senderName;
                break; 
            }
        }

        foreach (ChatMessage chat in sequence.messages)
        {
            SpawnBubble(chat.isPlayer, chat.senderName, chat.message);
            yield return new WaitForSeconds(1.5f); 
        }

        if (sequence.requirePlayerReply)
        {
            choice1Text.text = sequence.choice1Text;
            choice2Text.text = sequence.choice2Text;
            replyPanel.SetActive(true);
            Canvas.ForceUpdateCanvases();
        }
    }

    private void SpawnBubble(bool isPlayer, string sender, string messageText)
    {
        GameObject prefabToSpawn = isPlayer ? playerBubblePrefab : npcBubblePrefab;
        GameObject newBubble = Instantiate(prefabToSpawn, chatContentArea);

        TextMeshProUGUI[] texts = newBubble.GetComponentsInChildren<TextMeshProUGUI>();
        
        if (texts.Length > 0)
        {
            texts[0].text = messageText;
        }

        Canvas.ForceUpdateCanvases();
    }

    public void ChooseOption1() 
    { 
        replyPanel.SetActive(false);
        SpawnBubble(true, "Player", currentAllSequences[currentIndex].choice1Text);
        
        int nextIndex = currentAllSequences[currentIndex].nextSequence1;
        if (nextIndex >= 0 && nextIndex < currentAllSequences.Length)
        {
            StartCoroutine(WaitAndPlayNext(nextIndex));
        }
    }

    public void ChooseOption2() 
    { 
        replyPanel.SetActive(false);
        SpawnBubble(true, "Player", currentAllSequences[currentIndex].choice2Text);
        
        int nextIndex = currentAllSequences[currentIndex].nextSequence2;
        if (nextIndex >= 0 && nextIndex < currentAllSequences.Length)
        {
            StartCoroutine(WaitAndPlayNext(nextIndex));
        }
    }

    IEnumerator WaitAndPlayNext(int index)
    {
        yield return new WaitForSeconds(1.0f);
        currentIndex = index; // Update index ke rute cabang
        StartCoroutine(PlayChatSequence(currentAllSequences[currentIndex]));
    }

    public void ClosePhone()
    {
        phonePanel.SetActive(false);
    }
}