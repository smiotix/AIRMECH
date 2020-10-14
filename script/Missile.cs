using System.Collections;
using System.Collections.Generic;
//using Unity.Entities.UniversalDelegates;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Missile : MonoBehaviour
{
    //[SerializeField]
    //private GameObject directioner;
    [SerializeField]
    private float Timespeed = 60;
    [SerializeField]
    private float Interval;
    private float Timer;
    public float Velocity;
    [System.NonSerialized]
    public string TargetTag;
    private Rigidbody Rb;
    private GameObject Target;
    private float x = 0;
    private float y = 0;
    private float z = 0;
    [SerializeField]
    private int Count = 5;
    private int Counter;
    public float Pow = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();        
        Timer = Interval;
        Counter = Count;
    }

    // Update is called once per frame
    void Update()
    {
        //directioner.transform.position = transform.position;
        if(Timer <= 0.0f && Counter > 0)
        {
            LooKTarget();
            Rb.velocity = transform.forward * Velocity;
            Counter--;
            Timer = Interval;
        }
        Timer -= 0.1f * Timespeed * Time.deltaTime;
    }
    void LooKTarget()
    {
        Target = GameObject.FindGameObjectWithTag(TargetTag);
        if (Target != null)
        {
            transform.LookAt(Target.transform.position);
        }
    }/*
    public void Direction()
    {
        Target = GameObject.FindGameObjectWithTag(TargetTag);
        //directioner.transform.LookAt(Target.transform.position);
        //x = directioner.transform.localRotation.eulerAngles.x;
        //y = directioner.transform.localRotation.eulerAngles.y;
        //z = directioner.transform.localRotation.eulerAngles.z;
        if (x > 315)
        {
            x = 315;
        }
        else if (x > 270)
        {
            x = 270;
        }
        else if (x > 225)
        {
            x = 225;
        }
        else if (x > 180)
        {
            x = 180;
        }
        else if (x > 135)
        {
            x = 135;
        }
        else if (x > 90)
        {
            x = 90;
        }
        else if (x > 45)
        {
            x = 45;
        }
        else if (x > 0)
        {
            x = 0;
        }
        if (y > 315)
        {
            y = 315;
        }
        else if (y > 270)
        {
            y = 270;
        }
        else if (y > 225)
        {
            y = 225;
        }
        else if (y > 180)
        {
            y = 180;
        }
        else if (y > 135)
        {
            y = 135;
        }
        else if (y > 90)
        {
            y = 90;
        }
        else if (y > 45)
        {
            y = 45;
        }
        else if (y > 0)
        {
            y = 0;
        }
        if (z > 315)
        {
            z = 315;
        }
        else if (z > 270)
        {
            z = 270;
        }
        else if (z > 225)
        {
            z = 225;
        }
        else if (z > 180)
        {
            z = 180;
        }
        else if (z > 135)
        {
            z = 135;
        }
        else if (z > 90)
        {
            z = 90;
        }
        else if (z > 45)
        {
            z = 45;
        }
        else if (z > 0)
        {
            z = 0;
        }
        transform.rotation = Quaternion.Euler(x, y, z);
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (TargetTag == "RmissileT")
        {
            if (collision.gameObject.CompareTag("BLUE"))
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        if (TargetTag == "BmissileT")
        {
            if (collision.gameObject.CompareTag("RED"))
            {

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
