using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 画面外でも行動する
    /// </summary>
    
    public bool nonVisibleAct = false;
    /// <summary>
    /// スピード
    /// </summary>
    public float speed = 5;
    /// <summary>
    /// 重力
    /// </summary>
    public float gravity = 0;
    /// <summary>
    /// 接触判定
    /// </summary>
    public EnemyCollisionCheck checkCollision = null;

    private SpriteRenderer sr = null;
    private Rigidbody2D rb = null;
    private bool rightLeftF = false;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool isDead = false;
    private Animator anim = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float lScale = 0.5f;
        if (!oc.playerStepOn)
        {
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkCollision.isOn)
                {
                    rightLeftF = !rightLeftF;
                }
                int xVector = -1;
                if (rightLeftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-lScale, lScale, lScale);
                }
                else
                {
                    transform.localScale = new Vector3(lScale, lScale, lScale);
                }
                rb.linearVelocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
            }
        }
        else
        {
            if (!isDead)
            {
                anim.Play("enemy_down");
                rb.linearVelocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                Destroy(gameObject, 3f);
            } else
            {
                transform.Rotate(new Vector3(0,0,5));
            }
        }
    }
}
