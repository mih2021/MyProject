using UnityEngine;

public class SelectMapTrigger : MonoBehaviour
{
    [SerializeField] private SelectMapFukidashi fukidashi = default;
    /// <summary>
    /// 前提ステージをクリアしていなかったら表示しない
    /// </summary>
    [SerializeField] private string preStage = "";

    private string playerTag = "Player";
    private bool isTrigger = false;

    private void Start()
    {
        fukidashi.gameObject.SetActive(false);
    }
    public bool IsTrigger()
    {
        return isTrigger;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(preStage != "" && PlayerPrefs.GetInt(preStage) == 0)
        {
            return;
        }
        if(collision.tag == playerTag)
        {
            fukidashi.gameObject.SetActive(true);
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(preStage != "" && PlayerPrefs.GetInt(preStage) == 0)
        {
            return;
        }
        if (collision.tag == playerTag)
        {
            fukidashi.gameObject.SetActive(false);
            isTrigger = false;
        }
       
    }
}
