using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hohei : MonoBehaviour
{
    private List<Transform> basespos = new List<Transform>();
    [SerializeField]
    private float speed;
    [SerializeField]
    private float Goaldes;
    [SerializeField]
    private GameObject Shooter;
    [SerializeField]
    private string BaseTag;
    [SerializeField]
    private string EBaseTag;
    [SerializeField]
    private string Etag;
    [SerializeField]
    private int HitPoint;
    private Transform Target;
    private List<Transform> enemylist = new List<Transform>();
    //private bool Attack_F = false;
    private float tdis;
    // Start is called before the first frame update
    void Start()
    {
        Shooter.SetActive(false);
        Seach();
    }
    // Update is called once per frame
    void Update()
    {
        EListRemove();
        if (enemylist.Count == 0)
        {
            Shooter.SetActive(false);
            if (basespos.Count > 0 && basespos[0] != null)
            {
                float dis = Vector3.Distance(transform.position, basespos[0].position);
                if (dis >= Goaldes)
                {
                    transform.LookAt(basespos[0].position);
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
            else
            {
                Seach();
            }
            Check();
        }
        else
        {
            LockOn();
            tdis = Vector3.Distance(Target.transform.position, transform.position);
            if (tdis >= 4.0f)
            {
                AttackMove();
            }
            else
            {
                Attack();
            }
        }
        ChKDead();
    }
    void ChKDead()
    {
        if(HitPoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void Attack()
    {
        transform.LookAt(Target.transform.position);
        Shooter.SetActive(true);
    }
    void AttackMove()
    {
        Shooter.SetActive(false);
        transform.LookAt(Target.transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void EListRemove()
    {
        List<Transform> tList = new List<Transform>();
        foreach(Transform ts in enemylist)
        {
            tList.Add(ts);
        }
        foreach(Transform ts in tList)
        {
            if (ts == null)
            {
                enemylist.Remove(ts);
            }
        }
    }
    void Seach()
    {
        basespos.Clear();
        GameObject[] bases;
        bases = GameObject.FindGameObjectsWithTag("minibase");
        GameObject[] ebases;
        ebases = GameObject.FindGameObjectsWithTag(EBaseTag);
        foreach (GameObject obj in bases)
        {
            basespos.Add(obj.transform);
        }
        foreach (GameObject obj in ebases)
        {
            basespos.Add(obj.transform);
        }
        basespos.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(this.gameObject.transform.position, b.transform.position));
        });
    }
    void Check()
    {
        //Debug.Log("GG");
        List<Transform> tList = new List<Transform>();
        foreach (Transform ts in basespos)
        {
            tList.Add(ts);
        }
        foreach (Transform ts in tList)
        {
            if (ts.gameObject.CompareTag(BaseTag))
            {
                basespos.Remove(ts);
            }
        }
        tList.Clear();
        foreach (Transform ts in basespos)
        {
            tList.Add(ts);
        }
        foreach (Transform ts in tList)
        {
            MiniBase MB = ts.parent.gameObject.GetComponent<MiniBase>();
            if (!MB.Flag01)
            {
                Seach();
            }
        }
    }
    void LockOn()
    {
        if (enemylist.Count > 0)
        {
            enemylist.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.transform.position, b.transform.position));
            });
            Target = enemylist[0];
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            HitPoint -= 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Etag))
        {
            //Debug.Log("VK");
            enemylist.Add(other.transform);
        }
    }
}
