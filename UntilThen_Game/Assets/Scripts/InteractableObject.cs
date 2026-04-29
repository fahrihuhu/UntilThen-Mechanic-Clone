using UnityEngine;

// MATERI WORKSHOP: Interface (IInteractable wajib punya void Interact)
public class InteractableObject : MonoBehaviour, IInteractable 
{
    [TextArea(3, 10)]
    public string[] dialogueLines; // Teks yang bisa diisi dari Inspector

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines);
    }
}