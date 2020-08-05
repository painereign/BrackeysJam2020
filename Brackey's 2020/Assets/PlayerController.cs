using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    public int Health = 100;
    bool damageImune = false;
    float immuneTimer = 0.5f;
    float currentImmuneTime = 0f;

    public bool Falling;
    public float FallThresh;

    public float speed;
    public float jumpForce;
    public float moveInput;

    public Rigidbody2D rb;

    private bool facingRight = true;

    //public bool isGrounded;

    public IsGrounded IsGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatIsground;

    private float jumpTimeCounter;
    public float jumpTime;

    private bool isJumping = false;

    public bool UseBetterJump = false;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    private Vector2 lastPos;

    public GameObject Bullet;

    private float heldDownTime = 0f;
    public float timeBetweenHeldShots = 0.5f;

    public float SprintMultiplier = 1.5f;

    private float sprintVal = 1f;

    public Animator Anim;

    private bool lastFrameOnGround = false;

    public Vector2 LastVelocityChange;

    float horMoveAdjustMult = 1f;

    float HeightModPerFrame = 0f;

    bool modIsWater = false;

    public float SandWaterSprintHeightIncrease = 0.1f;

    public enum PlayerState
    {
        Normal,
        Crouched,
        MoprhBall,
        MorphBallJump,
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckSprint();
        OriginalJump();

        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, WhatIsground);

        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed * sprintVal * horMoveAdjustMult, rb.velocity.y);

        if (HeightModPerFrame != 1f)
        {
            if (!modIsWater)
            {
                IsGrounded.OnGround = true;
            }
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            if (sprintVal == 1f)
                transform.position = new Vector3(transform.position.x, transform.position.y - HeightModPerFrame);
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + SandWaterSprintHeightIncrease);
            }
        }
        else
        {
            //IsGrounded.OnGround = false;
            rb.gravityScale = 1f;
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        } else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (lastPos == new Vector2(transform.position.x, transform.position.y))
        {
            //Debug.Log("NOT MOVING");
        }

        CheckShoot();

        if (IsGrounded.OnGround && ! lastFrameOnGround)
        {
            Anim.SetTrigger("Squish");
        }

        if (lastPos.y > (transform.position.y + FallThresh))
        {
            Falling = true;
        }
        else
        {
            Falling = false;
        }



        lastPos = transform.position;
        lastFrameOnGround = IsGrounded.OnGround;

        LastVelocityChange = rb.velocity;

        CheckImmune();
    }

    public void CheckImmune()
    {
        if (damageImune)
        {
            currentImmuneTime -= Time.deltaTime;
            if (currentImmuneTime < 0)
            {
                currentImmuneTime = immuneTimer;
                damageImune = false;
            }
        }
    }

    public void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintVal = SprintMultiplier;
        }
        else
        {
            sprintVal = 1f;
        }
    }

    public void HoriMoveAjust(float val)
    {
        horMoveAdjustMult = val;
    }

    public void HeighModPerFrame(float val, bool isWater)
    {
        HeightModPerFrame = val;
        modIsWater = isWater;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;

        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void OriginalJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded.OnGround)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;

            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                //Debug.Log("Jump counter: " + jumpTimeCounter);
                rb.velocity = Vector2.up * jumpForce;
                //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                jumpTimeCounter -= Time.deltaTime;

            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void CheckShoot()
    {
        if (Input.GetMouseButton(0))
        {
            heldDownTime -= Time.deltaTime;
            if (heldDownTime <= 0)
            {
                Shoot();
                heldDownTime = timeBetweenHeldShots;
            }
        }
        else
        {
            heldDownTime = 0f;
        }
    }

    public void Shoot()
    {
        GameObject go = GameObject.Instantiate(Bullet, this.transform.position, new Quaternion(), GameController.Instance.TmpObjHolder.transform);
        if (facingRight)
        {
            go.GetComponent<Bullet>().FacingRight = true;
        }
        else
            go.GetComponent<Bullet>().FacingRight = false;
    }

    public void TakeDamage(int damage)
    {
        if (!damageImune)
        {
            Health -= damage;
            damageImune = true;
        }
    }
}


