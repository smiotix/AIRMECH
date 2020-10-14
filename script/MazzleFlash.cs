using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


public class MazzleFlash : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Flash;
    [SerializeField]
    private float Interval;
    [SerializeField]
    private float ActiveTime;
    private float Timer;
    private float AcTimer;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in Flash)
        {
            obj.SetActive(false);
        }
        Timer = Interval;
        AcTimer = ActiveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer <= 0.0f)
        {
            if (!Flash[0].activeSelf)
            {
                foreach (GameObject obj in Flash)
                {
                    obj.SetActive(true);
                }
            }
            if (AcTimer <= 0.0f)
            {
                if (Flash[0].activeSelf)
                {
                    foreach (GameObject obj in Flash)
                    {
                        obj.SetActive(false);
                    }
                }
                Timer = Interval;
                AcTimer = ActiveTime;
            }
            AcTimer -= 0.1f * 60 * Time.deltaTime;
        }
        Timer -= 0.1f * 60 * Time.deltaTime;
    }
}
