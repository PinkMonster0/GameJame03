using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    private Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (Mathf.Abs(player1.position.x - player2.position.x) / 2) * 9 / 16 + 1;
        
         m_camera.orthographicSize = Mathf.Clamp(distance, 5.0f, 10.0f);
        
        float camera_x = (player1.position.x+player2.position.x) / 2f;
        
        camera_x = Mathf.Clamp(camera_x, -25.4f + (m_camera.orthographicSize*16f/9f), 27.88f - (m_camera.orthographicSize*16f/9f));
        
        float camera_y = m_camera.orthographicSize - 2.5f;
        camera_y = Mathf.Max(camera_y , (player1.position.y+player2.position.y)/2f + 0.66f); 

        m_camera.transform.position = new Vector3( camera_x, camera_y, -10f);
    }
}
