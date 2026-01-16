using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("プレイヤーがはねる高さ")] public float boundHeight;

    /// <summary>
    /// プレイヤーがふんだかどうか
    /// </summary>
    [HideInInspector] public bool playerStepOn;

}
