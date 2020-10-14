using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class MiniBase : MonoBehaviour
{
    [SerializeField]
    private GameObject Flag;
    public GameObject Tag;
    //private int point;
    [SerializeField]
    private int b_point;
    [SerializeField]
    private int r_point;
    [System.NonSerialized]
    public bool Flag01 = true;
    [SerializeField]
    private Material bmat;
    [SerializeField]
    private Material rmat;
    private Renderer Rend;
    private int mode = 0;
    private List<Transform> mBList = new List<Transform>();
    private List<Transform> BBList = new List<Transform>();
    private List<Transform> RBList = new List<Transform>();
    [SerializeField]
    private string BmBaseTag = "BminiBase";
    [SerializeField]
    private string RmBaseTag = "RminiBase";
    [SerializeField]
    private string BmBaseMTag = "BMiniBase_Minus";
    [SerializeField]
    private string RmBaseMTag = "RMiniBase_Minus";
    private GameObject[] EBases;
    public CpuMiniBaseUnitList NCMU;
    public CpuMiniBaseUnitList CMU;
    [System.NonSerialized]
    public string BaseString = "minibase";
    private int NmBM_priority;
    [SerializeField]
    private string Unit01;
    [SerializeField]
    private string Unit02;
    [SerializeField]
    private string Unit03;
    [SerializeField]
    private string Unit04;
    private List<Transform> BZList = new List<Transform>();
    [SerializeField]
    private float Interval = 8f;
    private float BZTimer;
    [SerializeField]
    private int[] B_CommandArray;
    [SerializeField]
    private int[] R_CommandArray;
    [SerializeField]
    private int[] B_BZCommandArray;
    [SerializeField]
    private int[] R_BZCommandArray;
    private int B_Commandint = 0;
    private int B_BZCommandint = 0;
    private int R_Commandint = 0;
    private int R_BZCommandint = 0;
    [SerializeField]
    private float ZoneFloat = 300;
    private GameObject[] Units01;
    private GameObject[] Units02;
    private GameObject[] Units03;
    private GameObject[] Units04;
    [SerializeField]
    private Slider Sd1;
    [SerializeField]
    private Slider Sd2;
    [SerializeField]
    private bool PFlag = true;
    // Start is called before the first frame update
    async UniTask Start()
    {
        GameObject[] mBases = GameObject.FindGameObjectsWithTag("minibase");
        foreach (GameObject obj in mBases)
        {
            mBList.Add(obj.transform);
        }
        mBList.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(this.gameObject.transform.position, b.transform.position));
        });
        mBList.RemoveAt(0);
        Rend = Flag.GetComponent<Renderer>();
        CMU = Tag.GetComponent<CpuMiniBaseUnitList>();
        BZTimer = Interval;
        Sd1.value = 1f;
        Sd2.value = 1f;
    }
    // Update is called once per frame
    async UniTask Update()
    {
        if (Flag01)
        {
            TagFalse();
        }/*
        if(BZTimer <= 0)
        {
            GetBattleZone();
            BZTimer = Interval;
        }
        BZTimer -= 0.1f * 60 * Time.deltaTime;*/
        MinusPoint();
    }
    bool GetBattleZone()
    {
        BZList.Clear();
        Units01 = GameObject.FindGameObjectsWithTag(Unit01);
        Units02 = GameObject.FindGameObjectsWithTag(Unit02);
        Units03 = GameObject.FindGameObjectsWithTag(Unit03);
        Units04 = GameObject.FindGameObjectsWithTag(Unit04);
        foreach (GameObject obj in Units01)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            Soldier So = obj.GetComponent<Soldier>();
            if (Wr != null)
            {
                if (Wr.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
            if (So != null)
            {
                if (So.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
        }
        foreach (GameObject obj in Units02)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            Soldier So = obj.GetComponent<Soldier>();
            if (Wr != null)
            {
                if (Wr.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
            if (So != null)
            {
                if (So.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
        }
        foreach (GameObject obj in Units03)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            Soldier So = obj.GetComponent<Soldier>();
            if (Wr != null)
            {
                if (Wr.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
            if (So != null)
            {
                if (So.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
        }
        foreach (GameObject obj in Units04)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            Soldier So = obj.GetComponent<Soldier>();
            if (Wr != null)
            {
                if (Wr.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
            if (So != null)
            {
                if (So.BattleFlag)
                {
                    BZList.Add(obj.transform);
                }
            }
        }
        if (BZList.Count > 1)
        {
            BZList.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.gameObject.transform.position, b.transform.position));
            });
        }
        if (BZList.Count > 0)
        {
            float dis = Vector3.Distance(BZList[0].position, transform.position);
            if(dis < ZoneFloat)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        /*if(BZList.Count > 0)
        {
            float dis = Vector3.Distance(BZList[0].position, transform.position);
            //Debug.Log(dis);
        }*/
    }
    void MinusPoint()
    {
        if (Tag.CompareTag(BmBaseTag))
        {
            if (r_point > 0)
            {
                Tag.tag = BmBaseMTag;
                mode = 3;
            }
        }
        if (Tag.CompareTag(RmBaseTag))
        {
            if (b_point > 0)
            {
                Tag.tag = RmBaseMTag;
                mode = 4;
            }
        }
        Sd1.value = (float)b_point / 4f;
        Sd2.value = (float)r_point / 4f;
    }
    void TagFalse()
    {
        if (b_point >= 4)
        {
            //mBList.Remove(transform);
            BBList.Add(transform);
            BaseString = BmBaseTag;
            Tag.tag = BmBaseTag;
            Rend.material = bmat;
            mode = 1;
            Flag01 = false;

        }
        if (r_point >= 4)
        {
            //mBList.Remove(transform);
            RBList.Add(transform);
            BaseString = RmBaseTag;
            Tag.tag = RmBaseTag;
            Rend.material = rmat;
            mode = 2;
            Flag01 = false;

        }
    }
    public Vector3 ReBzV3()
    {
        GetBattleZone();
        if (BZList.Count > 0)
        {
            return BZList[0].position;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
    public int ReCommand()
    {
        if (Tag.CompareTag("BminiBase")|| Tag.CompareTag("BMiniBase_Minus"))
        {
            if (!GetBattleZone())
            {
                int i = B_CommandArray[B_Commandint];
                B_Commandint++;
                if(B_CommandArray.Length <= B_Commandint)
                {
                    B_Commandint = 0;
                }
                return i;
            }
            else
            {
                int i = B_BZCommandArray[B_BZCommandint];
                B_BZCommandint++;
                if (B_BZCommandArray.Length <= B_BZCommandint)
                {
                    B_Commandint = 0;
                }
                return i;
            }
        }
        else if (Tag.CompareTag("RminiBase") || Tag.CompareTag("RMiniBase_Minus"))
        {
            if (!GetBattleZone())
            {
                int i = R_CommandArray[R_Commandint];
                R_Commandint++;
                if (R_CommandArray.Length <= R_Commandint)
                {
                    R_Commandint = 0;
                }
                return i;
            }
            else
            {
                int i = R_BZCommandArray[R_BZCommandint];
                R_BZCommandint++;
                if (R_BZCommandArray.Length <= R_BZCommandint)
                {
                    R_Commandint = 0;
                }
                return i;
            }
        }
        else
        {
            return 0;
        }
    }
    public CpuMiniBaseUnitList RetrunCMU()
    {
        return CMU;
    }
    public CpuMiniBaseUnitList ReNmBM(int i)
    {
        NCMU = mBList[i].gameObject.GetComponent<CpuMiniBaseUnitList>();
        return NCMU;
    }
    private async UniTask OnCollisionEnter(Collision collision)
    {
        if (!Flag01)
        {
            Flag01 = true;
        }
        if (collision.gameObject.CompareTag("Bhohei"))
        {
            if (mode != 1)
            {
                b_point += 1;
                if (r_point > 0)
                {
                    r_point -= 1;
                }
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Rhohei"))
        {
            if (mode != 2)
            {
                r_point += 1;
                if (b_point > 0)
                {
                    b_point -= 1;
                }
                Destroy(collision.gameObject);
            }
        }
    }
    private async UniTask OnTriggerEnter(Collider other)
    {
        if (Tag.CompareTag(BmBaseTag) || Tag.CompareTag(BmBaseMTag))
        {
            if (other.CompareTag("BPlayer"))
            {
                Fighter jiki = other.GetComponent<Fighter>();
                if (jiki != null)
                {
                    jiki.StayBase = true;
                }
                CPU cpu = other.gameObject.GetComponent<CPU>();
                if (cpu != null)
                {
                    if (cpu.Type == "BLUE")
                    {
                        cpu.StayBase = true;
                    }
                }
            }
            else if (other.CompareTag("RPlayer"))
            {
                Fighter jiki = other.GetComponent<Fighter>();
                if (jiki != null)
                {
                    jiki.Obs = true;
                }
                CPU cpu = other.gameObject.GetComponent<CPU>();
                if (cpu != null)
                {
                    if (cpu.Type == "BLUE")
                    {
                        cpu.Obs = true;
                    }
                }
            }
        }
        else if (Tag.CompareTag(RmBaseTag) || Tag.CompareTag(RmBaseMTag))
        {
            if (other.CompareTag("RPlayer"))
            {
                Fighter jiki = other.GetComponent<Fighter>();
                if (jiki != null)
                {
                    jiki.StayBase = true;
                }
                CPU cpu = other.gameObject.GetComponent<CPU>();
                if (cpu != null)
                {
                    if (cpu.Type == "RED")
                    {
                        cpu.StayBase = true;
                    }
                    if (PFlag)
                    {
                        cpu.Pursuit_Flag = true;
                    }
                    else
                    {
                        cpu.Pursuit_Flag = false;
                    }
                }
            }
            else if (other.CompareTag("BPlayer"))
            {
                Fighter jiki = other.GetComponent<Fighter>();
                if (jiki != null)
                {
                    jiki.Obs = true;
                }
                CPU cpu = other.gameObject.GetComponent<CPU>();
                if (cpu != null)
                {
                    if (cpu.Type == "BLUE")
                    {
                        cpu.Obs = true;
                    }
                    if (PFlag)
                    {
                        cpu.Pursuit_Flag = true;
                    }
                    else
                    {
                        cpu.Pursuit_Flag = false;
                    }
                }
            }
        }
        else
        {
            Fighter jiki = other.GetComponent<Fighter>();
            if (jiki != null)
            {
                jiki.Obs = true;
            }
            CPU cpu = other.gameObject.GetComponent<CPU>();
            if (cpu != null)
            {
                cpu.Obs = true;
                if (PFlag)
                {
                    cpu.Pursuit_Flag = true;
                }
                else
                {
                    cpu.Pursuit_Flag = false;
                }
            }
        }
    }
    private async UniTask OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BPlayer") || other.CompareTag("RPlayer"))
        {
            Fighter jiki = other.GetComponent<Fighter>();
            if (jiki != null)
            {
                jiki.StayBase = false;
                jiki.Obs = false;
            }
            CPU cpu = other.gameObject.GetComponent<CPU>();
            if (cpu != null)
            {
                cpu.StayBase = false;
                cpu.Obs = false;
            }
        }
    }
}
