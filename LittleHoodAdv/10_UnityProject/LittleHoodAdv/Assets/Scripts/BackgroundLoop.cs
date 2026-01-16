using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform bg1;
    [SerializeField] private Transform bg2;

    private float bgWidth;

    private void Start()
    {
        bgWidth = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void LateUpdate()
    {
        // ‰E•ûŒü
        if (cameraTransform.position.x - bg1.position.x > bgWidth)
        {
            MoveToRight(bg1);
        }
        else if (bg1.position.x - cameraTransform.position.x > bgWidth)
        {
            MoveToLeft(bg1);
        }

        // bg2
        if (cameraTransform.position.x - bg2.position.x > bgWidth)
        {
            MoveToRight(bg2);
        }
        else if (bg2.position.x - cameraTransform.position.x > bgWidth)
        {
            MoveToLeft(bg2);
        }
    }

    private void MoveToRight(Transform bg)
    {
        Transform other = (bg == bg1) ? bg2 : bg1;

        bg.position = new Vector3(
            other.position.x + bgWidth,
            bg.position.y,
            bg.position.z
        );
    }

    private void MoveToLeft(Transform bg)
    {
        Transform other = (bg == bg1) ? bg2 : bg1;

        bg.position = new Vector3(
            other.position.x - bgWidth,
            bg.position.y,
            bg.position.z
        );
    }
}