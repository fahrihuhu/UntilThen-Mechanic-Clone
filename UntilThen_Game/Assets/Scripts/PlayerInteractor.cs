using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        // Tekan 'E' untuk interaksi, ATAU tekan 'E' untuk lanjut ke teks berikutnya
        if (DialogueManager.Instance.dialoguePanel.activeSelf)
        {
            // Lanjut dialog pakai Enter (Return) ATAU Klik Kiri Mouse (0)
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
            return; // Hentikan kode di sini agar pemain tidak bisa memicu interaksi lain saat ngobrol
        }

        // 2. Kondisi saat sedang jalan-jalan dan ingin berinteraksi
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null) 
        {
            currentInteractable = interactable;
            currentInteractable.ShowPrompt(); // Suruh ikonnya muncul
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null) 
        {
            interactable.HidePrompt(); // Suruh ikonnya hilang
            if (currentInteractable == interactable) currentInteractable = null;
        }
    }
}