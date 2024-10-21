using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class MessageHandler : MonoBehaviour
{
    public static MessageHandler Instance { get; private set; }
    public TMP_Text messageText;

    private Queue<(string, Color?)> messageQueue = new Queue<(string, Color?)>();
    private bool isDisplayingMessage = false;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateText(string message, Color? color = null)
    {
        messageQueue.Enqueue((message, color));
        if (!isDisplayingMessage)
        {
            StartCoroutine(DisplayMessages());
        }
    }

    private IEnumerator DisplayMessages()
    {
        isDisplayingMessage = true;

        while (messageQueue.Count > 0)
        {
            var (message, color) = messageQueue.Dequeue();
            messageText.gameObject.SetActive(true);
            messageText.text = message;
            messageText.color = color ?? Color.white;

            // Wait for the message to be fully visible and then start fading out
            yield return messageText.DOFade(1f, 0.2f).WaitForCompletion(); // Optional fade-in
            yield return new WaitForSeconds(2f); // Display message for 2 seconds before fading out
            yield return messageText.DOFade(0f, 1f).WaitForCompletion();

            messageText.gameObject.SetActive(false);
            messageText.color = Color.white;
        }

        isDisplayingMessage = false;
    }
}


