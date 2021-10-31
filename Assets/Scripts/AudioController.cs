using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip jump;
    public static AudioClip crash;
    public static AudioClip bullet_time;
    public static AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        jump = Resources.Load<AudioClip>("jump");
        crash = Resources.Load<AudioClip>("crash");
        bullet_time = Resources.Load<AudioClip>("bullet_time");
        click = Resources.Load<AudioClip>("click");
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public static void PlayJump()
    {
        audioSrc.PlayOneShot(jump);
    }
    public static void PlayCrash()
    {
        audioSrc.PlayOneShot(crash);
    }

    public static void PlayClick()
    {
        audioSrc.PlayOneShot(click);
    }
}
