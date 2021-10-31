using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayCtrl : MonoBehaviour
{
    [SerializeField]
    private Animator m_amimator;
    [SerializeField]
    private GameObject m_fxPrefab;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_amimator.SetTrigger("Jump");
            Instantiate(m_fxPrefab, transform);
        }
    }
}
