using UnityEngine;
using System.Collections.Generic;
using System;

public class DialogueManager : MonoBehaviour
{
    public DialogueCSVLoader loader;

    List<DialogueLine> lines;
    int index;

    [SerializeField]
    List<CharacterDialogue> characters;   // Inspector‚Å“o˜^

    Dictionary<string, CharacterDialogue> characterMap;

    Action onFinish;

    void Awake()
    {
        characterMap = new Dictionary<string, CharacterDialogue>();

        foreach (var c in characters)
        {
            if (c == null) continue;

            // GameObject–¼‚ð speaker ID ‚Æ‚µ‚ÄŽg‚¤
            characterMap[c.gameObject.name] = c;
        }
    }

    public void StartDialogue(string eventId, Action onFinish = null)
    {
        this.onFinish = onFinish;

        lines = loader.Load(eventId);
        index = 0;
        ShowLine();
    }

    void Update()
    {
        if (lines == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Next();
        }
    }

    void ShowLine()
    {
        
        foreach (var c in characterMap.Values)
        {
            c.Hide();
        }

        var line = lines[index];

        if (characterMap.TryGetValue(line.speaker, out var character))
        {
            character.Speak(line.text);
        }
        else
        {
            Debug.LogWarning($"Character not found: {line.speaker}");
        }
    }

    void Next()
    {
        index++;

        if (index >= lines.Count)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        foreach (var c in characterMap.Values)
        {
            c.Hide();
        }
           

        lines = null;

        onFinish?.Invoke();
    }
}
