using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider2D leftWall;
    [SerializeField] private BoxCollider2D rightWall;

    [SerializeField] private float offsetX = 2.5f;
    [SerializeField] private float followSpeed = 0.15f;

    private float fixedY;
    private float camHalfWidth;

    private void Start()
    {
        // Y固定
        fixedY = transform.position.y;

        // カメラ半分幅（論理値）
        Camera cam = GetComponent<Camera>();
        camHalfWidth = cam.orthographicSize * cam.aspect;
    }

    private void LateUpdate()
    {
        // 追従したいX
        float targetX = player.position.x + offsetX;

        // 壁Collider基準で制限
        float leftLimit = leftWall.bounds.max.x + camHalfWidth;
        float rightLimit = rightWall.bounds.min.x - camHalfWidth;

        targetX = Mathf.Clamp(targetX, leftLimit, rightLimit);

        // なめらか追従（Pixel Perfect が最終補正）
        Vector3 targetPos = new Vector3(
            targetX,
            fixedY,
            transform.position.z
        );

        transform.position = targetPos;

        //transform.position = Vector3.Lerp(
        //    transform.position,
        //    targetPos,
        //    followSpeed
        //);
    }
}