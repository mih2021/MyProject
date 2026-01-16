using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] GameObject startWhite;
    [SerializeField] GameObject starYellow;

    public void SetWhite(bool active)
    {
        startWhite.SetActive(active);
        starYellow.SetActive(!active);
    }
}
