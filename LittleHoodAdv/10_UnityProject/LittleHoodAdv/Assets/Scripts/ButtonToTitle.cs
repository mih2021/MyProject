using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonToTitle : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickTitle);
    }

    private void OnClickTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
