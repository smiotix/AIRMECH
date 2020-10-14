using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eobject : MonoBehaviour
{
    [SerializeField]
    private int MaxHitPoint;
    private int HitPoint;
    [SerializeField]
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        HitPoint = MaxHitPoint;
        slider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider();
        if(HitPoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void HPSlider()
    {
        slider.value = (float)HitPoint / (float)MaxHitPoint;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            HitPoint -= 1;
        }
    }
}
