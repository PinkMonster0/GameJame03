using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

	
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyUp(KeyCode.P))
        {
            GameManager.Instance.onPausePressed();
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            GameManager.Instance.onLeftPressed();
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            GameManager.Instance.onRightPressed();
        }
    }
}
