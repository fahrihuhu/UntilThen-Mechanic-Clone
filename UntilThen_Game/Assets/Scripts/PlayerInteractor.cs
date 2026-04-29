using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        // Tekan 'E' untuk interaksi, ATAU tekan 'E' untuk lanjut ke teks berikutnya
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueManager.Instance.dialoguePanel.activeSelf)
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
            else if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null) currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null) currentInteractable = null;
    }
}