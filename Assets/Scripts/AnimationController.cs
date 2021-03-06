using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    

    public Animator Camera_animator;
    public Animator UI_animator;
    private bool isplay = false;
    private bool iswin = false;
    // Start is called before the first frame update
    void Start()
    {
        Camera_animator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isplay==true)
        {
            isplay = false;
            Camera_animator.SetBool("IsPlay", true);
            UI_animator.SetBool("IsPlay", true);
        }
        if (iswin == true)
        {
            iswin = false;
            UI_animator.SetBool("IsWin", true);
        }

    }
    public void play()
    {
        //print(1);
        isplay = true;
    }
    public void win()
    {
        iswin = true;
    }
    public void playagian()
    {
        //print(1);
        isplay = true;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void SetAnimatorDefualt()
    {
        Camera_animator.SetBool("IsPlay", false);
        UI_animator.SetBool("IsPlay", false);
        Camera_animator.SetBool("IsWin", false);
        UI_animator.SetBool("IsWin", false);
        Camera_animator.enabled = false;
        GameManager.Instance.paused = false;
    }
    
}
