using UnityEngine;

public class UserManager : MonoBehaviour
{
    public static UserManager Instance { get; private set; }

    public User user;

    private const string SaveKey = "USER_DATA";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void CreateNewUser(string userName)
    {
        user = new User(userName);
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(user);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            user = null;
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
        user = null;
    }
    public bool CheckUser()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
