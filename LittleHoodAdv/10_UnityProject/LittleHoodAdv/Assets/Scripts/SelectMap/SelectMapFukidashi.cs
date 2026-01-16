using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMapFukidashi : MonoBehaviour
{
    [SerializeField] private string stage = null;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        switch (stage)
        {
            case "Stage1":
                // ‰ï˜b•¶‚Ìì¬
                SceneManager.LoadScene(stage);
                break;
            case "Stage2":
                // ‰ï˜b•¶‚Ìì¬
                SceneManager.LoadScene(stage);
                break;
            case "Stage3":
                // ‰ï˜b•¶‚Ìì¬
                SceneManager.LoadScene(stage);
                break;
        }
    }

}
