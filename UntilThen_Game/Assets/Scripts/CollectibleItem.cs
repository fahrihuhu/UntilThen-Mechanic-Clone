using UnityEngine;
using UnityEngine.Events; // Wajib buat Unity Event

// INHERITANCE: Skrip ini adalah "Anak" dari InteractableObject
public class CollectibleItem : InteractableObject 
{
    [Header("Event Saat Dipungut")]
    public UnityEvent OnPickUp; // UNITY EVENT

    // POLYMORPHISM: Ngerombak cara kerja tombol 'E' khusus buat barang
    public override void Interact()
    {
        // Jalankan event di Inspector (misal: mutar sound effect)
        OnPickUp?.Invoke(); 

        // Munculin dialog kayak biasa (mewarisi sifat bapaknya)
        DialogueManager.Instance.StartDialogue(dialogueLines);

        // Hancurkan barangnya dari map (biar seolah-olah masuk tas)
        Destroy(gameObject); 
        HidePrompt(); 
    }
}