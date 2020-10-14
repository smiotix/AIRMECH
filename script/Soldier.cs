using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
//using Unity.Mathematics;

public class Soldier : MonoBehaviour
{
    private string mBaseTag = "minibase";
    private string ABaseMTag;
    private string EBaseTag;
    private string EBaseMtag;
    private string AUnitTag01;
    private string EnemyTag01;
    private string EnemyTag02;
    public float MaxHitPoint;
    [System.NonSerialized]
    public float HitPoint;
    [System.NonSerialized]
    public bool BattleFlag = false;
    [SerializeField]
    private float EnemyCutDis;
    [SerializeField]
    private float EnemyAttackDis;
    [SerializeField]
    private GameObject Shooter;
    private bool isGround = false;
    private NavMeshAgent NMA;
    private List<Transform> mBList = new List<Transform>();
    private List<GameObject> EList = new List<GameObject>();
    private Transform TmB;
    private Animator Anim;
    private Rigidbody Rb;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject Select;
    private bool Flag01 = false;
    [SerializeField]
    private GameObject EHomeBase;
    [SerializeField]
    private float EHB_dis = 25;
    [SerializeField]
    private float Defense = 1.4f;
    private GameObject[] mBs;
    private GameObject[] EBs;
    private GameObject[] ABM;
    private GameObject[] EBM;
    private float dis;
    private float dis2;
    private GameObject[] E01s;
    private GameObject[] E02s;
    [SerializeField]
    private float EnemySerachDis = 75;
    private Ray ray;
    private RaycastHit hit;
    private float raydist = 0.3f;
    private float mBFirstDis = 4.0f;
    // Start is called before the first frame update
    async UniTask Start()
    {
        if (this.gameObject.CompareTag("Bhohei"))
        {
            ABaseMTag = "BMiniBase_Minus";
            EBaseTag = "RminiBase";
            EBaseMtag = "RMiniBase_Minus";
            AUnitTag01 = "BLUE";
            EnemyTag01 = "RED";
            EnemyTag02 = "Rhohei";
        }
        else
        {
            ABaseMTag = "RMiniBase_Minus";
            EBaseTag = "BminiBase";
            EBaseMtag = "BMiniBase_Minus";
            AUnitTag01 = "RED";
            EnemyTag01 = "BLUE";
            EnemyTag02 = "Bhohei";
        }
        Shooter.SetActive(false);
        Select.SetActive(false);
        slider.value = 1f;
        NMA = GetComponent<NavMeshAgent>();
        NMA.enabled = false;
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        await GetmBList();
        HitPoint = MaxHitPoint;
        if (Rb.isKinematic)
        {
            Rb.isKinematic = false;
        }
    }

