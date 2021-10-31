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
    
    public float moveSpeed;
    public float jumpForce;
    public float jumpDamp;
    public float gravity;
    public PlayerStat ps;
    
    public AudioClip[] jumpClips;

    private Transform groundCheck;
    private Animator anim;
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private bool currentMoveLeft;
    private bool grounded;
    private float ySpeed;

    public void Jump(float force)
    {
        if (!grounded || ps != PlayerStat.WALKING)
        {
            return;
        }

        ps = PlayerStat.JUMPINGUP;
        ySpeed = force;
        anim.SetTrigger(Jump1);

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
        transform.Find("headCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;
        transform.Find("FootCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;
        transform.Find("headFootCheck").GetComponent<CollisionHandler>().collideDelegate += OnHeadFootHit;

        anim = GetComponent<Animator>();
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

        StartMove();
    }

    void StartMove()
    {
        anim.SetFloat(Speed, moveSpeed);
    }

    void Move()
    {
        // Debug.Log(ps + ", " + grounded);
        Vector3 currentSpeed = Vector3.zero;
        int moveDir = Convert.ToInt16(currentMoveLeft) * 2 - 1;
        if (!grounded)
        {
            ySpeed -= gravity * Time.deltaTime;
            if (ySpeed < -jumpForce * 0.4)
            {
                ySpeed = -jumpForce;
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
                    ps = PlayerStat.WALKING;
                }
                else
                {
                    currentSpeed.x -= ySpeed * moveDir * 0.5f;
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

    public void Flip()
    {
        Debug.Log("Flip!");
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
        Debug.Log("hit body + " + other.name);
        Flip();
    }

    void OnHeadFootHit(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHit();
            // Flip();
            // Jump(10);
        }
    }
    
    void CheckGround()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, Int32.MaxValue - LayerMask.GetMask($"Player"));
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }
}
