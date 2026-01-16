using UnityEngine;

public class FollowTargetUI : MonoBehaviour
{
    public Transform target;      // Player
    public Vector3 offset;        // ì™ÇÃè„Ç»Ç«
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos =
            cam.WorldToScreenPoint(target.position + offset);

        transform.position = screenPos;
    }
}