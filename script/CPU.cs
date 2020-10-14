using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEditorInternal;

public class CPU : MonoBehaviour
{
    [System.NonSerialized]
    public bool Flying = true;
    public string Type;
    [SerializeField]
    private GameObject Box;
    [SerializeField]
    private GameObject ReleasePos;
    [SerializeField]
    private GameObject Shooter;
    [System.NonSerialized]
    public bool StayBase = true;
    private bool isGround = false;
    //Flag08 and Flag11 *行動用フラグ
    // LookAt(Tarpos) on and off
    private bool Flag01 = true;
    //TakeOff and Landing
    private bool Flag02 = true;
    private bool Flag03 = true;
    //対本拠地攻撃迎撃
    private bool Flag04 = true;
    //行動用Build
    private bool Flag05 = true;
    private bool Flag06 = true;
    //BuildAction
    private bool Flag07 = true;
    private bool Flag08 = true;
    //追撃行動用
    private bool Flag09 = true;
    //ThrowAndRetrunBase
    private bool Flag10 = true;
    private bool Flag11 = true;
    private bool Flag12 = true;
    private bool Flag13 = true;
    //追撃行動用
    private bool Flag14 = true;
    //地上追撃用
    private bool Flag15 = true;
    //基地防御
    private bool Flag16 = true;
    private List<GameObject> AHEList = new List<GameObject>();
    private int AHETarMode = 0;
    private float speed;
    [SerializeField]
    private float run_speed;
    [SerializeField]
    private float fly_speed;
    [SerializeField]
    private float fallspeed;
    private Animator Anim;
    private float Leap_t;
    private Quaternion rot;
    //private CharacterController CC;
    private Rigidbody Rb;
    private float v;
    private int MoveD = 0;
    private bool LandingFlag = false;
    private int TakeOffMode = 0;
    private float TakeOff_y;
    [SerializeField]
    private float DownForce;
    private float Ground_y;
    private List<GameObject> mBList = new List<GameObject>();
    //private List<Transform> EBList = new List<Transform>();
    private List<GameObject> ABList = new List<GameObject>();
    private string mBaseTag = "minibase";
    private string EBaseTag;
    private string ABaseTag;
    private string EMBtag;
    private string ABMtag;
    [SerializeField]
    private GameObject EBASE;
    [SerializeField]
    private GameObject ABASE;
    private string EUnit01;
    private string EUnit02;
    [SerializeField]
    private float Release_dis;
    private Vector3 Tarpos;
    [SerializeField]
    private GameObject directioner;
    [SerializeField]
    private float BuildTime = 20;
    private float BuildTimer;
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private float TankBuildTime;
    [SerializeField]
    private float TankCost;
    [SerializeField]
    private GameObject Hohei;
    [SerializeField]
    private float SolBuildTime;
    [SerializeField]
    private float SolCost;
    [SerializeField]
    private GameObject Missile;
    [SerializeField]
    private float MissBuildTime;
    [SerializeField]
    private float MisCost;
    [SerializeField]
    private GameObject Acar;
    [SerializeField]
    private float AcarBuildTime;
    [SerializeField]
    private float AcCost;
    [SerializeField]
    private GameObject Medic;
    [SerializeField]
    private float MediBuildTime;
    [SerializeField]
    private float MedCost;
    [SerializeField]
    private GameObject Battery;
    [SerializeField]
    private float BattBuildTime;
    [SerializeField]
    private float BattCost;
    [System.NonSerialized]
    public List<string> BuildList = new List<string>();
    private int BuildMode = 0;
    private int TarMode = 0;
    private Vector3 mBcenter;
    [SerializeField]
    private float st_dis = 4;
    //[SerializeField]
    private int CommandMode = 16;
    private int solcount = 0;
    private int WrCommand = 0;
    private int ChargeTankint = 0;
    private bool Baseposint = false;
    [SerializeField]
    private float unit_dis = 18f;
    [SerializeField]
    private float BattleDis;
    //private float GdBt_dis;
    [SerializeField]
    private float CommandChangeUnitDis = 80;
    [SerializeField]
    private float CommandChangeTime = 8f;
    private float CommandChangeTimer;
    private List<GameObject> AUnitList = new List<GameObject>();
    private string AwayLunchUnit = null;
    private MiniBase MB;
    private EBase EB;
    //private int CommandStack;
    [SerializeField]
    private float baseoutdis = 150;
    private float BoxHP;
    [SerializeField]
    private float MaxHitPoint = 30;
    [System.NonSerialized]
    public float HitPoint;
    [SerializeField]
    private Slider SD;
    private float CCTimer;
    private List<GameObject> EList = new List<GameObject>();
    //private BoxCollider BC;
    [SerializeField]
    private float GroundBattleDis;
    [SerializeField]
    private float GroundBattleTime = 300;
    private float GBT;
    private float dis;
    private GameObject[] mBs;
    private GameObject[] EBs;
    private GameObject[] ABESES;
    private GameObject[] EBMs;
    private GameObject[] ABMs;
    private GameObject[] units;
    private GameObject[] EUNITSA;
    private GameObject[] EUNITSB;
    private GameObject[] EUNITSC;
    private GameObject[] AHEUNITS;
    private Vector3 BZpos = new Vector3(0, 0, 0);
    [SerializeField]
    private float MissDef = 0.6f;
    private List<int> BoxLookInt = new List<int>();
    private Vector3 startpos;
    [SerializeField]
    private float ResPownTime = 300f;
    private float ResPownTimer;
    private string EPlayerTag;
    private GameObject[] EPlayers;
    private GameObject TargetEP;
    private float EPdis = 100000000000;
    private float TEPdis;
    [SerializeField]
    private float EPAttackDis = 300;
    private Ray ray;
    private RaycastHit hit;
    //[SerializeField]
    private float raydist = 1.5f;
    private GameObject[] AB1;
    private GameObject[] AB2;
    private List<GameObject> AMBList = new List<GameObject>();
    private Fighter Fht;
    private CPU Ecp;
    private float TakeOff_t;
    [SerializeField]
    private float MaxOil;
    private float Oil;
    [SerializeField]
    private Slider OilSld;
    private GameObject[] solarray;
    private GameObject[] unitarray;
    private List<GameObject> TankTriList = new List<GameObject>();
    private string TankTag;
    private string SolTag;
    [SerializeField]
    private float SelectDis = 15;
    [SerializeField]
    private float basic_income = 0.05f;
    [SerializeField]
    private float mono_income = 0.05f;
    private float float_income;
    [System.NonSerialized]
    public int int_income;
    //[SerializeField]
    //private TextMeshProUGUI money_txt;
    private GameObject[] bbs;
    private GameObject[] bmbs;
    [SerializeField]
    private GameObject Proper;
    private float i_dis;
    [SerializeField]
    private float Pursuit_Limit_Length;
    private int AcCount = 0;
    [SerializeField]
    private float ESearchLength;
    [SerializeField]
    private float WaitTime = 400;
    private float WaitTimer;
    private bool BFlag = false;
    private int DefCount;
    [System.NonSerialized]
    public bool Pursuit_Flag = true;
    private GameObject mB01 = null;
    private GameObject aB01 = null;
    [System.NonSerialized]
    public bool Obs;
    private List<bool> FLagSP = new List<bool>();
    private List<Material> MatList = new List<Material>();
    private List<GameObject> ChGList = new List<GameObject>();
    //private Material FlyOptMat;
    [SerializeField]
    private Material AlphaZeroMat;
    //private bool CtlFlag = true;
    private int DeadMode = 0;
    private float[] DeadTime = new float[2] { 30, 30 };
    private float DeadTimer;
    private bool CtlFlag = true;
    [System.NonSerialized]
    public bool Life = true;
    // Start is called before the first frame update
    async UniTask Start()
    {
        //ChGList.Clear();
        /*foreach (Transform ts in this.gameObject.transform)
        {
            ChGList.Add(ts.gameObject);
        }*/
        await GetChil(this.gameObject);
        foreach (GameObject obj in ChGList)
        {
            Renderer Rnd = obj.GetComponent<Renderer>();
            if (Rnd != null)
            {
                MatList.Add(Rnd.material);
            }
        }/*
        if (Proper != null)
        {
            Renderer Rndf = Proper.GetComponent<Renderer>();
            FlyOptMat = Rndf.material;
        }*/
        //GdBt_dis = BattleDis;
        SD.value = 1f;
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        //CC = GetComponent<CharacterController>();
        Box.SetActive(false);
        Shooter.SetActive(false);
        Rb.isKinematic = true;
        TakeOff_y = transform.position.y;
        rot = transform.rotation;
        speed = fly_speed;
        MoveD = 5;
        BuildTimer = BuildTime;
        EB = ABASE.GetComponent<EBase>();
        HitPoint = MaxHitPoint;
        //BC = GetComponent<BoxCollider>();
        GBT = GroundBattleTime;
        startpos = Rb.position;
        ResPownTimer = ResPownTime;
        TankTag = Tank.tag;
        SolTag = Hohei.tag;
        if (this.CompareTag("RPlayer"))
        {
            EPlayerTag = "BPlayer";
            ABaseTag = "RminiBase";
            ABMtag = "RMiniBase_Minus";
            EBaseTag = "BminiBase";
            EMBtag = "BMiniBase_Minus";
        }
        else if (this.CompareTag("BPlayer"))
        {
            EPlayerTag = "RPlayer";
            ABaseTag = "BminiBase";
            ABMtag = "BMiniBase_Minus";
            EBaseTag = "RminiBase";
            EMBtag = "RMiniBase_Minus";
        }
        if (this.CompareTag("BPlayer"))
        {
            EUnit01 = "RED";
            EUnit02 = "Rhohei";
        }
        else if (this.CompareTag("RPlayer"))
        {
            EUnit01 = "BLUE";
            EUnit02 = "Bhohei";
        }
        Oil = MaxOil;
        for (int i = 0; i < 8; i++)
        {
            FLagSP.Add(false);
        }
        await UnitStart();
    }
    // Update is called once per frame
    async UniTask Update()
    {
        //Debug.Log(isGround);

        //await KeyDownDeath();
        await Income();
        await SliderHP();
        await BuildAction();
        i_dis = Vector3.Distance(Rb.position, new Vector3(EBASE.transform.position.x, Rb.position.y, EBASE.transform.position.z));
        if (i_dis > Pursuit_Limit_Length && Pursuit_Flag)
        {
            await TarGetEPlayer();
        }
        await DetectionHomeBaseAttacker();
        await GroundRay();
        await ChargeOil();
        Debug.Log(CommandMode.ToString() + " " + isGround.ToString() + " " + Oil.ToString() + " " + this.gameObject.tag);
        if (CommandMode == 0)
        {
            //BuildandFWDmB();
            await CarrySol();
        }
        else if (CommandMode == 1)
        {
            await MinmalGard();
        }
        else if (CommandMode == 2)
        {
            await ReleaseSoldier();
        }
        else if (CommandMode == 3)
        {
            await HeadFrontBase();
        }
        else if (CommandMode == 4)
        {
            await GardABASE();
        }
        else if (CommandMode == 5)
        {
            await CurrentBase();
        }
        else if (CommandMode == 6)
        {
            await ChargeTank(1);
        }
        else if (CommandMode == 7)
        {
            await UnitCommanChange(1);
        }
        else if (CommandMode == 8)
        {
            await ReturnABase();
        }
        else if (CommandMode == 9)
        {
            await GardMissile();
        }
        else if (CommandMode == 10)
        {
            //Do nothing
        }
        else if (CommandMode == 11)
        {
            await GroundMoveAttack();
        }
        else if (CommandMode == 12)
        {
            await WaitReSpown();
        }
        else if (CommandMode == 13)
        {
            await PursuitEP();
        }
        else if (CommandMode == 14)
        {
            await ChangeCBase();
        }
        else if (CommandMode == 15)
        {
            await MoveNearPmB();
        }
        else if (CommandMode == 16)
        {
            await ContinuousAttack(2);
        }
        else if (CommandMode == 17)
        {
            await UnitCommanChange(2);
        }
        else if (CommandMode == 18)
        {
            await ChargeTank(2);
        }
        else if (CommandMode == 19)
        {
            await GardmBTank();
        }
        else if (CommandMode == 20)
        {
            await WaitCommand();
        }
        else if (CommandMode == 21)
        {
            await ContinuousAttack(1);
        }
        else if (CommandMode == 22)
        {
            await InterceptionHomeBaseAttack();
        }
        else if (CommandMode == 23)
        {
            await CurrySolNearMB();
        }
        else if (CommandMode == 24)
        {
            await PursuitAHE();
        }
    }
    async UniTask SearchSPU()
    {
        FLagSP.Clear();
        for (int i = 0; i < 8; i++)
        {
            FLagSP.Add(false);
        }
        GameObject[] AUs = GameObject.FindGameObjectsWithTag(Tank.tag);
        foreach (GameObject obj in AUs)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            if (Wr.UnitType == "goliath")
            {
                FLagSP[0] = true;
            }
            else if (Wr.UnitType == "momiji")
            {
                FLagSP[0] = true;
            }
            else if (Wr.UnitType == "hatate")
            {
                FLagSP[1] = true;
            }
            else if (Wr.UnitType == "luna")
            {
                FLagSP[0] = true;
            }
            else if (Wr.UnitType == "sunny")
            {
                FLagSP[1] = true;
            }
            else if (Wr.UnitType == "cirno")
            {
                FLagSP[2] = true;
            }
            else if (Wr.UnitType == "crown")
            {
                FLagSP[3] = true;
            }
            else if (Wr.UnitType == "ete")
            {
                FLagSP[4] = true;
            }
            else if (Wr.UnitType == "dai1")
            {
                FLagSP[5] = true;
            }
            else if (Wr.UnitType == "dai2")
            {
                FLagSP[6] = true;
            }
            else if (Wr.UnitType == "dai3")
            {
                FLagSP[7] = true;
            }
            else if (Wr.UnitType == "mayumi")
            {
                FLagSP[0] = true;
            }
            else if (Wr.UnitType == "marisa")
            {
                FLagSP[0] = true;
            }
            else if (Wr.UnitType == "aun")
            {
                FLagSP[1] = true;
            }
        }
    }
    async UniTask UnitStart()
    {
        Vector3 UnitPos = ABASE.transform.position + new Vector3(8, 0, -35);
        GameObject tank01 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Warrior Wr = tank01.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = ABASE.transform.position + new Vector3(-8, 0, -35);
        GameObject tank02 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank02.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = ABASE.transform.position + new Vector3(-35, 0, 8);
        GameObject tank03 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        if (Wr != null)
        {
            Wr = tank03.GetComponent<Warrior>();
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = ABASE.transform.position + new Vector3(-35, 0, -8);
        GameObject tank04 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank04.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = ABASE.transform.position + new Vector3(-35, 0, 0);
        GameObject tank05 = (GameObject)Instantiate(Missile, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank05.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "missile";
        }
        UnitPos = ABASE.transform.position + new Vector3(0, 0, -35);
        GameObject tank06 = (GameObject)Instantiate(Missile, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank06.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "missile";
        }
        UnitPos = ABASE.transform.position + new Vector3(35, 0, -35);
        GameObject tank07 = (GameObject)Instantiate(Medic, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank07.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 4;
            Wr.UnitType = "medic";
        }
    }
    async UniTask Income()
    {
        bbs = GameObject.FindGameObjectsWithTag(ABaseTag);
        bmbs = GameObject.FindGameObjectsWithTag(ABMtag);
        float_income += (((float)(bbs.Length + bmbs.Length) * mono_income) + basic_income) * 60 * Time.deltaTime;
        int_income = (int)float_income;
    }
    async UniTask WaitCommand()
    {
        if (Flag08)
        {
            WaitTimer = WaitTime;
            Flag08 = false;
        }
        WaitTimer -= 0.1f * 60 * Time.deltaTime;
        //Debug.Log(WaitTimer);
        if (WaitTimer < 0.0f)
        {
            Flag16 = true;
            Flag08 = true;
            CommandMode = 8;
        }
    }
    async UniTask ChangeCBase()
    {
        if (Flag08)
        {
            await GetABList();
            if (ABList.Count > 2)
            {
                ABList.Reverse();
                Transform TBase;
                int j;
                Transform ts01 = ABList[ABList.Count - 1].transform;
                Transform ts02 = ABList[ABList.Count - 2].transform;
                float dis0 = 1000000;
                dis = 0;
                foreach (GameObject ts in ABList)
                {
                    //Debug.Log(ts.position);
                    dis = Vector3.Distance(EBASE.transform.position, ts.transform.position);
                    if (dis < dis0)
                    {
                        dis0 = dis;
                        ts02 = ts01;
                        ts01 = ts.transform;
                    }
                }
                j = Random.Range(1, 3);
                if (j == 1)
                {
                    TBase = ts01;
                }
                else
                {
                    TBase = ts02;
                }
                //Debug.Log(TBase.position.ToString() + "H");
                Tarpos = new Vector3(TBase.position.x, transform.position.y, TBase.position.z);
            }
            Flag08 = false;
        }
        await FlyingFirst();
        await MoveDirection();
        //Debug.Log(Tarpos);
        dis = Vector3.Distance(Rb.position, Tarpos);
        if (dis < st_dis)
        {
            Flag08 = true;
            Flag16 = true;
            CommandMode = 5;
        }

    }
    async UniTask PursuitEP_G()
    {
        dis = Vector3.Distance(Rb.position, TargetEP.transform.position);
        if (dis < 30 && !isGround)
        {
            await Landing();
        }
        else if (isGround)
        {
            //StartCoroutine("PEG_ShotOn");
            Anim.SetBool("Shot", true);
            Shooter.SetActive(true);
            transform.LookAt(TargetEP.transform.position);
        }
        else
        {
            if (Flag01)
            {
                Flag01 = false;
            }
            directioner.transform.LookAt(TargetEP.transform.position);
            await FlyingFirst();
            await MoveDirection();
        }
    }
    async UniTask PursuitEP_F()
    {
        dis = Vector3.Distance(Rb.position, TargetEP.transform.position);
        if (dis < 15)
        {
            Anim.SetBool("Shot", true);
            Shooter.SetActive(true);
            transform.LookAt(TargetEP.transform.position);
        }
        else
        {
            if (Flag01)
            {
                Flag01 = false;
            }
            directioner.transform.LookAt(TargetEP.transform.position);
            await FlyingFirst();
            await MoveDirection();
        }
    }
    async UniTask PursuitEP()
    {
        if (Flag09)
        {
            GBT = 50f;
            Fht = null;
            Ecp = null;
            Fht = TargetEP.GetComponent<Fighter>();
            Ecp = TargetEP.GetComponent<CPU>();
            Flag09 = false;
        }
        if (Ecp != null)
        {
            if (Flag15 && GBT > 0.0f && !Ecp.StayBase && Oil > MaxOil / 3)
            {
                if (Fht != null)
                {
                    if (Fht.Flying)
                    {
                        await PursuitEP_F();
                    }
                    else
                    {
                        await PursuitEP_G();
                    }
                }
                if (Ecp != null)
                {
                    if (Ecp.Flying)
                    {
                        await PursuitEP_F();
                    }
                    else
                    {
                        await PursuitEP_G();
                    }
                }
            }
        }
        if (Fht != null)
        {
            if (Flag15 && GBT > 0.0f && !Fht.StayBase && Oil > MaxOil / 3)
            {
                if (Fht != null)
                {
                    if (Fht.Flying)
                    {
                        await PursuitEP_F();
                    }
                    else
                    {
                        await PursuitEP_G();
                    }
                }
                if (Ecp != null)
                {
                    if (Ecp.Flying)
                    {
                        await PursuitEP_F();
                    }
                    else
                    {
                        await PursuitEP_G();
                    }
                }
            }
        }
        GBT -= 0.1f * 60 * Time.deltaTime;
        Debug.Log(GBT);
        if (GBT <= 0.0f && Flying)
        {
            Flag09 = true;
            Flag08 = true;
            Flag11 = true;
            Flag01 = true;
            Flag15 = true;
            Flag16 = true;
            if (Shooter.activeSelf)
            {
                Shooter.SetActive(false);
            }
            if (Anim.GetBool("Shot"))
            {
                Anim.SetBool("Shot", false);
            }
            transform.rotation = rot;
            CommandMode = 8;
        }
        else if (GBT <= 0.0f && LandingFlag && !Flag15)
        {
            //Debug.Log("VVV");
            await TakeOff();
        }
        else if (GBT <= 0.0f && LandingFlag && Flag15)
        {
            if (Shooter.activeSelf)
            {
                Shooter.SetActive(false);
            }
            if (Anim.GetBool("Shot"))
            {
                Anim.SetBool("Shot", false);
            }
            TakeOffMode = 0;
            Flag02 = true;
            Flag15 = false;
        }

    }
    async UniTask TarGetEPlayer()
    {
        if (Flag14)
        {
            EPlayers = GameObject.FindGameObjectsWithTag(EPlayerTag);
            if (EPlayers.Length > 0)
            {
                foreach (GameObject obj in EPlayers)
                {
                    float EPdis2 = Vector3.Distance(Rb.position, obj.transform.position);
                    if (EPdis2 < EPdis)
                    {
                        EPdis = EPdis2;
                        TargetEP = obj;
                    }
                }
                EPdis = 100000000000;
            }
            if (TargetEP != null)
            {
                TEPdis = Vector3.Distance(Rb.position, TargetEP.transform.position);
                CPU ECPU = TargetEP.GetComponent<CPU>();
                Fighter EFG = TargetEP.GetComponent<Fighter>();
                if (ECPU != null)
                {
                    if (TEPdis < EPAttackDis && !Box.activeSelf && ECPU.Life)
                    {
                        Flag11 = true;
                        Flag08 = true;
                        Flag10 = false;
                        Flag16 = true;
                        CommandMode = 13;
                    }
                }
                if (EFG != null)
                {
                    if (TEPdis < EPAttackDis && !Box.activeSelf && EFG.Life)
                    {
                        Flag11 = true;
                        Flag08 = true;
                        Flag10 = false;
                        Flag16 = true;
                        CommandMode = 13;
                    }
                }
            }
            Flag14 = false;
        }
    }
    async UniTask WaitReSpown()
    {
        if (Flag08)
        {
            ResPownTimer = ResPownTime;
            Flag08 = false;
        }
        if (ResPownTimer <= 0.0f)
        {
            CommandMode = 5;
            Flag08 = true;
            Flag11 = true;
            Flag16 = true;
        }
        ResPownTimer -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask GroundMoveAttack()
    {
        Debug.Log(TarMode);
        if (!StayBase && !Obs && Flying && Box.activeSelf)
        {
            await Release();
        }
        await GetEList();
        if (Flag08)
        {
            GBT = GroundBattleTime;
            TarMode = 0;
            Flag08 = false;
        }
        if (EList.Count > 0 && TarMode != 3)
        {
            Tarpos = new Vector3(EList[0].transform.position.x, Rb.position.y, EList[0].transform.position.z);
        }
        //Debug.Log(Tarpos);
        dis = Vector3.Distance(Rb.position, Tarpos);
        if (dis > 50 && !Flying)
        {
            if (Flag11)
            {
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
            }
            await TakeOff();
        }
        if (TarMode == 0 && dis < st_dis + BattleDis && !StayBase)
        {
            await Landing();
            Flag02 = true;
            TarMode = 1;
        }
        else if (TarMode == 1 && dis < st_dis + GroundBattleDis && !Flying)
        {
            if (EList.Count > 0 && HitPoint > MaxHitPoint / 3)
            {
                if (EList[0].transform.position.y < Rb.position.y + 1 && EList[0].transform.position.y > Rb.position.y - 1)
                {
                    //Debug.Log("P");
                    transform.LookAt(EList[0].transform.position);
                    if (!Shooter.activeSelf)
                    {
                        Shooter.SetActive(true);
                    }
                    if (Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", false);
                    }
                    if (!Anim.GetBool("Shot"))
                    {
                        Anim.SetBool("Shot", true);
                    }
                    if (!Rb.isKinematic)
                    {
                        Rb.isKinematic = true;
                    }
                }
                else
                {
                    if (Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", false);
                    }
                }
            }
            else
            {
                GBT = -0.1f;
            }
        }
        else if (TarMode == 1 && Flying)
        {
            TarMode = 0;
        }
        else if (TarMode > 1 && Flying)
        {
            TarMode = 3;
            ABList.Clear();
            ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
            ABMs = GameObject.FindGameObjectsWithTag(ABMtag);
            foreach (GameObject ab in ABESES)
            {
                ABList.Add(ab);
            }
            foreach (GameObject ab in ABMs)
            {
                ABList.Add(ab);
            }
            ABList.Add(ABASE);
            ABList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
            Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
            dis = Vector3.Distance(Tarpos, Rb.position);
            if (dis < st_dis)
            {
                Flag01 = true;
                Flag08 = true;
                Flag16 = true;
                TarMode = 0;
                CommandMode = 8;
            }
            else
            {
                await FlyingFirst();
                await MoveDirection();
            }
        }
        else
        {
            if (!Flying)
            {
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
            }
            await FlyingFirst();
            await MoveDirection();
        }
        if (GBT < 0.0f || Oil < MaxOil / 2)
        {
            //Debug.Log("Flying" + " " + Flying.ToString());
            if (!Flying)
            {
                if (Flag11)
                {
                    if (Shooter.activeSelf)
                    {
                        Shooter.SetActive(false);
                    }
                    if (Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", false);
                    }
                    if (Anim.GetBool("Shot"))
                    {
                        Anim.SetBool("Shot", false);
                    }
                    if (Rb.isKinematic)
                    {
                        Rb.isKinematic = false;
                    }
                }
                await TakeOff();
                //Debug.Log("DDDDDGGGG");
            }
            else
            {
                TarMode = 3;
                ABList.Clear();
                ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
                ABMs = GameObject.FindGameObjectsWithTag(ABMtag);
                foreach (GameObject ab in ABESES)
                {
                    ABList.Add(ab);
                }
                foreach (GameObject ab in ABMs)
                {
                    ABList.Add(ab);
                }
                ABList.Add(ABASE);
                ABList.Sort(delegate (GameObject a, GameObject b)
                {
                    return Vector3.Distance(Rb.position, a.transform.position)
                    .CompareTo(
                      Vector3.Distance(Rb.position, b.transform.position));
                });
                Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
                dis = Vector3.Distance(Tarpos, Rb.position);
                if (dis < st_dis)
                {
                    Flag01 = true;
                    Flag08 = true;
                    Flag16 = true;
                    TarMode = 0;
                    CommandMode = 8;
                }
                else
                {
                    await FlyingFirst();
                    await MoveDirection();
                }
            }
        }
        GBT -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask TakeOff()
    {
        if (Flag02)
        {
            if (TakeOffMode == 0)
            {
                //BC.enabled = true;
                Rb.useGravity = false;
                Rb.isKinematic = true;
                Anim.CrossFade("TakeOff", 0.01f);
                Ground_y = transform.position.y;
                TakeOffMode++;
                TakeOff_t = 0;
            }
            if (TakeOffMode == 1)
            {
                //CC.enabled = true;
                TakeOffMode++;
            }
            Flag02 = false;
        }
        else if (TakeOffMode > 0)
        {
            Anim.SetBool("Idle", false);
            if (transform.position.y != TakeOff_y)
            {
                TakeOff_t += 0.05f * 60 * Time.deltaTime;
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, Ground_y, transform.position.z), new Vector3(transform.position.x, TakeOff_y, transform.position.z), TakeOff_t);
            }
            else
            {
                Flag02 = true;
                //CC.enabled = true;
                LandingFlag = false;
                Flying = true;
                Rb.isKinematic = true;
                if (Proper != null)
                {
                    Proper.SetActive(true);
                }
                TakeOffMode = 0;
                //Rb.velocity = Vector3.zero;
            }
        }
    }
    async UniTask GetEList()
    {
        EList.Clear();
        EUNITSA = GameObject.FindGameObjectsWithTag(EUnit01);
        EUNITSB = GameObject.FindGameObjectsWithTag(EUnit02);
        EUNITSC = GameObject.FindGameObjectsWithTag(EPlayerTag);

        foreach (GameObject obj in EUNITSA)
        {
            float dis_e = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < ESearchLength)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in EUNITSB)
        {
            float dis_e = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < ESearchLength)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in EUNITSC)
        {
            float dis_e = Vector3.Distance(Rb.position, obj.transform.position);
            if (dis < ESearchLength)
            {
                CPU Cpu = obj.GetComponent<CPU>();
                Fighter Fgt = obj.GetComponent<Fighter>();
                if (Cpu != null)
                {
                    if (!Cpu.Flying && Cpu.Life)
                    {
                        EList.Add(obj);
                    }
                    else
                    {
                        EList.Remove(obj);
                    }
                }
                if (Fgt != null)
                {
                    if (!Fgt.Flying && Fgt.Life)
                    {
                        EList.Add(obj);
                    }
                    else
                    {
                        EList.Remove(obj);
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
    async UniTask MinmalGard()
    {
        await GetABList();
        mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        if (Flag05)
        {
            if (!Flag01)
            {
                Flag01 = true;
            }
            BuildMode = 5;
            BFlag = true;
            if (BuildList.Count > 0)
            {
                if (BuildList[0] != "battery")
                {
                    await ThrowAndRetrunBase();
                }
                else
                {
                    TarMode = 0;
                    Flag05 = false;
                }
            }
        }
        else if (BuildList.Count > 0)
        {
            if (Flag08)
            {
                Tarpos = mBcenter;
                TarMode = 0;
                dis = Vector3.Distance(Rb.position, Tarpos);
                if (dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-8, 0, -20);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(8, 0, 20);
                    }
                    TarMode = 1;
                    Flag08 = false;
                }
            }
            else
            {
                dis = Vector3.Distance(Tarpos, Rb.position);
                if (TarMode == 1 && dis < st_dis)
                {
                    await Release();
                    Tarpos = mBcenter;
                    TarMode = 2;
                }
                else if (TarMode == 2 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 2 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-20, 0, -8);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(20, 0, 8);
                    }
                    TarMode = 3;
                }
                else if (TarMode == 2 && dis < st_dis && !StayBase)
                {
                    Tarpos = mBcenter;
                }
                else if (TarMode == 3 && dis < st_dis && !StayBase)
                {
                    BuildMode = 4;
                    BFlag = true;
                    await Release();
                    Tarpos = mBcenter;
                    TarMode = 4;
                    //Debug.Log(AMBList.Count);
                }
                else if (TarMode == 4 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 4 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-20, 0, -10);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(20, 0, 10);
                    }
                    TarMode = 5;
                }
                else if (TarMode == 4 && dis < st_dis && !StayBase)
                {
                    Tarpos = mBcenter;
                }
                else if (TarMode == 5 && dis < st_dis)
                {
                    await Release();
                    Flag11 = true;
                    Flag08 = true;
                    Flag05 = true;
                    Flag16 = true;
                    CommandMode = 8;
                }
            }
            await FlyingFirst();
            await MoveDirection();
        }
        else
        {
            BFlag = true;
        }
    }
    async UniTask GardmBTank()
    {
        if (Flag11)
        {
            GBT = 500;
        }
        await GetABList();
        mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        float dis_m = Vector3.Distance(mBcenter, Rb.position);
        if (dis_m > 150)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            TarMode = 0;
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        else
        {
            if (TarMode < 7)
            {
                BuildMode = 1;
            }
            WrCommand = 0;
            BFlag = true;
            if (BuildList.Count > 0)
            {
                if (BuildList[0] == "tank")
                {
                    if (Flag08)
                    {
                        TarMode = 0;
                        Tarpos = mBcenter;
                        Flag08 = false;
                    }
                    dis = Vector3.Distance(Tarpos, Rb.position);
                    if (TarMode == 0 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 0 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-15, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-15, 0, 35);
                        }
                        TarMode = 1;
                    }
                    else if (TarMode == 0 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 1 && dis < st_dis)
                    {
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 2;
                    }
                    else if (TarMode == 2 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 2 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(15, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(15, 0, 35);
                        }
                        TarMode = 3;
                    }
                    else if (TarMode == 2 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 3 && dis < st_dis)
                    {
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 4;
                    }
                    else if (TarMode == 4 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 4 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-35, 0, 15);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(35, 0, 15);
                        }
                        TarMode = 5;
                    }
                    else if (TarMode == 4 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 5 && dis < st_dis)
                    {
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 6;
                    }
                    else if (TarMode == 6 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 6 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-35, 0, -15);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(35, 0, -15);
                        }
                        TarMode = 7;
                    }
                    else if (TarMode == 6 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 7 && dis < st_dis)
                    {
                        BuildMode = 2;
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 8;
                    }
                    else if (TarMode == 8 && dis < st_dis)
                    {
                        BuildMode = 2;
                        BuildMode = 2;
                        WrCommand = 1;
                        await ThrowAndRetrunBase();
                    }
                    await FlyingFirst();
                    await MoveDirection();
                }
                else if (BuildList[0] == "missile")
                {
                    dis = Vector3.Distance(Tarpos, Rb.position);
                    if (TarMode == 8 && dis < st_dis && StayBase)
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-8, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-8, 0, 35);
                        }
                        TarMode = 9;
                    }
                    else if (TarMode == 8 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 9 && dis < st_dis && !StayBase)
                    {
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 10;
                    }
                    else if (TarMode == 10 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABaseTag) || TarMode == 10 && dis < st_dis && StayBase && AMBList[0].gameObject.CompareTag(ABMtag))
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(8, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(8, 0, 35);
                        }
                        TarMode = 11;
                    }
                    else if (TarMode == 10 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 11 && dis < st_dis && !StayBase)
                    {
                        await Release();
                        TarMode = 0;
                        Flag08 = true;
                        Flag16 = true;
                        Flag11 = true;
                        CommandMode = 8;
                    }
                    await FlyingFirst();
                    await MoveDirection();
                }
                else
                {
                    await ThrowAndRetrunBase();
                }
            }
            else
            {
                await BuildAction();
            }
        }
        if(GBT <= 0.0f)
        {
            TarMode = 0;
            Flag08 = true;
            Flag16 = true;
            Flag11 = true;
            CommandMode = 8;
        }
        GBT -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask GardABASE()
    {
        if (BuildList.Count > 0 && Flag05)
        {
            BuildMode = 1;
            BFlag = true;
            if (BuildList[0] != "tank")
            {
                await ThrowAndRetrunBase();
            }
            int t = 0;
            foreach (string unit in BuildList)
            {
                if (unit == "tank")
                {
                    t++;
                }
            }
            if (t > 3)
            {
                Flag05 = false;
            }
        }
        else if (BuildList.Count > 0)
        {
            if (BuildList[0] != "tank")
            {
                await ThrowAndRetrunBase();
            }
            else
            {
                if (Flag08)
                {
                    WrCommand = 0;
                    mBcenter = new Vector3(ABASE.transform.position.x, transform.position.y, ABASE.transform.position.z);
                    Tarpos = mBcenter;
                    TarMode = 0;
                    dis = Vector3.Distance(Tarpos, transform.position);
                    if (dis < st_dis)
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-15, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-15, 0, 35);
                        }
                        TarMode++;
                        Flag08 = false;
                    }
                }
                else
                {
                    dis = Vector3.Distance(Tarpos, transform.position);
                    if (TarMode == 1 && dis < st_dis)
                    {
                        WrCommand = 0;
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 2;
                    }
                    else if (TarMode == 2 && dis < st_dis)
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(15, 0, -35);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(15, 0, 35);
                        }
                        TarMode = 3;
                    }
                    else if (TarMode == 3 && dis < st_dis)
                    {
                        WrCommand = 0;
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 4;
                    }
                    else if (TarMode == 4 && dis < st_dis)
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-35, 0, 15);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(35, 0, 15);
                        }
                        TarMode = 5;
                    }
                    else if (TarMode == 5 && dis < st_dis)
                    {
                        WrCommand = 0;
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 6;
                    }
                    else if (TarMode == 6 && dis < st_dis)
                    {
                        await Lunch();
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-35, 0, -15);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(35, 0, -15);
                        }
                        TarMode = 7;
                    }
                    else if (TarMode == 7 && dis < st_dis)
                    {
                        TarMode = 0;
                        WrCommand = 0;
                        await Release();
                        Tarpos = mBcenter;
                        Flag08 = true;
                        Flag05 = true;
                        Flag16 = true;
                        CommandMode = 8;
                    }
                }
                await MoveDirection();
                await FlyingFirst();
            }
        }
        else
        {
            BuildMode = 1;
            BFlag = true;
        }
    }
    async UniTask SliderHP()
    {
        if (HitPoint <= 0.0f)
        {
            Flag08 = true;
            await Dead();
        }
        else if (StayBase && HitPoint <= MaxHitPoint && DeadMode == 0)
        {
            HitPoint += 0.1f * 30 * Time.deltaTime;
        }
        if (SD.gameObject.activeSelf)
        {
            SD.value = HitPoint / MaxHitPoint;
        }
        if (OilSld.gameObject.activeSelf)
        {
            OilSld.value = Oil / MaxOil;
        }
        if (StayBase && Oil < MaxOil && DeadMode == 0)
        {
            Oil += 0.1f * (MaxOil / 2) * Time.deltaTime;
        }
    }
    async UniTask GetmBList()
    {
        mBList.Clear();
        mBs = GameObject.FindGameObjectsWithTag(mBaseTag);
        EBs = GameObject.FindGameObjectsWithTag(EBaseTag);
        EBMs = GameObject.FindGameObjectsWithTag(EMBtag);
        foreach (GameObject mB in mBs)
        {
            mBList.Add(mB);
        }
        foreach (GameObject EB in EBs)
        {
            mBList.Add(EB);
        }
        foreach (GameObject EB in EBMs)
        {
            mBList.Add(EB);
        }
        mBList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(Rb.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(Rb.position, b.transform.position));
        });
    }
    async UniTask MoveNearPmB()
    {
        if (Flag08)
        {
            if (!Flag01)
            {
                Flag01 = true;
            }
            EPlayers = GameObject.FindGameObjectsWithTag(EPlayerTag);
            if (EPlayers.Length > 0)
            {
                foreach (GameObject obj in EPlayers)
                {
                    float EPdis2 = Vector3.Distance(Rb.position, obj.transform.position);
                    if (EPdis2 < EPdis)
                    {
                        EPdis = EPdis2;
                        TargetEP = obj;
                    }
                }
                EPdis = 100000000000;
            }
            await GetABList();
            if (ABList.Count > 1)
            {
                ABList.Sort(delegate (GameObject a, GameObject b)
                {
                    return Vector3.Distance(TargetEP.transform.position, a.transform.position)
                    .CompareTo(
                      Vector3.Distance(TargetEP.transform.position, b.transform.position));
                });
            }
            if (ABList.Count > 0)
            {
                Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
            }
            else
            {
                Tarpos = new Vector3(ABASE.transform.position.x, Rb.position.y, ABASE.transform.position.z);
            }
            Flag08 = false;
        }
        dis = Vector3.Distance(Tarpos, Rb.position);
        if (dis < st_dis)
        {
            CommandMode = 5;
            Flag08 = true;
            Flag16 = true;
        }
        await FlyingFirst();
        await MoveDirection();
    }
    async UniTask CarrySol()
    {
        await GetABList();
        mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        if (solcount < 8)
        {
            BuildMode = 0;
            BFlag = true;
            if (BuildList.Count > 0)
            {
                if (BuildList[0] == "hohei")
                {
                    if (Flag08)
                    {
                        solcount = 0;
                        TarMode = 0;
                        Tarpos = mBcenter;
                        Flag08 = false;
                    }
                    dis = Vector3.Distance(Tarpos, Rb.position);
                    if (TarMode == 0 && dis < st_dis && StayBase)
                    {
                        await Lunch();
                        await GetmBList();
                        if (Oil >= MaxOil)
                        {
                            Tarpos = new Vector3(mBList[0].transform.position.x, Rb.position.y, mBList[0].transform.position.z);
                            TarMode = 1;
                        }
                    }
                    else if (TarMode == 0 && dis < st_dis && !StayBase)
                    {
                        Tarpos = mBcenter;
                    }
                    else if (TarMode == 1 && dis < st_dis + Release_dis && !StayBase)
                    {
                        solcount++;
                        await Release();
                        Tarpos = mBcenter;
                        TarMode = 0;
                    }
                }
                else
                {
                    await ThrowAndRetrunBase();
                }
                await FlyingFirst();
                await MoveDirection();
            }
        }
        else
        {
            Flag08 = true;
            TarMode = 0;
            solcount = 0;
            Flag16 = true;
            CommandMode = 8;
        }
    }
    async UniTask GetNearMB()
    {
        AMBList.Clear();
        ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
        ABMs = GameObject.FindGameObjectsWithTag(ABMtag);
        foreach (GameObject ab in ABESES)
        {
            AMBList.Add(ab);
        }
        foreach (GameObject ab in ABMs)
        {
            AMBList.Add(ab);
        }
        AMBList.Add(ABASE);
        if (AMBList.Count > 1)
        {
            AMBList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
        }
        MB = null;
        MB = ABList[0].transform.parent.gameObject.GetComponent<MiniBase>();
        EB = null;
        EB = ABList[0].transform.parent.gameObject.GetComponent<EBase>();
    }
    async UniTask CurrentBase()
    {
        if (!Flag08)
        {
            Flag08 = true;
        }
        if (!Flag09)
        {
            Flag09 = true;
        }
        if (!Flag11)
        {
            Flag11 = true;
        }
        if (!Flag14)
        {
            Flag14 = true;
        }
        if (!Flag16)
        {
            Flag16 = true;
        }
        await GetABList();
        mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        if (Box.activeSelf)
        {
            if (ABaseTag == "RminiBase")
            {
                Tarpos = mBcenter + new Vector3(-28, 0, 0);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = mBcenter + new Vector3(28, 0, 0);
            }
            dis = Vector3.Distance(Rb.position, Tarpos);
            if (dis < st_dis)
            {
                await Release();
            }
            await FlyingFirst();
            await MoveDirection();
        }
        else
        {
            if (BuildList.Count > 0)
            {
                await ThrowAndRetrunBase();
            }
            else
            {
                Tarpos = mBcenter;
                dis = Vector3.Distance(Rb.position, mBcenter);
                if (dis < st_dis && !Box.activeSelf)
                {
                    StayBase = true;
                    if (HitPoint >= MaxHitPoint)
                    {
                        //Debug.Log("WWWE");
                        if (MB != null)
                        {
                            if (Oil >= MaxOil)
                            {
                                Flag16 = true;
                                CommandMode = MB.ReCommand();
                            }
                        }
                        else if (EB != null)
                        {
                            if (Oil >= MaxOil)
                            {
                                Flag16 = true;
                                CommandMode = EB.ReCommand();
                            }
                        }
                    }
                }
                else
                {
                    await FlyingFirst();
                    await MoveDirection();
                }
            }
        }
        await GetNearMB();
    }
    async UniTask ThrowAndRetrunBase()
    {
        if (Flag10)
        {
            //Debug.Log(ABaseTag);
            if (!Box.activeSelf)
            {
                await Lunch();
            }
            await GetABList();
            mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, transform.position.z);
            if (ABaseTag == "RminiBase")
            {
                Tarpos = mBcenter + new Vector3(-28, 0, 0);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = mBcenter + new Vector3(28, 0, 0);
            }
            TarMode = 0;
            //CommandStack = CommandMode;
            WrCommand = 1;
            Flag10 = false;
        }
        float dis = Vector3.Distance(transform.position, Tarpos);
        if (TarMode == 0 && dis < st_dis && !StayBase)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode = 1;
        }
        if (TarMode == 1 && dis < st_dis)
        {
            if (BuildList.Count > 0 && StayBase)
            {
                if (!Box.activeSelf)
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-25, 0, 0);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(25, 0, 0);
                    }
                }
                else
                {
                    Tarpos = mBcenter;
                }
            }
            else
            {
                //CommandStack = CommandMode;
                CommandMode = 8;
                Flag10 = true;
                Flag16 = true;
            }
            TarMode = 0;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask DetectionHomeBaseAttacker()
    {
        if (Flag16)
        {
            float AEdis = 2000.0f;
            AHEList.Clear();
            AHEUNITS = GameObject.FindGameObjectsWithTag(EUnit01);
            foreach (GameObject obj in AHEUNITS)
            {
                Warrior Wr = obj.GetComponent<Warrior>();
                if (Wr.CommandMode == 2 && !Wr.AntiHBA_F)
                {
                    AHEList.Add(obj);
                }
            }
            if (AHEList.Count > 0)
            {
                await GetEList();
                if (EList.Count > 1)
                {
                    EList.Sort(delegate (GameObject a, GameObject b)
                    {
                        return Vector3.Distance(ABASE.transform.position, a.transform.position)
                        .CompareTo(
                          Vector3.Distance(ABASE.transform.position, b.transform.position));
                    });
                }
                if (EList.Count > 0)
                {
                    AEdis = Vector3.Distance(ABASE.transform.position, EList[0].transform.position);
                    Debug.Log(AEdis);
                }
            }
            if (AEdis < 220.0f && !Box.activeSelf)
            {
                Tarpos = new Vector3(ABASE.transform.position.x, Rb.position.y, ABASE.transform.position.z);
                dis = Vector3.Distance(Tarpos, Rb.position);
                if (dis < st_dis)
                {
                    Flag16 = false;
                    if (CommandMode != 11)
                    {
                        Flag08 = true;
                    }
                    CommandMode = 11;
                }
                else
                {
                    await MoveDirection();
                    await FlyingFirst();
                }
            }
            else if (AHEList.Count < 5 && AHEList.Count > 0 && Flying && !Box.activeSelf)
            {
                Flag16 = false;
                if (CommandMode != 22)
                {
                    Flag08 = true;
                }
                CommandMode = 22;
            }
            else if (AHEList.Count > 4 && !Box.activeSelf && Flying)
            {
                Flag16 = false;
                if (CommandMode != 22)
                {
                    Flag08 = true;
                }
                CommandMode = 22;
            }
        }
        //Debug.Log(AHEList.Count);
    }
    async UniTask InterceptionHomeBaseAttack()
    {
        if (AHEList.Count > 0)
        {
            mBcenter = new Vector3(ABASE.transform.position.x, Rb.position.y, ABASE.transform.position.z);
            if (Flag04)
            {
                GBT = 500;
                WrCommand = 0;
                if ((int)float_income > 200)
                {
                    BuildMode = 1;
                }
                else
                {
                    BuildMode = 3;
                }
                if (!Flag01)
                {
                    Flag01 = true;
                }
                AHETarMode = 0;
                Tarpos = mBcenter;
                Flag04 = false;
            }
            BFlag = true;
            dis = Vector3.Distance(Tarpos, Rb.position);
            if (dis < st_dis && AHETarMode == 0)
            {
                if (DefCount < 8)
                {
                    if (BuildList.Count > 0)
                    {
                        await Lunch();
                        float x = AHEList[0].transform.position.x - mBcenter.x;
                        float z = AHEList[0].transform.position.z - mBcenter.z;
                        float rad = Mathf.Atan2(x, z);
                        Tarpos = new Vector3(ABASE.transform.position.x + (Mathf.Sin(rad) * 30), Rb.position.y, ABASE.transform.position.z + (Mathf.Cos(rad) * 30));
                        //Debug.Log(Tarpos);
                        AHETarMode = 1;
                    }
                }
                else
                {
                    Flag04 = true;
                    CommandMode = 16;
                }
            }
            else if (dis < st_dis && AHETarMode == 1)
            {
                DefCount++;
                await Release();
                Warrior Wr = AHEList[0].GetComponent<Warrior>();
                Wr.CommandMode = 4;
                Wr.AntiHBA_F = true;
                Tarpos = mBcenter;
                AHETarMode = 0;
                Flag04 = true;
                Flag16 = true;
                CommandMode = 15;
                AHETarMode = 0;
            }
            await FlyingFirst();
            await MoveDirection();
        }
        else
        {
            AHETarMode = 0;
            Flag04 = true;
            Flag16 = true;
            CommandMode = 15;
        }
        if(GBT <= 0.0f)
        {
            AHETarMode = 0;
            Flag04 = true;
            Flag16 = true;
            CommandMode = 15;
        }
        GBT -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask PursuitAHE()
    {
        if (Flag08)
        {
            GBT = GroundBattleTime;
            TarMode = 0;
            Flag08 = false;
        }
        if (GBT > 0.0f)
        {
            if (AHEList.Count > 1)
            {
                AHEList.Sort(delegate (GameObject a, GameObject b)
                {
                    return Vector3.Distance(ABASE.transform.position, a.transform.position)
                    .CompareTo(
                      Vector3.Distance(ABASE.transform.position, b.transform.position));
                });
            }
            if (AHEList.Count > 0 && TarMode == 0)
            {
                Tarpos = new Vector3(AHEList[0].transform.position.x, Rb.position.y, AHEList[0].transform.position.z);
            }
            else if (AHEList.Count > 0 && TarMode == 1)
            {
                await GetEList();
                if (EList.Count > 0)
                {
                    Tarpos = new Vector3(EList[0].transform.position.x, Rb.position.y, EList[0].transform.position.z);
                }
                else
                {
                    TarMode = 2;
                }
            }
            else
            {
                TarMode = 2;
            }
            dis = Vector3.Distance(Rb.position, Tarpos);
            if (TarMode == 0 && dis < st_dis + BattleDis && !StayBase && Flying)
            {
                await Landing();
                Flag02 = true;
                TarMode = 1;
            }
            else if (TarMode == 1 && dis < st_dis + GroundBattleDis && !Flying)
            {
                if (isGround)
                {
                    await GetEList();
                    transform.LookAt(EList[0].transform.position);
                    if (!Shooter.activeSelf)
                    {
                        Shooter.SetActive(true);
                    }
                    if (Anim.GetBool("Run"))
                    {
                        Anim.SetBool("Run", false);
                    }
                    if (!Anim.GetBool("Shot"))
                    {
                        Anim.SetBool("Shot", true);
                    }
                    if (!Rb.isKinematic)
                    {
                        Rb.isKinematic = true;
                    }
                }
            }
            else if (!Flying)
            {
                Debug.Log("KKKd");
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
                await FlyingFirst();
                await MoveDirection();
            }
            else
            {
                await FlyingFirst();
                await MoveDirection();
            }
        }
        if (GBT < 0.0f || Oil < MaxOil / 2 || TarMode == 2 || HitPoint < MaxHitPoint / 5)
        {
            if (!Flying)
            {

                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }

                await TakeOff();
            }
            else
            {
                Flag01 = true;
                Flag11 = true;
                Flag08 = true;
                Flag16 = true;
                CommandMode = 8;
            }
        }
        GBT -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask ChargeOil()
    {
        if (Oil >= MaxOil && CommandMode == -1)
        {
            Flag08 = true;
            Flag11 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        if (Oil < MaxOil / 6)
        {
            if (Flag08)
            {
                CommandMode = -1;
                if (Flag16)
                {
                    Flag16 = false;
                }
            }
            if (Flying)
            {
                await GetABList();
                Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
                dis = Vector3.Distance(Tarpos, Rb.position);
                await FlyingFirst();
                await MoveDirection();
            }
            else
            {
                if (Shooter.activeSelf)
                {
                    Shooter.SetActive(false);
                }
                if (Anim.GetBool("Run"))
                {
                    Anim.SetBool("Run", false);
                }
                if (Anim.GetBool("Shot"))
                {
                    Anim.SetBool("Shot", false);
                }
                if (Rb.isKinematic)
                {
                    Rb.isKinematic = false;
                }
                await TakeOff();
            }
            if (!StayBase && Box.activeSelf && !Obs && Flying)
            {
                await Release();
            }
        }

    }
    async UniTask ContinuousAttack(int c)
    {
        if (!Flag08 && GBT <= 0.0f)
        {
            Flag16 = true;
        }
        if (BuildList.Count > 0)
        {
            if (BuildList[0] == "acar" || BuildList[0] == "tank")
            {
                await GetABList();
                mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
                if (Flag08)
                {
                    GBT = 60;
                    WrCommand = c;
                    AcCount = 0;
                    TarMode = 0;
                    Tarpos = mBcenter;
                    Flag08 = false;
                }
                else
                {
                    GBT -= 0.1f * 60 * Time.deltaTime;
                }
                if (AcCount > 10)
                {
                    AcCount = 0;
                    TarMode = 0;
                    Flag08 = true;
                    Flag16 = true;
                    CommandMode = 8;
                }
                dis = Vector3.Distance(Rb.position, Tarpos);
                if (dis < st_dis && TarMode == 0)
                {
                    await Lunch();
                    if (Box.activeSelf)
                    {
                        //Debug.Log("Q");
                        if (ABaseTag == "RminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(-35, 0, 0);
                        }
                        else if (ABaseTag == "BminiBase")
                        {
                            Tarpos = mBcenter + new Vector3(35, 0, 0);
                        }
                        TarMode = 1;
                    }
                }
                else if (dis < st_dis && TarMode == 1 && !StayBase)
                {
                    await Release();
                    if (!Box.activeSelf)
                    {
                        Tarpos = mBcenter;
                        TarMode = 0;
                        AcCount++;
                    }
                }
                else if (dis < st_dis && TarMode == 1 && StayBase)
                {
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-40, 0, 0);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(40, 0, 0);
                    }
                }
                await FlyingFirst();
                await MoveDirection();
            }
            else
            {
                await ThrowAndRetrunBase();
                if ((int)float_income > 2000)
                {
                    BuildMode = 1;
                }
                else
                {
                    BuildMode = 3;
                }
                BFlag = true;
            }
        }
        else
        {
            WrCommand = c;
            if ((int)float_income > 2000)
            {
                BuildMode = 1;
            }
            else
            {
                BuildMode = 3;
            }
            BFlag = true;
        }
    }
    async UniTask UnitCommanChange(int c)
    {
        await GetABList();
        mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        if (Flag08)
        {
            CommandChangeTimer = CommandChangeTime;
            WrCommand = c;
            TarMode = 0;
            Flag08 = false;
        }
        if (BuildList.Count > 0)
        {
            await ThrowAndRetrunBase();
        }
        else
        {
            await GetAUList();
            Tarpos = new Vector3(AUnitList[0].transform.position.x, Rb.position.y, AUnitList[0].transform.position.z);
            dis = Vector3.Distance(Tarpos, Rb.position);
            if (TarMode == 0 && dis < st_dis)
            {
                if (Box.activeSelf && AwayLunchUnit == null)
                {
                    await Release();
                }
                else
                {
                    await ChangeLunch();
                }
                TarMode = 1;
            }
            else if (TarMode == 1)
            {
                if (Box.activeSelf)
                {
                    await ChangeRelease();
                }
                TarMode = 0;
            }
            await FlyingFirst();
            await MoveDirection();
        }
        if (CommandChangeTimer < 0.0f)
        {
            Flag08 = true;
            TarMode = 0;
            Flag16 = true;
            CommandMode = 8;
        }
        CommandChangeTimer -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask UnitCommandChangeBaseAttack(int c)
    {
        if (BuildList.Count < 1)
        {
            await GetAUList();
            await GetABList();
            mBcenter = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
            if (Flag08)
            {
                CCTimer = 140;
                CommandChangeTimer = CommandChangeTime;
                WrCommand = c;
                TarMode = 0;
                Flag11 = true;
                Flag08 = false;
            }
            float mBdis = Vector3.Distance(mBcenter, Rb.position);
            if (mBdis > baseoutdis)
            {
                Flag11 = true;
                Flag08 = true;
                Flag16 = true;
                CommandMode = 8;
            }
            if (AUnitList.Count > 0)
            {
                Flag05 = true;
                if (Flag11)
                {
                    Tarpos = new Vector3(AUnitList[0].transform.position.x, Rb.position.y, AUnitList[0].transform.position.z);
                    Flag11 = false;
                }
                float dis = Vector3.Distance(Tarpos, Rb.position);
                if (CommandChangeUnitDis < dis)
                {
                    Flag11 = true;
                    Flag08 = true;
                    Flag16 = true;
                    //CommandStack = CommandMode;
                    CommandMode = 8;
                }
                if (TarMode == 0 && dis < st_dis)
                {
                    if (!Box.activeSelf)
                    {
                        await ChangeLunch();
                        TarMode = 1;
                    }
                    else
                    {
                        await ChangeRelease();
                    }
                }
                if (TarMode == 1 && CommandChangeTimer < 0f)
                {
                    CommandChangeTimer = CommandChangeTime;
                    if (Box.activeSelf)
                    {
                        await ChangeRelease();
                    }
                    Flag11 = true;
                    TarMode = 0;
                }
                else if (TarMode == 1)
                {
                    CommandChangeTimer -= 0.1f * 60 * Time.deltaTime;
                }
                await MoveDirection();
                await FlyingFirst();
                //Debug.Log(dis);
            }
            else
            {
                if (Box.activeSelf)
                {
                    await ChangeRelease();
                }
                //CommandStack = CommandMode;
                CommandMode = 8;
                Flag11 = true;
                Flag08 = true;
                Flag16 = true;
            }
        }
        else
        {
            await ThrowUnitModeNoChange();
        }
        if (CCTimer <= 0.0f && !Box.activeSelf)
        {
            Flag08 = true;
            Flag11 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        CCTimer -= 0.1f * 60 * Time.deltaTime;
    }
    async UniTask ThrowUnitModeNoChange()
    {
        if (Flag12)
        {
            await GetABList();
            mBcenter = new Vector3(ABList[0].transform.position.x, transform.position.y, ABList[0].transform.position.z);
            Tarpos = mBcenter;
            TarMode = 0;
            Flag12 = false;
        }
        float dis = Vector3.Distance(Tarpos, transform.position);
        if (TarMode == 1 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode = 0;
        }
        if (TarMode == 0 && dis < st_dis)
        {
            if (!Box.activeSelf)
            {
                await Lunch();
            }
            if (ABaseTag == "RminiBase")
            {
                Tarpos = mBcenter + new Vector3(-25, 0, 0);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = mBcenter + new Vector3(25, 0, 0);
            }
            TarMode = 1;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask ChangeRelease()
    {
        if (!Obs)
        {
            if (AwayLunchUnit != null)
            {
                if (AwayLunchUnit == "tank")
                {
                    GameObject tank = (GameObject)Instantiate(Tank, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = WrCommand;
                    Wr.HitPoint = BoxHP;
                    Wr.UnitType = "tank";
                    MobKappa Mk = tank.GetComponent<MobKappa>();
                    MobKarasu Mkr = tank.GetComponent<MobKarasu>();
                    if (Mk != null && BoxLookInt.Count > 0)
                    {
                        Mk.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mk.Numbers.Add(i);
                        }
                        Mk.LooksChange();
                    }
                    else if (Mkr != null)
                    {
                        Mkr.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mkr.Numbers.Add(i);
                        }
                        await Mkr.ChangeLooks();
                    }
                    Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    tankRb.AddForce(Vector3.down * fallspeed);
                }
                if (AwayLunchUnit == "missile")
                {
                    GameObject tank = (GameObject)Instantiate(Missile, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = WrCommand;
                    Wr.HitPoint = BoxHP;
                    Wr.UnitType = "misslie";
                    MobKappa Mk = tank.GetComponent<MobKappa>();
                    MobKarasu Mkr = tank.GetComponent<MobKarasu>();
                    if (Mk != null && BoxLookInt.Count > 0)
                    {
                        Mk.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mk.Numbers.Add(i);
                        }
                        Mk.LooksChange();
                    }
                    else if (Mkr != null)
                    {
                        Mkr.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mkr.Numbers.Add(i);
                        }
                        await Mkr.ChangeLooks();
                    }
                    Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    tankRb.AddForce(Vector3.down * fallspeed);
                }
                if (AwayLunchUnit == "acar")
                {
                    GameObject tank = (GameObject)Instantiate(Missile, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = WrCommand;
                    Wr.HitPoint = BoxHP;
                    Wr.UnitType = "acar";
                    MobKappa Mk = tank.GetComponent<MobKappa>();
                    MobKarasu Mkr = tank.GetComponent<MobKarasu>();
                    if (Mk != null && BoxLookInt.Count > 0)
                    {
                        Mk.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mk.Numbers.Add(i);
                        }
                        Mk.LooksChange();
                    }
                    else if (Mkr != null)
                    {
                        Mkr.Numbers.Clear();
                        foreach (int i in BoxLookInt)
                        {
                            Mkr.Numbers.Add(i);
                        }
                        await Mkr.ChangeLooks();
                    }
                    Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    tankRb.AddForce(Vector3.down * fallspeed);
                }
            }
            AwayLunchUnit = null;
            Box.SetActive(false);
        }
    }
    async UniTask ChangeLunch()
    {
        GameObject obj = AUnitList[0].gameObject;
        Warrior Wr = obj.GetComponent<Warrior>();
        MobKappa Mk = obj.GetComponent<MobKappa>();
        MobKarasu Mkr = obj.GetComponent<MobKarasu>();
        BoxLookInt.Clear();
        if (Wr != null)
        {
            if (Wr.ReTurnSelect() && Wr.CommandMode == 0)
            {
                if (Mk != null)
                {
                    foreach (int i in Mk.Numbers)
                    {
                        BoxLookInt.Add(i);
                    }
                }
                else if (Mkr != null)
                {
                    foreach (int i in Mkr.Numbers)
                    {
                        BoxLookInt.Add(i);
                    }
                }
                BoxHP = Wr.HitPoint;
                if (Wr.UnitType == "tank")
                {
                    AwayLunchUnit = "tank";
                }
                else if (Wr.UnitType == "missile")
                {
                    AwayLunchUnit = "missile";
                }
                else if (Wr.UnitType == "acar")
                {
                    AwayLunchUnit = "acar";
                }
                Destroy(obj);
                if (!Box.activeSelf)
                {
                    Box.SetActive(true);
                }
                AUnitList.RemoveAt(0);
            }
        }
    }
    async UniTask GetAUList()
    {
        AUnitList.Clear();
        units = GameObject.FindGameObjectsWithTag(Tank.gameObject.tag);
        foreach (GameObject obj in units)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            if (Wr.CommandMode == 0)
            {
                AUnitList.Add(obj);
            }
        }
        if (AUnitList.Count > 1)
        {
            AUnitList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(Rb.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(Rb.position, b.transform.position));
            });
        }
    }
    async UniTask ChargeTank(int c)
    {
        if (ChargeTankint == 2)
        {
            Baseposint = false;
            ChargeTankint = 0;
        }
        int tanki = 0;
        foreach (string unit in BuildList)
        {
            if (unit == "tank")
            {
                tanki++;
            }
        }
        if (tanki < 5 && Flag11)
        {
            if (!Baseposint)
            {
                await GetABList();
                if (ABList.Count > 0)
                {
                    Tarpos = new Vector3(ABList[0].transform.position.x, transform.position.y, ABList[0].transform.position.z);
                }
                dis = Vector3.Distance(Tarpos, transform.position);
                if (dis < st_dis)
                {
                    Baseposint = true;
                }
                await OverABase();
            }
            BuildMode = 1;
        }
        else
        {
            Flag11 = false;
        }
        if (BuildList.Count > 0 && Baseposint)
        {
            if (BuildList[0] != "tank")
            {
                await ThrowUnit();
            }
        }
        if (!Flag11 && ChargeTankint == 0)
        {
            await ArrangementChargeTank01();
        }
        if (!Flag11 && ChargeTankint == 1)
        {
            await ArrangementChargeTank02(c);
        }
    }
    async UniTask ArrangementChargeTank01()
    {
        if (Flag08)
        {
            WrCommand = 0;
            BuildMode = 0;
            mBcenter = transform.position;
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(12, 0, -27);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(12, 0, 27);
            }
            TarMode = 0;
            Flag08 = false;
        }
        float mBdis = Vector3.Distance(mBcenter, transform.position);
        if (mBdis > baseoutdis)
        {
            Flag11 = true;
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        dis = Vector3.Distance(Tarpos, transform.position);
        if (TarMode == 0 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 1 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-12, 0, -27);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(-12, 0, 27);
            }
            TarMode++;
        }
        else if (TarMode == 2 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 3 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-27, 0, -12);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(27, 0, -12);
            }
            TarMode++;
        }
        else if (TarMode == 4 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 5 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-27, 0, 12);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(27, 0, 12);
            }
            TarMode++;
        }
        else if (TarMode == 6 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 7 && dis < st_dis)
        {
            ChargeTankint++;
            Flag11 = true;
            Flag08 = true;
            Flag05 = false;
            TarMode = 0;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask ArrangementChargeTank02(int c)
    {
        if (Flag08)
        {
            WrCommand = 0;
            BuildMode = 0;
            mBcenter = transform.position;
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(12, 0, -29);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(12, 0, 29);
            }
            TarMode = 0;
            Flag08 = false;
        }
        float mBdis = Vector3.Distance(mBcenter, transform.position);
        if (mBdis > baseoutdis)
        {
            Flag11 = true;
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        dis = Vector3.Distance(Tarpos, transform.position);
        if (TarMode == 0 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 1 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-12, 0, -29);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(-12, 0, 29);
            }
            TarMode++;
        }
        else if (TarMode == 2 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 3 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-29, 0, -12);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(29, 0, -12);
            }
            TarMode++;
        }
        else if (TarMode == 4 && dis < st_dis)
        {
            await Release();
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 5 && dis < st_dis)
        {
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-29, 0, 12);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(29, 0, 12);
            }
            TarMode++;
        }
        else if (TarMode == 6 && dis < st_dis)
        {
            if (Box.activeSelf)
            {
                await Release();
            }
            Tarpos = mBcenter;
            TarMode++;
        }
        else if (TarMode == 7 && dis < st_dis)
        {
            ChargeTankint = 0;
            if (c == 1)
            {
                CommandMode = 7;
            }
            else if (c == 2)
            {
                CommandMode = 17;
            }
            Flag11 = true;
            Flag08 = true;
            Flag05 = false;
            Flag16 = true;
            TarMode = 0;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask ThrowUnit()
    {
        if (Flag08)
        {
            mBcenter = transform.position;
            await Lunch();
            if (ABaseTag == "RminiBase")
            {
                Tarpos = transform.position + new Vector3(-25, 0, 0);
            }
            else if (ABaseTag == "BminiBase")
            {
                Tarpos = transform.position + new Vector3(25, 0, 0);
            }
            TarMode = 0;
            Flag08 = false;
        }
        float mBdis = Vector3.Distance(mBcenter, transform.position);
        if (mBdis > baseoutdis)
        {
            Flag11 = true;
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        dis = Vector3.Distance(transform.position, Tarpos);
        if (TarMode == 0 && dis < st_dis)
        {
            TarMode++;
            if (Box.activeSelf)
            {
                await Release();
            }
            await Release();
            Tarpos = mBcenter;
        }
        if (TarMode == 1 && dis < st_dis)
        {
            Baseposint = false;
            TarMode = 0;
            Flag08 = true;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask HeadFrontBase()
    {
        if (Flag08)
        {
            if (mBList.Count > 0)
            {
                float tdis = 5000000;
                dis = 0f;
                Transform cts = transform;
                foreach (GameObject ts in mBList)
                {
                    if (ts.gameObject.CompareTag(ABaseTag))
                    {
                        dis = Vector3.Distance(EBASE.transform.position, ts.transform.position);
                        if (dis < tdis)
                        {
                            tdis = dis;
                            cts = ts.transform;
                        }
                    }
                }
                Flag08 = false;
                Tarpos = new Vector3(cts.position.x, transform.position.y, cts.position.z);
            }
        }
        float dis0 = Vector3.Distance(transform.position, Tarpos);
        if (dis0 < st_dis)
        {
            Flag16 = true;
            CommandMode = 5;
            Flag08 = true;
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask ReleaseSoldier()
    {
        if (Flag08)
        {
            BuildMode = 0;
            solcount = 0;
            mBcenter = transform.position;
            Tarpos = mBcenter;
            TarMode = 0;
            Flag08 = false;
        }
        else if (solcount > 3)
        {
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
        else if (BuildList.Count > 0)
        {
            float mBdis = Vector3.Distance(mBcenter, transform.position);
            if (mBdis > baseoutdis)
            {
                Flag11 = true;
                Flag08 = true;
                Flag16 = true;
                CommandMode = 8;
            }
            dis = Vector3.Distance(Tarpos, transform.position);
            if (TarMode == 0 && dis < st_dis)
            {
                await Lunch();
                if (ABaseTag == "RminiBase")
                {
                    Tarpos = transform.position + new Vector3(-25, 0, 0);
                }
                else if (ABaseTag == "BminiBase")
                {
                    Tarpos = transform.position + new Vector3(25, 0, 0);
                }
                TarMode++;
            }
            else if (TarMode == 1 && dis < st_dis)
            {
                if (BuildList[0] == "hohei")
                {
                    if (Box.activeSelf)
                    {
                        await Release();
                    }
                    solcount++;
                    Tarpos = mBcenter;
                    TarMode = 0;
                }
                else
                {
                    if (Box.activeSelf)
                    {
                        await Release();
                    }
                    Tarpos = mBcenter;
                    TarMode = 0;
                }
            }
            await MoveDirection();
            await FlyingFirst();
        }
        else
        {
            BuildMode = 0;
        }
        BFlag = true;
    }
    async UniTask GetABList()
    {
        ABList.Clear();
        ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
        ABMs = GameObject.FindGameObjectsWithTag(ABMtag);
        foreach (GameObject ab in ABESES)
        {
            ABList.Add(ab);
        }
        foreach (GameObject ab in ABMs)
        {
            ABList.Add(ab);
        }
        ABList.Add(ABASE);
        EPlayers = GameObject.FindGameObjectsWithTag(EPlayerTag);
        if (EPlayers.Length > 0)
        {
            foreach (GameObject obj in EPlayers)
            {
                float EPdis2 = Vector3.Distance(Rb.position, obj.transform.position);
                if (EPdis2 < EPdis)
                {
                    EPdis = EPdis2;
                    TargetEP = obj;
                }
            }
            EPdis = 100000000000;
        }
        if (TargetEP != null && ABList.Count > 1)
        {
            ABList.Sort(delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(TargetEP.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(TargetEP.transform.position, b.transform.position));
            });
        }
    }
    async UniTask GardMissile()
    {
        //Debug.Log(Flag08);
        if (Flag08)
        {
            int mi = 0;
            foreach (string unit in BuildList)
            {
                if (unit == "missile")
                {
                    mi++;
                }
            }
            if (mi > 1)
            {
                Flag05 = true;
                Flag08 = false;
            }
            else if (BuildList.Count > 0)
            {
                if (BuildList[0] != "missile")
                {
                    await ThrowUnitModeNoChange();
                }
            }
            BuildMode = 2;
            BFlag = true;
        }
        else
        {
            if (Flag05)
            {
                TarMode = 0;
                await GetABList();
                mBcenter = new Vector3(ABList[0].transform.position.x, transform.position.y, ABList[0].transform.position.z);
                Tarpos = mBcenter;
                await MoveDirection();
                await FlyingFirst();
                if (StayBase)
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(10, 0, 25);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(10, 0, -25);
                    }
                    TarMode = 1;
                    Flag05 = false;
                }

            }
            else
            {
                float dis02 = Vector3.Distance(Tarpos, transform.position);
                if (TarMode == 1 && dis02 < st_dis)
                {
                    await Release();
                    Tarpos = mBcenter;
                    TarMode++;
                }
                else if (TarMode == 2 && dis02 < st_dis)
                {
                    await Lunch();
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-10, 0, 25);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-10, 0, -25);
                    }
                    TarMode++;
                }
                else if (TarMode == 3 && dis02 < st_dis)
                {
                    await Release();
                    Flag08 = true;
                    Flag05 = true;
                    Flag16 = true;
                    CommandMode = 8;
                }
                await MoveDirection();
                await FlyingFirst();
            }
        }
    }
    async UniTask ReturnABase()
    {
        //Debug.Log(MoveD);
        if (!Flag01)
        {
            Flag01 = true;
        }
        ABList.Clear();
        ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
        ABMs = GameObject.FindGameObjectsWithTag(ABMtag);
        foreach (GameObject obj in ABESES)
        {
            ABList.Add(obj);
        }
        foreach (GameObject obj in ABMs)
        {
            ABList.Add(obj);
        }
        ABList.Add(ABASE);
        ABList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(Rb.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(Rb.position, b.transform.position));
        });
        if (ABList.Count > 0)
        {
            Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        }
        else
        {
            Tarpos = new Vector3(ABASE.transform.position.x, Rb.position.y, ABASE.transform.position.z);
        }
        dis = Vector3.Distance(Rb.position, Tarpos);
        if (dis < st_dis)
        {
            //CommandStack = CommandMode;
            Flag14 = true;
            Flag05 = true;
            Flag08 = true;
            Flag09 = true;
            Flag10 = true;
            Flag16 = true;
            CommandMode = 5;
        }
        else
        {
            Debug.Log(Tarpos.ToString() + " " + Flag01.ToString());
            await FlyingFirst();
            await MoveDirection();
        }
        if (Box.activeSelf && !StayBase)
        {
            await Release();
        }
    }
    async UniTask CurrySolNearMB()
    {
        BuildMode = 0;
        BFlag = true;
        await SortMBNearAB();
        mBcenter = new Vector3(aB01.transform.position.x, Rb.position.y, aB01.transform.position.z);
        if (Flag08)
        {
            solcount = 0;
            TarMode = 0;
            Tarpos = mBcenter;
            Flag08 = false;
        }
        dis = Vector3.Distance(Tarpos, Rb.position);
        if (TarMode == 0 && dis < st_dis)
        {
            if (Oil >= MaxOil / 2)
            {
                if (Box.activeSelf)
                {
                    if (ABaseTag == "RminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(-25, 0, 0);
                    }
                    else if (ABaseTag == "BminiBase")
                    {
                        Tarpos = mBcenter + new Vector3(25, 0, 0);
                    }
                    TarMode = 1;
                }
                else
                {
                    await Lunch();
                }
            }
        }
        else if (TarMode == 1 && dis < st_dis)
        {
            if (!Box.activeSelf)
            {
                TarMode = 0;
                Tarpos = mBcenter;
                solcount++;
            }
            else
            {
                await Release();
            }
        }
        else
        {
            await FlyingFirst();
            await MoveDirection();
        }
        if (solcount > 7)
        {
            TarMode = 0;
            Flag08 = true;
            Flag16 = true;
            CommandMode = 8;
        }
    }
    async UniTask SortMBNearAB()
    {
        await GetABList();
        mBs = GameObject.FindGameObjectsWithTag(mBaseTag);
        float dis_ab;
        float dis_001 = 99999999999999999;
        float dis_002 = 99999999999999999;
        float dis_003;
        if (ABList.Count > 1)
        {
            foreach (GameObject obj in ABList)
            {
                foreach (GameObject mB in mBs)
                {
                    dis_ab = Vector3.Distance(obj.transform.position, mB.transform.position);
                    if (dis_ab < dis_001)
                    {
                        dis_001 = dis_ab;
                        mB01 = mB;
                    }
                }
                dis_003 = Vector3.Distance(MB.transform.position, obj.transform.position);
                if (dis_003 < dis_002)
                {
                    dis_002 = dis_003;
                    aB01 = obj;
                }
            }
        }
    }
    async UniTask RetrunABase0()
    {
        //Debug.Log(MoveD);
        if (!Flag01)
        {
            Flag01 = true;
        }
        await GetABList();
        if (ABList.Count > 0)
        {
            Tarpos = new Vector3(ABList[0].transform.position.x, Rb.position.y, ABList[0].transform.position.z);
        }
        dis = Vector3.Distance(Rb.position, Tarpos);
        if (dis < st_dis)
        {
            //CommandStack = CommandMode;
            Flag14 = true;
            Flag05 = true;
            Flag08 = true;
            Flag09 = true;
            Flag10 = true;
            Flag16 = true;
            CommandMode = 5;
        }
        else if (Box.activeSelf && !StayBase)
        {
            await Release();
        }
        await MoveDirection();
        await FlyingFirst();
    }
    async UniTask OverABase()
    {

        directioner.transform.position = transform.position;
        await MoveDirection();
        ABList.Clear();
        ABESES = GameObject.FindGameObjectsWithTag(ABaseTag);
        if (ABESES.Length > 0)
        {
            //Debug.Log("B");
            //Debug.Log(ABESES[0].transform.position);
            //Debug.Log(Tarpos);
        }
        foreach (GameObject ab in ABESES)
        {
            ABList.Add(ab);
        }
        ABList.Add(ABASE);
        ABList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(Rb.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(Rb.position, b.transform.position));
        });
        if (ABList.Count > 0)
        {
            //Debug.Log(ABList[0].transform.position);
            Tarpos = new Vector3(ABList[0].transform.position.x, transform.position.y, ABList[0].transform.position.z);
        }
        else
        {
            Tarpos = new Vector3(ABASE.transform.position.x, transform.position.y, ABASE.transform.position.z);
        }
        await FlyingFirst();
        dis = Vector3.Distance(transform.position, Tarpos);
        if (dis <= st_dis)
        {
            //CommandMode = 2;
            Flag10 = false;
        }
    }

    async UniTask Lunch()
    {
        if (StayBase && BuildList.Count > 0 && !Box.activeSelf)
        {
            Box.SetActive(true);
        }
    }
    async UniTask Release()
    {
        if (Box.activeSelf && BuildList.Count > 0 && !Obs)
        {
            Box.SetActive(false);
            if (BuildList[0] == "hohei")
            {
                //GS.hoheiint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject hohei = (GameObject)Instantiate(Hohei, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Rigidbody hoheitRb = hohei.GetComponent<Rigidbody>();
                hoheitRb.velocity = Vector3.down * fallspeed;
            }
            if (BuildList[0] == "tank")
            {
                //GS.tankint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject tank = (GameObject)Instantiate(Tank, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Warrior Wr = tank.GetComponent<Warrior>();
                Wr.CommandMode = WrCommand;
                Wr.UnitType = "tank";
                await Wr.SetTag();
                Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                tankRb.AddForce(Vector3.down * fallspeed);
            }
            if (BuildList[0] == "missile")
            {
                //GS.tankint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject tank = (GameObject)Instantiate(Missile, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Warrior Wr = tank.GetComponent<Warrior>();
                Wr.CommandMode = WrCommand;
                Wr.UnitType = "missile";
                await Wr.SetTag();
                Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                tankRb.AddForce(Vector3.down * fallspeed);
            }
            if (BuildList[0] == "acar")
            {
                //GS.tankint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject tank = (GameObject)Instantiate(Acar, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Warrior Wr = tank.GetComponent<Warrior>();
                Wr.CommandMode = WrCommand;
                Wr.UnitType = "acar";
                await Wr.SetTag();
                Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                tankRb.AddForce(Vector3.down * fallspeed);
            }
            if (BuildList[0] == "medic")
            {
                //GS.tankint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject tank = (GameObject)Instantiate(Medic, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Warrior Wr = tank.GetComponent<Warrior>();
                Wr.CommandMode = 4;
                Wr.UnitType = "medic";
                await Wr.SetTag();
                Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                tankRb.AddForce(Vector3.down * fallspeed);
            }
            if (BuildList[0] == "battery")
            {
                //GS.tankint -= 1;
                //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                GameObject tank = (GameObject)Instantiate(Battery, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                Warrior Wr = tank.GetComponent<Warrior>();
                Wr.CommandMode = 5;
                Wr.UnitType = "battery";
                await Wr.SetTag();
                Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                tankRb.AddForce(Vector3.down * fallspeed);
            }
            BuildList.RemoveAt(0);
        }
    }
    async UniTask BuildAction()
    {
        if (BFlag)
        {
            if (BuildList.Count < 7)
            {
                if (BuildMode == 0)
                {
                    if (Flag07 && float_income > SolCost)
                    {
                        float_income -= SolCost;
                        BuildTimer = SolBuildTime;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("hohei");
                    }
                }
                if (BuildMode == 1)
                {
                    if (Flag07 && float_income > TankCost)
                    {
                        BuildTimer = TankBuildTime;
                        float_income -= TankCost;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("tank");
                    }
                }
                if (BuildMode == 2)
                {
                    if (Flag07 && float_income > MisCost)
                    {
                        BuildTimer = MissBuildTime;
                        float_income -= MisCost;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("missile");
                    }
                }
                if (BuildMode == 3)
                {
                    if (Flag07 && float_income > AcCost)
                    {
                        BuildTimer = AcarBuildTime;
                        float_income -= AcCost;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("acar");
                    }
                }
                if (BuildMode == 4)
                {
                    if (Flag07 && float_income > MedCost)
                    {
                        BuildTimer = MediBuildTime;
                        float_income -= MedCost;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("medic");
                    }
                }
                if (BuildMode == 5)
                {
                    if (Flag07 && float_income > BattCost)
                    {
                        BuildTimer = BattBuildTime;
                        float_income -= BattCost;
                        Flag07 = false;
                    }
                    else if (BuildTimer <= 0.0f)
                    {
                        BFlag = false;
                        Flag07 = true;
                        BuildList.Add("battery");
                    }
                }
            }
            BuildTimer -= 0.1f * 60 * Time.deltaTime;
        }
        else
        {
            BuildTimer = 1f;
        }

    }
    async UniTask MoveDirection()
    {
        if (Flying && transform.position.y != TakeOff_y)
        {
            transform.position = new Vector3(transform.position.x, TakeOff_y, transform.position.z);
        }
        if (Flag01)
        {
            /*if(CommandMode == 8)
            {
                Debug.Log("O");
            }*/
            directioner.transform.LookAt(Tarpos);
        }
        if (LandingFlag)
        {
            if (Oil > 0.0f)
            {
                speed = run_speed;
            }
            else
            {
                speed = run_speed / 5f;
            }
            //EC.Flag = true;
        }
        else
        {
            if (Oil > 0.0f)
            {
                speed = fly_speed;
            }
            else
            {
                speed = fly_speed / 5f;
            }
            //EC.Flag = false;
        }
        if (MoveD == 4)
        {
            if (transform.rotation.eulerAngles == new Vector3(0, 0, 0) && !isGround)
            {
                v = 360;
                Flag06 = true;
            }
            if (transform.rotation.eulerAngles != new Vector3(0, 270, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                if (Flag06)
                {
                    transform.localEulerAngles = Vector3.Lerp(new Vector3(0, v, 0), new Vector3(0, 270, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                    else
                    {
                        Flag06 = false;
                    }
                }
                else
                {
                    transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 270, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                }
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    transform.localRotation = Quaternion.LookRotation(Vector3.left);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 6)
        {
            if (transform.rotation.eulerAngles != new Vector3(0, 90, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 90, 0), Leap_t);
                if (Leap_t < 1.0f)
                {
                    Leap_t += 0.1f * 30 * Time.deltaTime;
                }
            }
            else
            {
                Flag03 = true;
                if (LandingFlag)
                {
                    transform.localRotation = Quaternion.LookRotation(Vector3.right);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }

        }
        else if (MoveD == 8)
        {
            //Debug.Log(transform.rotation.eulerAngles.ToString() + " " + isGround.ToString());
            if (transform.rotation.eulerAngles.y > 180)
            {
                //Debug.Log("NNN");
                v = transform.eulerAngles.y;
                Flag06 = true;
            }
            if (transform.rotation.eulerAngles != new Vector3(0, 0, 0) && !isGround)
            {
                if (Flag06)
                {
                    if (Flag03)
                    {
                        Leap_t = 0;
                        Flag03 = false;
                    }
                    transform.localEulerAngles = Vector3.Lerp(new Vector3(0, v, 0), new Vector3(0, 360, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                    else
                    {
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                }
                else
                {
                    if (Flag03)
                    {
                        Leap_t = 0;
                        Flag03 = false;
                    }
                    transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                }
                //Debug.Log(transform.eulerAngles.y);
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    /*transform.localRotation = Quaternion.LookRotation(Vector3.forward);
                    if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 2)
        {
            if (transform.rotation.eulerAngles != new Vector3(0, 180, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 180, 0), Leap_t);
                if (Leap_t < 1.0f)
                {
                    Leap_t += 0.1f * 30 * Time.deltaTime;
                }
            }
            else
            {
                Flag03 = true;
                //transform.position = new Vector3(transform.position.x, y, transform.position.z);
                if (isGround)
                {
                    transform.localRotation = Quaternion.LookRotation(Vector3.back);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 3)
        {
            if (transform.rotation.eulerAngles != new Vector3(0, 135.0f, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 135.0f, 0), Leap_t);
                if (Leap_t < 1.0f)
                {
                    Leap_t += 0.1f * 30 * Time.deltaTime;
                }
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    transform.localRotation = Quaternion.Euler(0, 135, 0);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 9)
        {
            if (transform.rotation.eulerAngles != new Vector3(0, 45.0f, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 45.0f, 0), Leap_t);
                if (Leap_t < 1.0f)
                {
                    Leap_t += 0.1f * 30 * Time.deltaTime;
                }
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    transform.localRotation = Quaternion.Euler(0, 45, 0);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 1)
        {
            if (transform.rotation.eulerAngles == new Vector3(0, 270, 0))
            {
                v = 270;
                Flag06 = true;
            }
            if (transform.rotation.eulerAngles != new Vector3(0, 224.9999f, 0) && !isGround)
            {
                if (Flag06)
                {
                    if (Flag03)
                    {
                        Leap_t = 0;
                        Flag03 = false;
                    }
                    transform.localEulerAngles = Vector3.Lerp(new Vector3(0, v, 0), new Vector3(0, 224.9999f, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                }
                else
                {
                    if (Flag03)
                    {
                        Leap_t = 0;
                        Flag03 = false;
                    }
                    transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 224.9999f, 0), Leap_t);
                    if (Leap_t < 1.0f)
                    {
                        Leap_t += 0.1f * 30 * Time.deltaTime;
                    }
                }
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    transform.localRotation = Quaternion.Euler(0, 225f, 0);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 7)
        {
            if (transform.rotation.eulerAngles != new Vector3(0, 315, 0) && !isGround)
            {
                if (Flag03)
                {
                    Leap_t = 0;
                    Flag03 = false;
                }
                transform.localEulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 315, 0), Leap_t);
                if (Leap_t < 1.0f)
                {
                    Leap_t += 0.1f * 30 * Time.deltaTime;
                }
            }
            else
            {
                Flag03 = true;
                if (isGround)
                {
                    transform.localRotation = Quaternion.Euler(0, 315, 0);
                    /*if (!CC.enabled)
                    {
                        CC.enabled = true;
                    }*/
                    Flag01 = true;
                    Rb.isKinematic = false;
                    rot = transform.rotation;
                }
                Move();
            }
        }
        else if (MoveD == 5)
        {
            if (isGround)
            {
                Rb.isKinematic = true;
                Rb.isKinematic = false;
                //CC.enabled = false;
            }
            Anim.SetBool("Run", false);
        }
        if (LandingFlag && !isGround)
        {
            transform.rotation = rot;
        }
        await UnitMarking();
    }
    void Move()
    {
        if (LandingFlag)
        {
            if (Anim.GetBool("Shot"))
            {
                Anim.SetBool("Shot", false);
            }
            //BC.enabled = false;
            Anim.SetBool("Run", true);
        }
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        directioner.transform.position = transform.position;
        Oil -= (speed / 100) * 30 * Time.deltaTime;
        //CC.Move(transform.forward * speed * Time.deltaTime);
    }
    async UniTask Landing()
    {
        if (Flag02 && !isGround && !Obs)
        {
            if (Proper != null)
            {
                Proper.SetActive(false);
            }
            Rb.isKinematic = false;
            Rb.useGravity = true;
            speed = run_speed;
            LandingFlag = true;
            //CC.enabled = false;            
            Anim.CrossFade("Idle", 2.2f);
            Flag02 = false;
            Flying = false;
            Rb.velocity = Vector3.down * DownForce;
            //Debug.Log(Rb.velocity);
        }
    }
    async UniTask FlyingFirst()
    {
        float y = directioner.transform.localRotation.eulerAngles.y;
        //Debug.Log(y.ToString() + " " + MoveD.ToString());
        if (y > 315)
        {
            MoveD = 7;
        }
        else if (y > 270)
        {
            MoveD = 4;
        }
        else if (y > 225)
        {
            MoveD = 1;
        }
        else if (y > 180)
        {
            MoveD = 2;
        }
        else if (y > 135)
        {
            MoveD = 3;
        }
        else if (y > 90)
        {
            MoveD = 6;
        }
        else if (y > 45)
        {
            MoveD = 9;
        }
        else if (y >= 0)
        {
            MoveD = 8;
        }
    }
    async UniTask TriSort()
    {
        TankTriList.Clear();
        solarray = GameObject.FindGameObjectsWithTag(SolTag);
        unitarray = GameObject.FindGameObjectsWithTag(TankTag);
        foreach (GameObject obj in solarray)
        {
            float dis00 = Vector3.Distance(obj.transform.position, Rb.position);
            if (dis00 < unit_dis)
            {
                TankTriList.Add(obj);
            }
        }
        foreach (GameObject obj in unitarray)
        {
            float dis00 = Vector3.Distance(obj.transform.position, Rb.position);
            if (dis00 < unit_dis)
            {
                TankTriList.Add(obj);
            }
        }
        TankTriList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(Rb.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(Rb.position, b.transform.position));
        });
    }
    async UniTask UnitMarking()
    {
        await TriSort();
        if (TankTriList.Count > 0)
        {
            foreach (GameObject ts in TankTriList)
            {
                Warrior warrior = ts.gameObject.GetComponent<Warrior>();
                Soldier soldier = ts.gameObject.GetComponent<Soldier>();
                if (warrior != null)
                {
                    warrior.SelectOff();
                }
                if (soldier != null)
                {
                    soldier.SelectOff();
                }
            }
            if (TankTriList[0] != null)
            {
                float dis00 = Vector3.Distance(Rb.position, TankTriList[0].transform.position);
                if (dis00 < SelectDis)
                {
                    Warrior Wr = TankTriList[0].gameObject.GetComponent<Warrior>();
                    Soldier So = TankTriList[0].gameObject.GetComponent<Soldier>();
                    if (Wr != null)
                    {
                        Wr.SelectOn();
                    }
                    else if (So != null)
                    {
                        So.SelectOn();
                    }
                }
            }
        }
    }

    async UniTask GroundRay()
    {
        ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * raydist, Color.red, 5, false);
        if (Physics.Raycast(ray, out hit, raydist))
        {
            if (hit.collider.gameObject.CompareTag("RED") || hit.collider.gameObject.CompareTag("BLUE") || hit.collider.gameObject.CompareTag("Rhohei") || hit.collider.gameObject.CompareTag("Bhohei"))
            {
                int i = Random.Range(0, 3);
                Vector3 v = Vector3.forward;
                if (i == 0)
                {
                    v = Vector3.forward;
                }
                else if (i == 1)
                {
                    v = Vector3.back;
                }
                else if (i == 2)
                {
                    v = Vector3.left;
                }
                else if (i == 3)
                {
                    v = Vector3.right;
                }
                Rb.velocity = v * 3;
            }
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isGround = true;
            }
        }
        else
        {
            isGround = false;
        }
    }
    async UniTask Dead()
    {
        //Debug.Log(DeadTimer.ToString() + " " + DeadMode.ToString());
        if (DeadMode == 0)
        {
            Life = false;
            CommandMode = 10;
            CtlFlag = false;
            foreach (GameObject obj in ChGList)
            {
                Renderer Rnd = obj.GetComponent<Renderer>();
                if (Rnd != null)
                {
                    Rnd.material = AlphaZeroMat;
                }
            }
            /* if(Proper != null)
             {
                 Renderer Rndf = Proper.GetComponent<Renderer>();
                 Rndf.material = AlphaZeroMat;
             }*/
            SD.gameObject.SetActive(false);
            OilSld.gameObject.SetActive(false);
            if (Box.activeSelf)
            {
                if (BuildList.Count > 0 && AwayLunchUnit == null)
                {
                    BuildList.RemoveAt(0);
                }
                else if (AwayLunchUnit != null)
                {
                    AwayLunchUnit = null;
                }
                Box.SetActive(false);
            }
            if (Anim.GetBool("Run"))
            {
                Anim.SetBool("Run", false);
            }
            if (Anim.GetBool("Shot"))
            {
                Anim.SetBool("Shot", false);
            }
            DeadTimer = DeadTime[0];
            DeadMode = 1;
        }
        else if (DeadMode == 1 && DeadTimer > 0)
        {
            DeadTimer -= 0.1f * 60 * Time.deltaTime;
        }
        else if (DeadMode == 1 && DeadTimer <= 0)
        {
            Rb.useGravity = false;
            Rb.isKinematic = true;
            transform.position = startpos;
            if (Proper != null)
            {
                if (!Proper.activeSelf)
                {
                    Proper.SetActive(true);
                }
            }
            isGround = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            Anim.SetBool("PoseOff", true);
            DeadTimer = DeadTime[1];
            DeadMode = 2;
        }
        else if (DeadMode == 2 && DeadTimer > 0)
        {

            DeadTimer -= 0.1f * 60 * Time.deltaTime;
        }
        else if (DeadMode == 2 && DeadTimer <= 0)
        {
            if (Anim.GetBool("Idle"))
            {
                Anim.SetBool("Idle", false);
            }
            if (Anim.GetBool("TakeOff"))
            {
                Anim.SetBool("TakeOff", false);
            }
            if (Anim.GetBool("PoseOff"))
            {
                Anim.SetBool("PoseOff", false);
            }
            //Debug.Log(Anim.GetBool("Idle"));
            int i = 0;
            foreach (GameObject obj in ChGList)
            {
                Renderer Rnd = obj.GetComponent<Renderer>();
                if (Rnd != null)
                {
                    Rnd.material = MatList[i];
                    i++;
                }
            }
            /*if(Proper != null)
            {
                Renderer Rndf = Proper.GetComponent<Renderer>();
                Rndf.material = FlyOptMat;
            }*/
            SD.gameObject.SetActive(true);
            OilSld.gameObject.SetActive(true);
            HitPoint = MaxHitPoint;
            Oil = MaxOil;
            Flag02 = true;
            //CC.enabled = true;
            LandingFlag = false;
            TakeOffMode = 0;
            Flying = true;
            Rb.isKinematic = true;
            if (Proper != null)
            {
                Proper.SetActive(true);
            }
            DeadMode = 0;
            CtlFlag = true;
            Life = true;
            Life = true;
            Flag09 = true;
            Flag14 = true;
            Flag16 = true;
            Flag08 = true;
            Flag11 = true;
            CommandMode = 15;
            //Debug.Log(isGround);
        }
    }
    async UniTask GetChil(GameObject obj)
    {
        if (obj != this.gameObject)
        {
            ChGList.Add(obj);
        }
        if (obj.transform.childCount < 1)
        {
            return;
        }
        foreach (Transform ts in obj.transform)
        {
            await GetChil(ts.gameObject);
        }
    }
    async UniTask KeyDownDeath()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            HitPoint = -0.1f;
        }
    }
    private async UniTask OnCollisionEnter(Collision collision)
    {
        if (CtlFlag)
        {
            if (collision.gameObject.CompareTag("BULLET"))
            {
                HitPoint -= 1;
            }
            if (collision.gameObject.CompareTag("Missile"))
            {
                Missilemk2 Ms = collision.gameObject.GetComponent<Missilemk2>();
                HitPoint -= Ms.Pow * MissDef;
            }
        }
    }
}
