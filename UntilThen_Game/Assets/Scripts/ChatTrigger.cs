using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    [Header("Daftar Semua Rute Chat")]
    public ChatSequence[] allSequences; 
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; 
            PhoneManager.Instance.StartChatSequence(allSequences);
        }
    }
}