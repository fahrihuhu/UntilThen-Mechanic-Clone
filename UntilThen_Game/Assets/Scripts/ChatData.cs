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
    public string choice1Text; 
    public string choice2Text;
}