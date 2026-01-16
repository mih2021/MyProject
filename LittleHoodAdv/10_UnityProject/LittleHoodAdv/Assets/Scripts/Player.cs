using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    #region//パブリック変数
    public float speed = 1.0f;
    public float gravity = 1.0f;
    public float jumpSpeed = 1.0f;
    public float jumpHeight = 2.0f;
    public float jumpLimitTime = 0.0f;
    public GroundCheck ground;
    public GroundCheck head;
    public ContinuePanelController _continuePanel = null;

    public AnimationCurve dashCurve;
    public AnimationCurve jumpCurve;

    [Header("踏みつけ判定の高さの割合")] public float stepOnRate;

    
    #endregion

    #region//プライベート変数

    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private bool isJump = false;
    private bool isRun = false;
    private float jumpPos = 0.0f;
    private float otherJumpHeight;
    private bool isOtherJump = false;
    private bool isHead = false;
    private float dashTime, jumpTime;
    private float beforeKey;

    private bool isGround = false;
    private bool isPlay = true;

    private string enemyTag = "Enemy";
    private string deadAreaTag = "DeadArea";

    bool wasGround;
    float prevVerticalKey;
    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();

    }

    public void setIsPlay(bool isPlay)
    {
        this.isPlay = isPlay;
        if (!this.isPlay)
        {
            isRun =false;
            isJump=false;
            isGround=true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 接地判定
        wasGround = isGround;
        isGround = ground.IsGround();
        isHead = head.IsGround();

        // 着地した瞬間にリセット
        if (!wasGround && isGround)
        {
            isJump = false;
            isOtherJump = false;
            jumpTime = 0.0f;
        }

        float xSpeed = GetXSpeed();
        float ySpeed = GetYSpeed();

        SetAnimation();
        rb.linearVelocity = new Vector2(xSpeed, ySpeed);
    }
    float GetVerticalInput()
    {
        if (!isPlay) return 0.0f;
        return Input.GetAxis("Vertical");
    }

    float GetHorizontalInput()
    {
        if (!isPlay) return 0.0f;
        return Input.GetAxis("Horizontal");
    }

    /// <summary>
    /// Y成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>Y軸の速さ</returns>
    private float GetYSpeed()
    {
        float verticalKey = GetVerticalInput();

        // Axisの立ち上がり検出
        bool jumpDown = verticalKey > 0 && prevVerticalKey <= 0;
        bool jumpHold = verticalKey > 0;

        float ySpeed = -gravity;
        
        // 他ジャンプ（踏みつけ等）
        if (isOtherJump)
        {
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if (canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isOtherJump = false;
                jumpTime = 0.0f;
            }
        }
        // 地面にいるとき（ジャンプ開始）
        else if (isGround && jumpDown)
        {
            ySpeed = jumpSpeed;
            jumpPos = transform.position.y;
            isJump = true;
            jumpTime = 0.0f;
            AudioManager.Instance.PlaySeClick();
        }
        // 通常ジャンプ中（長押し）
        else if (isJump)
        {
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if (jumpHold && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }

        // ジャンプカーブ適用
        if (isJump || isOtherJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        // 前フレーム保存（最後に必ず）
        prevVerticalKey = verticalKey;

        return ySpeed;
    }

    /// <summary>
    /// X成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>X軸の速さ</returns>
    private float GetXSpeed()
    {
        float horizontalKey = GetHorizontalInput();
        float xSpeed = 0.0f;
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }

        //前回の入力からダッシュの反転を判断して速度を変える
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;

        xSpeed *= dashCurve.Evaluate(dashTime);
        beforeKey = horizontalKey;
        return xSpeed;
    }
    /// <summary>
    /// アニメーションを設定する
    /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump || isOtherJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }
    #region/接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == enemyTag)
        {
            float stepOnHeight = (capcol.size.y * stepOnRate / 100f);
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;
            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {
                    // ヒットした場所がふみつけの場合、敵をたおす
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision>();
                    if (o != null)
                    {
                        otherJumpHeight = o.boundHeight;
                        o.playerStepOn = true;
                        jumpPos = transform.position.y;
                        isOtherJump = true;
                        AudioManager.Instance.PlaySeClick();
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                    else
                    {
                        Debug.Log("ObjectCollision nothing");
                    }
                }
                else
                {
                    anim.Play("player_damage");
                    isPlay = false;
                    StartCoroutine(AfterDownEvent());
                    break;
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == deadAreaTag)
        {
            anim.Play("player_damage");
            isPlay = false;
            gravity = 0.0f;
            StartCoroutine(AfterDownEvent());
        }
    }

    IEnumerator AfterDownEvent()
    {
        yield return new WaitForSeconds(1f);
        if (_continuePanel != null)
        {
            _continuePanel.gameObject.SetActive(true);
            AudioManager.Instance.PlayBgmGameOver();
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AudioManager.Instance.PlayBgmMain();
        }
    }
    #endregion
}
