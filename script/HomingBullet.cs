using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class HomingBullet : MonoBehaviour
{
    [SerializeField]
    private float Interval = 8;
    private float Timer;
    [SerializeField]
    private string EUnitTag01;
    [SerializeField]
    private string EUnittag02;
    //[SerializeField]
    //private string EUnittag03;
    private GameObject[] Eobj01;
    private GameObject[] Eobj02;
    public float Power = 1f;
    private List<Transform> EList= new List<Transform>();
    private Rigidbody Rb;
    [SerializeField]
    private float velocity;
    // Start is called before the first frame update
    async UniTask Start()
    {
        Timer = 0.0f;
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    async UniTask Update()
    {
        if(Timer <= 0.0f)
        {
            EList.Clear();
            Eobj01 = GameObject.FindGameObjectsWithTag(EUnitTag01);
            Eobj02 = GameObject.FindGameObjectsWithTag(EUnittag02);
            foreach(GameObject obj in Eobj01)
            {
                EList.Add(obj.transform);
            }
            foreach (GameObject obj in Eobj02)
            {
                EList.Add(obj.transform);
            }
            if(EList.Count > 1)
            {
                EList.Sort(delegate (Transform a, Transform b)
                {
                    return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                    .CompareTo(
                      Vector3.Distance(this.gameObject.transform.position, b.transform.position));
                });
            }
            if (EList.Count > 0)
            {
                transform.LookAt(EList[0].position);
            }
            Rb.velocity = transform.forward * velocity;
            Timer = Interval;
        }
        Timer -= 0.1f * 60 * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
