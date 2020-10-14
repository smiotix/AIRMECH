using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class Warrior : MonoBehaviour
{
    private string mBaseTag = "minibase";
    private string AUnitTag01 = "minibase";
    private string EBaseTag = "minibase";
    private string EnemyTag01 = "minibase";
    private string EnemyTag02 = "minibase";
    private string EnemyTag03 = "minibase";
    private GameObject EHomeBase;
    [SerializeField]
    private float EHB_dis = 25;
    public float MaxHitPoint;
    [System.NonSerialized]
    public float HitPoint;
    [System.NonSerialized]
    public bool BattleFlag = false;
    [SerializeField]
    private float EnemyAttackDis;
    [SerializeField]
    private float mBIntervalDis;
    [SerializeField]
    private GameObject Shooter;
    [SerializeField]
    private GameObject GenMissile;
    private bool isGround = false;
    private NavMeshAgent NMA;
    private List<GameObject> mBList = new List<GameObject>();
    private List<GameObject> EList = new List<GameObject>();
    private List<GameObject> PList = new List<GameObject>();
    private Transform TmB;
    private Animator Anim;
    private Rigidbody Rb;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private GameObject Select;
    private bool Flag01 = false;
    private bool Flag02 = true;
    private bool Flag03 = true;
    private bool Flag04 = true;
    //[System.NonSerialized]
    public int CommandMode;
    private Vector3 StandPos;
    public string UnitType;
    [SerializeField]
    private float Defense;
    private float dis;
    private GameObject[] mBs;
    private GameObject[] EBs;
    private GameObject[] E01s;
    private GameObject[] E02s;
    private GameObject[] E03s;
    private GameObject[] P01s;
    [SerializeField]
    private float EnemySearchDis = 150;
    [SerializeField]
    private float HealSpeed = 60;
    private GameObject[] AUs01;
    private GameObject[] AUs02;
    private List<GameObject> AList = new List<GameObject>();
    [SerializeField]
    private float AllyCutDis = 150;
    [SerializeField]
    private float MedecineDis;
    private string AUtag1;
    private string AUtag2;
    [SerializeField]
    private float AntiAircraftDis = 25;
    private GameObject[] HBases;
    private Ray ray;
    private RaycastHit hit;
    //[SerializeField]
    private float raydist = 1.2f;
    [System.NonSerialized]
    public bool AntiHBA_F = false;
    private float dis_m;
    private float dis_H;
    // Start is called before the first frame update
    async UniTask Start()
    {
        await SetTag();

        Shooter.SetActive(false);
        GenMissile.SetActive(false);
        Select.SetActive(false);
        slider.value = 1f;
        NMA = GetComponent<NavMeshAgent>();
        NMA.enabled = false;
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        await GetmBList();
        HitPoint = MaxHitPoint;
        if (this.CompareTag("BLUE"))
        {
            AUtag1 = "BLUE";
            AUtag2 = "Bhohei";
        }
        else
        {
            AUtag1 = "RED";
            AUtag2 = "Rhohei";
        }
        HBases = GameObject.FindGameObjectsWithTag("HomeBase");
        if (HBases != null)
        {
            foreach (GameObject obj in HBases)
            {
                EBase EB = obj.GetComponent<EBase>();
                if (EB.Ally == "BLUE")
                {
                    if (this.CompareTag("RED"))
                    {
                        EHomeBase = obj;
                    }
                }
                else if (EB.Ally == "RED")
                {
                    if (this.CompareTag("BLUE"))
                    {
                        EHomeBase = obj;
                    }
                }
            }
        }
        if (Rb.isKinematic)
        {
            Rb.isKinematic = false;
        }
    }

    // Update is called once per frame
    async UniTask Update()
    {
        //Debug.Log(EnemyTag01 + " " + EnemyTag02);
        if (CommandMode == 0)
        {
            await Stand();
        }
        else if (CommandMode == 1)
        {
            await mBaseAttack();
        }
        else if (CommandMode == 2)
        {
            await HomeBaseAttack();
        }
        else if(CommandMode == 3)
        {
            await TrackEnemy();
        }
        else if (CommandMode == 4)
        {
            await Medic();
        }
        else if (CommandMode == 5)
        {
            await Battery();
        }
        SliderHP();
        ChKDead();
        await SelectIf();
        await GroundRay();
    }
    async UniTask TrackEnemy()
    {
        if (isGround)
        {
            await EnemySerch();
            if (EList.Count > 0)
            {
                dis = Vector3.Distance(Rb.position, EList[0].transform.position);
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
                Flag03 = true;
                BattleFlag = false;
                if (!Rb.isKinematic)
                {
                    Rb.isKinematic = true;
                }
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (NMA.enabled)
                {
                    NMA.enabled = false;
                }
            }
        }       
    }
    async UniTask Stand()
    {
        await EnemySerch();
        if (isGround)
        {
            if (Flag02)
            {
                StandPos = Rb.position;
                Flag02 = false;
            }
            if (EList.Count > 0)
            {
                dis = Vector3.Distance(Rb.position, EList[0].transform.position);
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
                Flag03 = true;
                BattleFlag = false;
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                dis = Vector3.Distance(Rb.position, StandPos);
                if (dis > 2f)
                {
                    if (!Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", true);
                    }
                    if (Rb.isKinematic)
                    {
                        Rb.isKinematic = false;
                    }
                    if (!NMA.enabled)
                    {
                        StartCoroutine("NMAOn");
                    }
                    if (NMA.enabled)
                    {
                        if (NMA.pathStatus != NavMeshPathStatus.PathInvalid)
                        {
                            NMA.SetDestination(StandPos);
                        }
                    }
                }
                else
                {
                    if (Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", false);
                    }
                    if (NMA.enabled)
                    {
                        NMA.enabled = false;
                    }
                }
            }
        }
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
    async UniTask mBaseAttack()
    {
        await GetmBList();
        await EnemySerch();
        //Debug.Log(mBList.Count.ToString() + " " + isGround.ToString() + " " + EList.Count.ToString());
        if (mBList.Count > 0)
        {
            dis_m = Vector3.Distance(mBList[0].transform.position, Rb.position);
            if (!Flag02)
            {
                Flag02 = true;
            }
            if (EList.Count > 0)
            {
                dis = Vector3.Distance(Rb.position, EList[0].transform.position);
                //Debug.Log(dis);
                if (dis > EnemyAttackDis)
                {
                    await AttackMove();
                }
                else
                {
                    await Attack();
                }
            }
            else if (mBList.Count > 0 && dis_m > mBIntervalDis)
            {
                await Move();
            }
            else
            {
                Flag03 = true;
                BattleFlag = false;
                if (NMA.enabled)
                {
                    NMA.enabled = false;
                }
                if (!Rb.isKinematic)
                {
                    Rb.isKinematic = true;
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
            }
        }
        else
        {
            CommandMode = 2;
        }
    }
    async UniTask Attack()
    {
        if (isGround)
        {
            Flag03 = false;
        }
        if (!Flag03)
        {
            if (!Rb.isKinematic)
            {
                Rb.isKinematic = true;
            }
            transform.LookAt(new Vector3(EList[0].transform.position.x,Rb.position.y,EList[0].transform.position.z));
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
                Anim.SetBool("Shot", true);
            }
            if (!Shooter.activeSelf)
            {
                Shooter.SetActive(true);
                BattleFlag = true;
            }
        }
    }
    async UniTask AttackMove()
    {
        if (Shooter.activeSelf)
        {
            Shooter.SetActive(false);
        }
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
    async UniTask Move()
    {
        if (isGround)
        {
            dis = Vector3.Distance(Rb.position, mBList[0].transform.parent.position);
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
            if (!NMA.enabled && dis > mBIntervalDis)
            {
                StartCoroutine("NMAOn");
            }
            else
            {
                if (dis > mBIntervalDis)
                {
                    if (NMA.pathStatus != NavMeshPathStatus.PathInvalid)
                    {
                        NMA.SetDestination(mBList[0].transform.parent.position);
                    }
                    else
                    {
                        Debug.Log("&&");
                    }
                }
            }
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
        foreach (GameObject obj in mBs)
        {
            mBList.Add(obj);
        }
        foreach (GameObject obj in EBs)
        {
            mBList.Add(obj);
        }
        if (mBList.Count > 1)
        {
            mBList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
        }
    }
    async UniTask HomeBaseAttack()
    {
        if (EHomeBase != null)
        {
            dis_H = Vector3.Distance(Rb.position, EHomeBase.transform.position);
            await EnemySerch();
            if (EList.Count > 0 && dis_H > EnemySearchDis)
            {
                dis = Vector3.Distance(Rb.position, EList[0].transform.position);                
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
                if (isGround)
                {
                    dis = Vector3.Distance(transform.position, EHomeBase.transform.position);
                    if (dis > EHB_dis)
                    {
                        Flag03 = true;
                        BattleFlag = false;
                        if (Shooter.activeSelf)
                        {
                            Shooter.SetActive(false);
                        }
                        if (Anim.GetBool("Shot"))
                        {
                            Anim.SetBool("Shot", false);
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
                    else
                    {
                        transform.LookAt(EHomeBase.transform.position);
                        if (isGround)
                        {
                            Flag03 = false;
                        }
                        if (!Flag03)
                        {
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
                                Anim.SetBool("Shot", true);
                            }
                            Shooter.SetActive(true);
                            BattleFlag = true;
                        }
                    }
                }
            }
        }
    }
    async UniTask SelectIf()
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
    async UniTask EnemySerch()
    {
        EList.Clear();
        E01s = GameObject.FindGameObjectsWithTag(EnemyTag01);
        E02s = GameObject.FindGameObjectsWithTag(EnemyTag02);
        E03s = GameObject.FindGameObjectsWithTag(EnemyTag03);
        foreach (GameObject obj in E01s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < EnemySearchDis)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in E02s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < EnemySearchDis)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in E03s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < EnemySearchDis)
            {
                CPU cpu = obj.GetComponent<CPU>();
                Fighter fgt = obj.GetComponent<Fighter>();
                if (cpu != null)
                {
                    if (!cpu.Flying)
                    {
                        EList.Add(obj);
                    }
                }
                if (fgt != null)
                {
                    if (!fgt.Flying)
                    {
                        EList.Add(obj);
                    }
                }
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
    public async UniTask SetTag()
    {
        if (UnitType == "missile")
        {
            if (this.CompareTag("BLUE"))
            {
                AUnitTag01 = "Bhohei";
                EBaseTag = "RminiBase";
                EnemyTag01 = "RPlayer";
                EnemyTag02 = "Empty";
                EnemyTag03 = "Empty";
            }
            else
            {
                AUnitTag01 = "Rhohei";
                EBaseTag = "BminiBase";
                EnemyTag01 = "BPlayer";
                EnemyTag02 = "Empty";
                EnemyTag03 = "Empty";
            }
        }
        else
        {
            if (this.CompareTag("BLUE"))
            {
                AUnitTag01 = "Bhohei";
                EBaseTag = "RminiBase";
                EnemyTag01 = "RED";
                EnemyTag02 = "Rhohei";
                EnemyTag03 = "RPlayer";
            }
            else
            {
                AUnitTag01 = "Rhohei";
                EBaseTag = "BminiBase";
                EnemyTag01 = "BLUE";
                EnemyTag02 = "Bhohei";
                EnemyTag03 = "BPlayer";
            }
        }
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
    async UniTask Battery()
    {
        if (isGround)
        {
            if (!Rb.isKinematic)
            {
                Rb.isKinematic = true;
            }
            await EnemySerch();
            await TarPSearch();
            if (PList.Count > 0)
            {
                transform.LookAt(new Vector3(PList[0].transform.position.x,Rb.position.y,PList[0].transform.position.z));
                if (!Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", true);
                }
                if (!GenMissile.activeSelf)
                {
                    GenMissile.SetActive(true);
                }
            }
            else if (EList.Count > 0)
            {
                transform.LookAt(EList[0].transform.position);
                if (!Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", true);
                }
                if (!Shooter.activeSelf)
                {
                    Shooter.SetActive(true);
                }
            }
            else
            {
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (GenMissile.activeSelf)
                {
                    GenMissile.SetActive(false);
                }
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
            }
        }
    }
    async UniTask TarPSearch()
    {
        PList.Clear();
        P01s = GameObject.FindGameObjectsWithTag(EnemyTag03);
        foreach (GameObject obj in P01s)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < AntiAircraftDis)
            {
                CPU cpu = obj.GetComponent<CPU>();
                Fighter fgt = obj.GetComponent<Fighter>();
                if (cpu != null)
                {
                    if (cpu.Flying)
                    {
                        PList.Add(obj);
                    }
                }
                if (fgt != null)
                {
                    if (fgt.Flying)
                    {
                        PList.Add(obj);
                    }
                }
            }
        }
        if (PList.Count > 1)
        {
            PList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
        }
    }
    async UniTask Medic()
    {
        await AllySearch();
        if (AList.Count > 0)
        {
            dis = Vector3.Distance(Rb.position, AList[0].transform.position);
            if (dis < MedecineDis)
            {
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", true);
                }
                if (NMA.enabled)
                {
                    NMA.enabled = false;
                }
                if (!Shooter.activeSelf)
                {
                    Shooter.SetActive(true);
                }
                Warrior Wr = AList[0].GetComponent<Warrior>();
                Soldier So = AList[0].GetComponent<Soldier>();
                if (Wr != null)
                {
                    Wr.HitPoint += 0.1f * HealSpeed * Time.deltaTime;
                }
                if (So != null)
                {
                    So.HitPoint += 0.1f * HealSpeed * Time.deltaTime;
                }
                Flag04 = true;
            }
            else
            {
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Flag04)
                {
                    StartCoroutine("NMAOn");
                    Flag04 = false;
                }
                if (!Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", true);
                }
                if (NMA.enabled)
                {
                    NMA.SetDestination(AList[0].transform.position);
                }
            }
        }
        else
        {
            if (Anim.GetBool("Run"))
            {
                Anim.SetBool("Run", false);
            }
            if (Anim.GetBool("Shot"))
            {
                Anim.SetBool("Shot", false);
            }
            if (NMA.enabled)
            {
                NMA.enabled = false;
            }
            if (Shooter.activeSelf)
            {
                Shooter.SetActive(false);
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
                //Debug.Log("LLL");
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
    private async UniTask AllySearch()
    {
        AList.Clear();
        AUs01 = GameObject.FindGameObjectsWithTag(AUtag1);
        AUs02 = GameObject.FindGameObjectsWithTag(AUtag2);
        foreach (GameObject obj in AUs01)
        {
            if (obj != this.gameObject)
            {
                dis = Vector3.Distance(Rb.position, obj.transform.position);
                if (dis < AllyCutDis)
                {
                    Warrior Wr = obj.GetComponent<Warrior>();
                    if (Wr != null)
                    {
                        if (Wr.HitPoint < Wr.MaxHitPoint)
                        {
                            AList.Add(obj);
                        }
                    }
                }
            }
        }
        foreach (GameObject obj in AUs02)
        {
            dis = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < AllyCutDis)
            {
                Soldier So = obj.GetComponent<Soldier>();
                if (So != null)
                {
                    if (So.HitPoint < So.MaxHitPoint)
                    {
                        AList.Add(obj);
                    }
                }
            }
        }
        if (AList.Count > 1)
        {
            AList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
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
