using UnityEngine;

// System.Serializable wajib ada agar struktur ini muncul di Inspector Unity
[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public Sprite characterPortrait; // Gambar wajah
    
    [TextArea(3, 10)]
    public string sentence; // Teks percakapan
}