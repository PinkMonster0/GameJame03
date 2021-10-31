using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlayerStat
{
    WALKING,
    JUMPINGUP,
    JUMPINGDOWN,
}

public class PlayerController : MonoBehaviour
{
    public bool isLeftPlayer;
    public bool currentMoveLeft;

    public float moveSpeed;
    public float maxJumpForce;
    public float gravity;
    public PlayerStat ps;
    public int health;
    
    public AudioClip[] jumpClips;
    public GameObject jumpEffect;
    public GameObject hitEffect;
    
    private Transform groundCheck;
    private Animator anim;
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int Landing = Animator.StringToHash("Landing");
    private static readonly int GetHit = Animator.StringToHash("GetHit");

    private Vector3 prevPos;
    private bool grounded;
    private float ySpeed;
    [SerializeField] private float initialPhase;
    private float currentPhase;
    private float jumpForce;
    private float startJumpForce;

    public void Jump(float force)
    {
        Debug.Log(grounded + "jump" + ps + " f: " + force);
        if (!grounded || ps != PlayerStat.WALKING)
        {
            return;
        }

        ps = PlayerStat.JUMPINGUP;
        ySpeed = force;
        startJumpForce = force;
        anim.SetTrigger(Jump1);
        Instantiate(jumpEffect, groundCheck.position + Vector3.up, groundCheck.rotation);

        // int i = Random.Range(0, jumpClips.Length);
        // AudioSource.PlayClipAtPoint(jumpClips[i], position);

        // transform.Translate(new Vector3(0f, jumpForce, 0f));

        // currentMoveLeft ^= true;
    }

    private void OnJump()
    {
        Jump(jumpForce);
    }
    
    private void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        transform.Find("hitCheck").GetComponent<CollisionHandler>().collideDelegate += OnBodyHit;
        // transform.Find("headCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;
        // transform.Find("FootCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;
        transform.Find("headFootCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;

        anim = transform.Find("JumpingStudent").GetComponent<Animator>();
        ps = PlayerStat.WALKING;

    }

    void Start()
    {
        currentMoveLeft = isLeftPlayer;
        if (isLeftPlayer)
        {
            GameManager.Instance.onLeftPressed += OnJump;
        }
        else
        {
            GameManager.Instance.onRightPressed += OnJump;
        }

        currentPhase = initialPhase;
        health = 5;
        // StartMove();
        InvokeRepeating(nameof(CheckMovement), 1.0f, 0.3f);
    }

    // void StartMove()
    // {
    //     anim.SetFloat(Speed, moveSpeed);
    // }

    void Move()
    {
        // Debug.Log(ps + ", " + grounded);
        Vector3 currentSpeed = Vector3.zero;
        int moveDir = Convert.ToInt16(currentMoveLeft) * 2 - 1;
        if (!grounded)
        {
            ySpeed -= gravity * Time.deltaTime;
            if (ySpeed < -maxJumpForce * 0.4)
            {
                ySpeed = -maxJumpForce;
            }
        }
        switch (ps)
        {
            case PlayerStat.JUMPINGUP:
                if (ySpeed <= 0)
                {
                    ps = PlayerStat.JUMPINGDOWN;
                }
                else
                {
                    currentSpeed.x += ySpeed * moveDir * 0.5f;
                }
                break;
            case PlayerStat.JUMPINGDOWN:
                if (grounded)
                {
                    ySpeed = 0.0f;
                    currentPhase = initialPhase;
                    anim.SetTrigger(Landing);
                    ps = PlayerStat.WALKING;
                }
                else
                {
                    currentSpeed.x -= ySpeed * moveDir * 0.3f;
                }
                break;
            case PlayerStat.WALKING:
                if (grounded)
                {
                    ySpeed = 0.0f;
                }
                break;
        }

        currentSpeed.y += ySpeed;
        currentSpeed.x += moveSpeed * moveDir;
        transform.Translate(currentSpeed * Time.deltaTime);
    }

    public void Hit()
    {
        anim.SetTrigger(GetHit);
        Instantiate(hitEffect, transform, true);
    }
    
    public void Flip()
    {
        // Debug.Log("Flip!");
        currentMoveLeft ^= true;
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1;
        transform1.localScale = theScale;
    }

    void OnBodyHit(Collider2D other)
    {
        if ((1 << other.gameObject.layer & (LayerMask.GetMask($"Player")  + LayerMask.GetMask($"HighGround"))) != 0)
        {
            return;
        }
        // Debug.Log("hit body + " + other.name);
        Flip();
    }

    void OnHeadFootHit(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHit();
            // Collider2D[] contacts = new Collider2D[100];
            // other.GetContacts(contacts);
            // Instantiate(hitEffect, contacts[0].transform.position, contacts[0].transform.rotation);
            // Flip();
            // Jump(10);
        }
    }
    
    void CheckGround()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, Int32.MaxValue - LayerMask.GetMask($"Player"));
    }

    void CheckMovement()
    {
        Vector3 currPos = transform.position;
        if (prevPos == currPos)
        {   
            Flip();
        }

        prevPos = currPos;

    }

    private void Update()
    {
        // if (ps == PlayerStat.WALKING)
        // {
        //     currentPhase += Time.time * 2;
        // }
        // float t = Mathf.Sin(currentPhase);
        // if (t >= 0)
        // {
        //     jumpForce = t * maxJumpForce + 5;
        // }
        // else
        // {
        //     jumpForce = 0;
        // }

        jumpForce = maxJumpForce + 5;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.paused)
        {
            return;
        }
        CheckGround();
        Move();
    }
}
