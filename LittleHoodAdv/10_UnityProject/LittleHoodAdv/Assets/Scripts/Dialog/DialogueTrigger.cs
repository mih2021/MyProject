using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    [SerializeField] GameManager gameManager;
    [SerializeField] string eventId = "event01";
    //[SerializeField] bool triggerOnce = true;

    //bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (triggerOnce && triggered) return;
        if (!other.CompareTag("Player")) return;

        //triggered = true;
        gameManager.callEvent(eventId);
    }
}
