using UnityEngine;

[System.Serializable]
public class ChatMessage
{
    public string senderName; 
    public bool isPlayer; 
    [TextArea(2, 5)] public string message;
}

[System.Serializable]
public class ChatSequence
{
    public ChatMessage[] messages; 
    public bool requirePlayerReply; 
    
    [Header("Opsi 1")]
    public string choice1Text; 
    [Tooltip("Isi angka urutan lanjutannya. Biarkan -1 kalau chat tamat.")]
    public int nextSequence1 = -1; 
    
    [Header("Opsi 2")]
    public string choice2Text;
    [Tooltip("Isi angka urutan lanjutannya. Biarkan -1 kalau chat tamat.")]
    public int nextSequence2 = -1; 
}