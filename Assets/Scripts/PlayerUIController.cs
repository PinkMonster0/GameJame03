using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform HealthPanel_P1;
    public Transform HealthPanel_P2;
    public Transform Power_P1;
    public Transform Power_P2;
    private Transform[] Health_P1_Image=new Transform[5];
    private Transform[] Health_P2_Image = new Transform[5];
    public float P1_Health=100;
    public float P2_Health=100;
    public float P1_Power=0;
    public float P2_Power=0;
    void Start()
    {
        init();

    }

    // Update is called once per frame
    void Update()
    {

        checkpower(P1_Power, Power_P1);
        checkpower(P2_Power, Power_P2);
        checkhealth(P1_Health, Health_P1_Image);
        checkhealth(P2_Health, Health_P2_Image);

    }
    void init()
    {
        for (int i = 0; i < 5; i++)
        {
            Health_P1_Image[i] = HealthPanel_P1.GetChild(i).GetChild(0);
            Health_P2_Image[i]= HealthPanel_P2.GetChild(i).GetChild(0);
            Health_P1_Image[i].gameObject.SetActive(true);
            Health_P2_Image[i].gameObject.SetActive(true);
        }
    }
    void checkhealth(float health, Transform[] HealthPanel)
    {
        if (health <= 80)
        {
            HealthPanel[4].gameObject.SetActive(false);
            HealthPanel[3].gameObject.SetActive(true);
            HealthPanel[2].gameObject.SetActive(true);
            HealthPanel[1].gameObject.SetActive(true);
            HealthPanel[0].gameObject.SetActive(true);
        }
        if (health <= 60)
        {
            HealthPanel[4].gameObject.SetActive(false);
            HealthPanel[3].gameObject.SetActive(false);
            HealthPanel[2].gameObject.SetActive(true);
            HealthPanel[1].gameObject.SetActive(true);
            HealthPanel[0].gameObject.SetActive(true);

        }
        if (health <= 40)
        {
            HealthPanel[4].gameObject.SetActive(false);
            HealthPanel[3].gameObject.SetActive(false);
            HealthPanel[2].gameObject.SetActive(false);
            HealthPanel[1].gameObject.SetActive(true);
            HealthPanel[0].gameObject.SetActive(true);
        }
        if (health <= 20)
        {
            HealthPanel[4].gameObject.SetActive(false);
            HealthPanel[3].gameObject.SetActive(false);
            HealthPanel[2].gameObject.SetActive(false);
            HealthPanel[1].gameObject.SetActive(false);
            HealthPanel[0].gameObject.SetActive(true);
        }
        if (health <= 0)
        {
            HealthPanel[4].gameObject.SetActive(false);
            HealthPanel[3].gameObject.SetActive(false);
            HealthPanel[2].gameObject.SetActive(false);
            HealthPanel[1].gameObject.SetActive(false);
            HealthPanel[0].gameObject.SetActive(false);
        }

    }
    void checkpower(float power, Transform PowerSlide)
    {
        PowerSlide.GetComponent<Slider>().value = power;
    }
}
