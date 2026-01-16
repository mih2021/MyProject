using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuePanelController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
    }
    /// <summary>
    /// リトライボタン
    /// </summary>
    public void OnClickRetry()
    {
        AudioManager.Instance.PlayBgmMain();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ステージ選択ボタン
    /// </summary>
    public void OnClickQuit()
    {
        AudioManager.Instance.PlayBgmMain();
        SceneManager.LoadScene("SelectMap");
    }
}
