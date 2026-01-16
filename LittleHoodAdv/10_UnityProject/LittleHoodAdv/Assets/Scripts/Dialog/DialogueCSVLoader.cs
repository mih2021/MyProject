using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueCSVLoader : MonoBehaviour
{
    public TextAsset csvFile;

    private bool isDebug = false;

    public List<DialogueLine> Load(string eventId)
    {
        var list = new List<DialogueLine>();
        var lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var cols = lines[i].Split(',');

            if (cols[0] != eventId) continue;

            list.Add(new DialogueLine
            {
                eventId = cols[0],
                line = int.Parse(cols[1]),
                speaker = cols[2],
                text = cols[3]
            });
        }

        if(isDebug)
        {
            Debug.Log($"Dialogue Loaded: {list.Count} lines");

            foreach (var l in list)
            {
                Debug.Log($"[{l.eventId}] {l.line} {l.speaker} : {l.text}");
            }
        }
        

        return list;
    }
}