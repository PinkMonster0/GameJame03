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

    private bool isPaused;
    private float prevTimeScale;
    
    
    void LeftPresed()
    {
        Debug.Log("Left Pressed");
    }

    void RightPressed()
    {
        Debug.Log("Right Pressed");
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
        Player1.Flip();
        Player2.Flip();
        Player1.Jump(10);
        Player2.Jump(10);

    }
    
    void Start()
    {
        onLeftPressed += LeftPresed;
        onRightPressed += RightPressed;
        onPausePressed += PausePressed;
    }

    void Update()
    {
        
    }
}
