using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ChatSequence chatSequence; 
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah yang nabrak adalah Player
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Kunci biar nggak ke-trigger dua kali
            
            // Panggil fungsi StartChatSequence yang ada di PhoneManager baru
            PhoneManager.Instance.StartChatSequence(chatSequence);
        }
    }
}