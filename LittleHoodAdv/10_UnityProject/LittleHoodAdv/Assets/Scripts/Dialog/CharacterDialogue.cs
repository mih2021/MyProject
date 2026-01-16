using UnityEngine;
using TMPro;

public class CharacterDialogue : MonoBehaviour
{
    public GameObject speechBubble;
    public TMP_Text text;

    void Awake()
    {
        speechBubble.SetActive(false);
    }

    public void Speak(string message)
    {
        speechBubble.transform.parent.gameObject.SetActive(true);
        speechBubble.SetActive(true);
        text.text = message;
    }

    public void Hide()
    {
        speechBubble.SetActive(false);
    }
}

