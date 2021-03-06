using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance;
    public static GameManager Instance
    {
        get {
            if(s_Instance != null) {
                return s_Instance;
            }
            s_Instance = FindObjectOfType<GameManager>(); 
            if(s_Instance != null) {
                return s_Instance;
            }
            GameObject singletonObject = new GameObject("GameManager");
            s_Instance = singletonObject.AddComponent<GameManager> ();
            return s_Instance;
        }
    }

    public delegate void OnKeyPressed();

    public OnKeyPressed onLeftPressed;
    public OnKeyPressed onRightPressed;
    public OnKeyPressed onPausePressed;

    public PlayerController Player1;
    public PlayerController Player2;
    public PlayerUIController uIController;
    public AnimationController animController;

    public bool paused = false;
    
    private bool isPaused;
    private float prevTimeScale;
    
    
    void LeftPresed()
    {
        // Debug.Log("Left Pressed");
    }

    void RightPressed()
    {
        // Debug.Log("Right Pressed");
    }

    void PausePressed()
    {
        Debug.Log("Pause Pressed");
        if(isPaused)
            Time.timeScale = prevTimeScale;
        else
        {
            prevTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        isPaused ^= true;
    }

    public void PlayerHit()
    {
        Debug.Log("player hit!");
        var position1 = Player1.transform.position;
        var position2 = Player2.transform.position;
        bool isPlayer1AtLeft = position1.x < position2.x;
        int isPlayer1Higher = 0;
        if (position1.y - position2.y >= 0.3f)
        {
            isPlayer1Higher = 1;
        }
        else if (position2.y - position1.y >= 0.3f)
        {
            isPlayer1Higher = -1;
        }
        
        if (!(isPlayer1AtLeft ^ Player1.currentMoveLeft))
        {
            Player1.Flip();
            Player1.Jump(10);
        }

        if (!(!isPlayer1AtLeft ^ Player2.currentMoveLeft))
        {
            Player2.Flip();
            Player2.Jump(10);
        }

        if (isPlayer1Higher == 0 || (Player1.ps == PlayerStat.WALKING && Player2.ps == PlayerStat.WALKING))
        {

            return;
        }
        else
        {

            if (isPlayer1Higher == 1)
            {
                if (Player1.ps == PlayerStat.JUMPINGDOWN)
                {
                    // Debug.Log("player1 win");
                    PlayerWin(Player1);
                }
                else if (Player2.ps == PlayerStat.JUMPINGUP)
                {
                    // Debug.Log("player2 win");
                    PlayerWin(Player2);

                }
            }
            else if (isPlayer1Higher == -1)
            {
                if (Player2.ps == PlayerStat.JUMPINGDOWN)
                {
                    // Debug.Log("player2 win");
                    PlayerWin(Player2);

                }
                else if (Player1.ps == PlayerStat.JUMPINGUP)
                {
                    // Debug.Log("player1 win");
                    PlayerWin(Player1);

                }
            }
        }
    }
    
    void PlayerWin(PlayerController p)
    {
        PlayerController otherP = GetOtherPlayer(p);
        otherP.Hit();
        if (otherP == Player1)
        {
            uIController.P1_Health -= 20;
            if (uIController.P1_Health <= 0)
            {
                animController.win();
            }
        }
        else if (otherP == Player2)
        {
            uIController.P2_Health -= 20;
            if (uIController.P2_Health <= 0)
            {
                animController.win();
            }
        }

        SlowTime();
        Invoke(nameof(ResumeTime), 0.5f);
        Debug.Log(p.name);
    }
    
    PlayerController GetOtherPlayer(PlayerController current)
    {
        if (current == Player1)
        {
            return Player2;
        }
        else if (current == Player2)
        {
            return Player1;
        }
        else
        {
            return null;
        }
    }

    void SlowTime()
    {
        // Time.timeScale = 0.1f;
        paused = true;
    }
    
    void ResumeTime()
    {
        // Time.timeScale = 1.0f;
        paused = false;
        var position1 = Player1.transform.position;
        var position2 = Player2.transform.position;
        bool isPlayer1AtLeft = position1.x < position2.x;
        if (!(isPlayer1AtLeft ^ Player1.currentMoveLeft))
        {
            Player1.Flip();
        }

        if (!(!isPlayer1AtLeft ^ Player2.currentMoveLeft))
        {
            Player2.Flip();
        }
    }
    
    void Start()
    {
        onLeftPressed += LeftPresed;
        onRightPressed += RightPressed;
        onPausePressed += PausePressed;

        paused = true;
    }

    void Update()
    {
        
    }
}
