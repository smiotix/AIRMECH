using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;

public class Missilemk2 : MonoBehaviour
{
    [SerializeField]
    private GameObject directioner;
    [System.NonSerialized]
    public string TargetTag;
    private GameObject[] Enemys;
    private GameObject TargetEP;
    private Rigidbody Rb;
    private float ang_intv;
    [SerializeField]
    private float InterVal;
    private float Timer;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float RotSpeed = 0.8f;
    public float Pow = 8f;
    // Start is called before the first frame update
    async UniTask Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    async UniTask Update()
    {
        if (Timer <= 0.0f)
        {
            await Targeting();
            Timer = InterVal;
        }
        Timer -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask SearchEP()
    {
        Enemys = GameObject.FindGameObjectsWithTag(TargetTag);
        if (Enemys.Length > 1)
        {
            float dis01 = Vector3.Distance(Enemys[0].transform.position, Rb.position);
            float dis02 = Vector3.Distance(Enemys[1].transform.position, Rb.position);
            if (dis01 < dis02)
            {
                TargetEP = Enemys[0];
            }
            else
            {
                TargetEP = Enemys[1];
            }
        }
        else
        {
            TargetEP = Enemys[0];
        }
    }
    async UniTask Targeting()
    {
        await SearchEP();
        directioner.transform.LookAt(TargetEP.transform.position);
        ang_intv = Quaternion.Angle(transform.rotation, directioner.transform.rotation);
        ang_intv = 181 - Mathf.Abs(ang_intv);
        transform.rotation = Quaternion.Lerp(transform.rotation, directioner.transform.rotation, ang_intv * RotSpeed * Time.deltaTime);
        Rb.velocity = transform.forward * speed;
    }
    async UniTask OnCollisionEnter(Collision collision)
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
