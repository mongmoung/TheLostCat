using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageTXT : MonoBehaviour
{
    public Image messageImage;
    public TextMeshProUGUI messageText;

    private Dictionary<int, string> messages = new Dictionary<int, string>();
    public Canvas canvas;

    void Awake()
    {
        SetMessage();
    }

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
    }

    void SetMessage()
    {
        messages.Add(0, "Help me..");
        messages.Add(1, "Find the key!");
        messages.Add(2, "run away" +System.Environment.NewLine+"He's coming");
        messages.Add(3, "Bye, my little cat");
    }

    public string GetMessage(int id)
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        return messages[id];
    }

   
}