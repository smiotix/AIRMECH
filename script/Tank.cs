using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField]
    private GameObject Houdai;
    [SerializeField]
    private GameObject Shooter;
    [SerializeField]
    private string Etag;
    //[SerializeField]
    //private string EBULLET;
    private List<Transform> enemylist = new List<Transform>();
    private Transform Target;
    [SerializeField]
    private GameObject Goal;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float GoalDis;
    [SerializeField]
    private float TarDis;
    private bool Move_F = true;
    //[SerializeField]
    //private bool DMGFlag;
    [SerializeField]
    private int HitPoint;
    //private Quaternion Hrot;
    // Start is called before the first frame update
    void Start()
    {
        Shooter.SetActive(false);
        //Hrot = Houdai.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move_F)
        {
            Move();
        }
        else
        {
            TarMove();
        }
        MFtrue();
        Break();
        //Debug.Log(enemylist.Count);
    }
    void Move()
    {
        float dis = Vector3.Distance(Goal.transform.position, transform.position);
        if (dis >= GoalDis)
        {
            transform.LookAt(Goal.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    void TarMove()
    {
        if (enemylist.Count != 0)
        {
            float dis = 100;
            LockOn();
            if (Target != null)
            {
                transform.LookAt(Target.transform.position);
                dis = Vector3.Distance(Target.transform.position, transform.position);
            }
            //float dis = Vector3.Distance(Target.transform.position, transform.position);
            if (dis >= TarDis)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        StartAttack();
    }
    void StartAttack()
    {
        if (Target != null)
        {
            Attack();
        }
        else
        {
            StopAttack();
        }
    }
    void Attack()
    {
        Houdai.transform.LookAt(Target.transform.position);
        Shooter.SetActive(true);
    }
    void StopAttack()
    {
        if (enemylist.Count != 0)
        {
            List<Transform> TList = new List<Transform>();
            foreach (Transform enemy in enemylist)
            {
                TList.Add(enemy);
            }
            foreach (Transform enemy in TList)
            {
                if (enemy == null)
                {
                    enemylist.Remove(enemy);
                }
            }
        }
        else
        {
            Shooter.SetActive(false);
            Houdai.transform.LookAt(Goal.transform.position);
        }
    }
    void MFtrue()
    {
        if (enemylist.Count == 0)
        {
            Move_F = true;
        }
    }
    void LockOn()
    {
        if (enemylist.Count < 1)
        {
            enemylist.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.transform.position, b.transform.position));
            });
        }
        Target = enemylist[0];
    }
    void Break()
    {
        if (HitPoint <= 0)
        {
            Destroy(this.gameObject);
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
        if (other.CompareTag(Etag))
        {
            enemylist.Add(other.gameObject.transform);
            Move_F = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Etag))
        {
            if (enemylist.Count != 0)
            {
                enemylist.Remove(other.gameObject.transform);
            }
        }
    }
}
