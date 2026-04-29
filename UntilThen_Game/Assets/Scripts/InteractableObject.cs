using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable 
{
    //[TextArea(3, 10)]
    public DialogueLine[] dialogueLines;
    
    // Variabel untuk menampung ikon interaksi
    public GameObject promptIcon; 

    private void Start()
    {
        // Pastikan ikonnya hilang saat game baru mulai
        if (promptIcon != null) promptIcon.SetActive(false);
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines);
        HidePrompt(); // Sembunyikan ikon saat obrolan dimulai
    }

    public void ShowPrompt()
    {
        if (promptIcon != null) promptIcon.SetActive(true);
    }

    public void HidePrompt()
    {
        if (promptIcon != null) promptIcon.SetActive(false);
    }
}