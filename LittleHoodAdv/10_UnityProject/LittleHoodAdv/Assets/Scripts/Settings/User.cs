using UnityEngine;

public class User
{
    public string userName;
    public bool tutorialCleared;
    public bool stage1Cleared;
    public bool stage2Cleared;
    public bool stage3Cleared;

    public User(string name)
    {
        userName = name;
        tutorialCleared = false;
        stage1Cleared = false;
        stage2Cleared = false;
        stage3Cleared = false;
    }
}