    // Update is called once per frame
    async UniTask Update()
    {
        //Debug.Log(EList.Count);
        await Command();
        await GroundRay();
        SliderHP();
        ChKDead();
        SelectIf();
    }
    void ChKDead()
    {
        if (HitPoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void SliderHP()
    {
        slider.value = HitPoint / MaxHitPoint;
    }
    async UniTask Command()
    {
        if (isGround)
        {
            await EnemySerch();
            await GetmBList();
            if (mBList.Count > 0)
            {
                if (EList.Count > 0)
                {
                    dis = Vector3.Distance(transform.position, EList[0].transform.position);
                    dis2 = Vector3.Distance(transform.position, mBList[0].position);
                    if (dis < dis2 + mBFirstDis)
                    {
                        if (dis > EnemyAttackDis)
                        {
                            await AttackMove();
                        }
                        else
                        {
                            await Attack();
                        }
                    }
                    else
                    {
                        Move();
                    }
                }
                else
                {
                    await Move();
                }
            }
            else
            {
                await HomeBaseAttack();
            }
        }
    }
    async UniTask Attack()
    {
        transform.LookAt(EList[0].transform.position);
        if (!Rb.isKinematic)
        {
            Rb.isKinematic = true;
        }
        if (NMA.enabled)
        {
            NMA.enabled = false;
        }
        if (Anim.GetBool("Run"))
        {
            Anim.SetBool("Run", false);
        }
        if (!Anim.GetBool("Shot"))
        {
            Anim.SetBool("Shot", true); ;
        }
        if (!Shooter.activeSelf)
        {
            Shooter.SetActive(true);
            BattleFlag = true;
        }
    }
    async UniTask AttackMove()
    {
        if (Rb.isKinematic)
        {
            Rb.isKinematic = false;
        }
        if (!Anim.GetBool("Run"))
        {
            Anim.SetBool("Run", true);
        }
        if (EList.Count > 0)
        {
            if (!NMA.enabled)
            {
                NMA.enabled = true;
            }
            else
            {
                if (NMA.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    NMA.SetDestination(EList[0].transform.position);
                }
            }
        }
    }
    async UniTask  Move()
    {
        if (mBList.Count > 0)
        {
            if (isGround)
            {
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
                if (BattleFlag)
                {
                    BattleFlag = false;
                }
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (!Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", true);
                }
                if (!NMA.enabled)
                {
                    StartCoroutine("NMAOn");
                }

                if (TmB != null)
                {
                    if (NMA.pathStatus != NavMeshPathStatus.PathInvalid)
                    {
                        NMA.SetDestination(TmB.position);
                    }
                }

            }
        }
        else
        {
            await HomeBaseAttack();
        }
    }
    private IEnumerator NMAOn()
    {
        yield return new WaitForSeconds(2.8f);
        NMA.enabled = true;
    }
    async UniTask GetmBList()
    {
        mBList.Clear();
        mBs = GameObject.FindGameObjectsWithTag(mBaseTag);
        EBs = GameObject.FindGameObjectsWithTag(EBaseTag);
        ABM = GameObject.FindGameObjectsWithTag(ABaseMTag);
        EBM = GameObject.FindGameObjectsWithTag(EBaseMtag);
        foreach (GameObject obj in mBs)
        {
            mBList.Add(obj.transform);
        }
        foreach (GameObject obj in EBs)
        {
            mBList.Add(obj.transform);
        }
        foreach(GameObject obj in ABM)
        {
            mBList.Add(obj.transform);
        }
        foreach(GameObject obj in EBM)
        {
            mBList.Add(obj.transform);
        }
        if (mBList.Count > 0)
        {
            mBList.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.gameObject.transform.position, b.transform.position));
            });
            TmB = mBList[0];
        }
        else
        {
            TmB = null;
        }
    }
    async UniTask HomeBaseAttack()
    {
        dis = Vector3.Distance(transform.position, EHomeBase.transform.position);
        if (dis > EHB_dis)
        {
            if (isGround)
            {
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
                if (BattleFlag)
                {
                    BattleFlag = false;
                }
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (!Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", true);
                }
                if (!NMA.enabled)
                {
                    StartCoroutine("NMAOn");
                }
                if (NMA.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    NMA.SetDestination(EHomeBase.transform.position);
                }
            }
        }
        else
        {
            if (isGround)
            {
                transform.LookAt(EHomeBase.transform.position);
                if (NMA.enabled)
                {
                    NMA.enabled = false;
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (!Anim.GetBool("Shot"))
                {
                    Anim.CrossFade("Shot", 0.2f);
                }
                if (Anim.IsInTransition(0))
                {
                    Shooter.SetActive(true);
                    BattleFlag = true;
                }
            }
        }
    }
    async UniTask EnemySerch()
    {
        EList.Clear();
        E01s = GameObject.FindGameObjectsWithTag(EnemyTag01);
        E02s = GameObject.FindGameObjectsWithTag(EnemyTag02);
        foreach (GameObject obj in E01s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < EnemySerachDis)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in E02s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < EnemySerachDis)
            {
                EList.Add(obj);
            }
        }
        if (EList.Count > 1)
        {
            EList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
        }
    }
    void SelectIf()
    {
        if (Flag01)
        {
            Select.SetActive(true);
        }
        else
        {
            Select.SetActive(false);
        }
    }
    public void SelectOn()
    {
        Flag01 = true;
    }
    public void SelectOff()
    {
        Flag01 = false;
    }
    public bool ReTurnSelect()
    {
        return Flag01;
    }
    public bool ReIfDameged()
    {
        if (HitPoint <= MaxHitPoint)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private async UniTask OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            Bullet Bt = collision.gameObject.GetComponent<Bullet>();
            HitPoint -= Bt.Power * Defense;
        }
        if (collision.gameObject.CompareTag(AUnitTag01) || collision.gameObject.CompareTag(this.gameObject.tag))
        {
            int i = Random.Range(0, 3);
            if (i == 0)
            {
                Rb.velocity = Vector3.forward * 9;
            }
            if (i == 1)
            {
                Rb.velocity = Vector3.left * 9;
            }
            if (i == 0)
            {
                Rb.velocity = Vector3.right * 9;
            }
            if (i == 0)
            {
                Rb.velocity = Vector3.back * 9;
            }
        }
    }
    async UniTask GroundRay()
    {
        ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, raydist))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isGround = true;
            }
            else
            {
                isGround = false;
            }
            if (hit.collider.gameObject.CompareTag(AUnitTag01) || hit.collider.gameObject.CompareTag(this.gameObject.tag))
            {
                int i = Random.Range(0, 3);
                if (i == 0)
                {
                    Rb.velocity = Vector3.forward * 9;
                }
                if (i == 1)
                {
                    Rb.velocity = Vector3.left * 9;
                }
                if (i == 0)
                {
                    Rb.velocity = Vector3.right * 9;
                }
                if (i == 0)
                {
                    Rb.velocity = Vector3.back * 9;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (NMA && NMA.enabled)
        {
            Gizmos.color = Color.red;
            var prefPos = transform.position;

            foreach (var pos in NMA.path.corners)
            {
                Gizmos.DrawLine(prefPos, pos);
                prefPos = pos;
            }
        }
    }
}