using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningGameManager : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Player player;

    [SerializeField] GameObject _clearText;

    /// <summary>
    /// Event ID for the initial conversation
    /// </summary>
    private string openingEventId = "event00";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        callEvent(openingEventId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callEvent(string eventId)
    {
        switch (eventId)
        {
            case "event00":
                player.setIsPlay(false);
                dialogueManager.StartDialogue(eventId, () =>
                {
                    player.setIsPlay(true);
                });

                break;
            case "event01":
                player.setIsPlay(false);
                dialogueManager.StartDialogue(eventId, () =>
                {
                    StartCoroutine(AfterDialogueEvent());
                });

                break;
            case "event02":
                break;
            default:
                break;

        }
    }

    IEnumerator AfterDialogueEvent()
    {
        _clearText.SetActive(true);
        player.setIsPlay(true);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SelectMap");
    }
}
