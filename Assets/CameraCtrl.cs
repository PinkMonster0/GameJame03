using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    [HideInInspector]
    public bool isGameStart = false;

    private Camera m_camera;
    private Animator m_cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStart) return;

        m_cameraAnimator.enabled = false;


        float distance = (Mathf.Abs(player1.position.x - player2.position.x) / 2) * 9 / 16 + 1;
        Debug.Log(distance);
        
         m_camera.orthographicSize = Mathf.Clamp(distance, 5.0f, 15.0f);
        
        float camera_x = (player1.position.x+player2.position.x) / 2f;
        
        camera_x = Mathf.Clamp(camera_x, -25.4f + (m_camera.orthographicSize*16f/9f), 27.88f - (m_camera.orthographicSize*16f/9f));
        
        float camera_y = m_camera.orthographicSize - 2.5f;
        camera_y = Mathf.Max(camera_y , (player1.position.y+player2.position.y)/2f + 0.66f); 

        Vector3 targetPosition = new Vector3( camera_x, camera_y, -10f);
        

        float frameSpeed = Time.deltaTime * 5f;
        float dis = Vector3.Distance(transform.position, targetPosition);
        float pross = 0;
        if (frameSpeed >dis)
        {   
            transform.position = targetPosition;
        }
        else
        {
            pross = frameSpeed / dis;
            transform.position = Vector3.Lerp(transform.position, targetPosition, pross);
        }

        //transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);

        //transform.position = targetPosition;
    }

        public void SetGameStart()
    {
        isGameStart = true;
    }
        public void SetGameStop()
    {
        isGameStart = false;
    }
}
