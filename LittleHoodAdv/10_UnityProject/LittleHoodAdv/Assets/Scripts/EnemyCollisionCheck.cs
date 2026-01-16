using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCollisionCheck : MonoBehaviour
{
    /// <summary>
    /// ”»’è“à‚É“G‚©•Ç‚ª‚ ‚é
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private string groundTag = "Ground";
    private string enemyTag = "Enemy";

    /// <summary>
    /// ÚG”»’è
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag ||  collision.tag == enemyTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag == enemyTag)
        {
            isOn = false;
        }
    }
}
