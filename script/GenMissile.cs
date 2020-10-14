using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GenMissile : MonoBehaviour
{
    [SerializeField]
    private string TargetTag;
    [SerializeField]
    private float Interval;
    private float Timer;
    [SerializeField]
    private float TimeSpeed;
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private float DestroyTime = 10;
    // Start is called before the first frame update
    async UniTask Start()
    {
        Timer = -0.1f;
    }

    // Update is called once per frame
    async UniTask Update()
    {
        if(Timer <= 0.0f)
        {
            GameObject Missile01 = (GameObject)Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 0));
            Missilemk2 Mk2 = Missile01.GetComponent<Missilemk2>();
            Mk2.TargetTag = TargetTag;
            /*Missile Ms = Missile01.GetComponent<Missile>();
            Ms.TargetTag = TargetTag;
            Rigidbody MsRb = Missile01.GetComponent<Rigidbody>();
            MsRb.velocity = Missile01.transform.forward * Ms.Velocity;*/
            Destroy(Missile01, DestroyTime);
            Timer = Interval;
        }
        Timer -= 0.1f * TimeSpeed * Time.deltaTime;
    }
}
