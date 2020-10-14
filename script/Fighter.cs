using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Globalization;

public class Fighter : MonoBehaviour
{
    private string ABtag01 = "BminiBase";
    private string ABtag02 = "BMiniBase_Minus";
    private string EU01 = "RED";
    private string EU02 = "Rhohei";
    private string EU03 = "RPlayer";
    private GameObject[] EUs01;
    private GameObject[] EUs02;
    private GameObject[] EUs03;
    private List<GameObject> EList = new List<GameObject>();
    //private CharacterController CC;
    [SerializeField]
    private float fly_speed;
    [SerializeField]
    private float run_speed;
    private float speed;
    [SerializeField]
    private GameObject Box;
    [SerializeField]
    private GameObject FLYOption;
    [System.NonSerialized]
    public bool StayBase = false;
    //private GameSystem GS;
    private List<string> Head;
    [SerializeField]
    private float fallspeed;
    private string TankTag;
    private string SolTag;
    [SerializeField]
    private GameObject Hohei;
    [SerializeField]
    private GameObject missilew;
    [SerializeField]
    private GameObject acar;
    [SerializeField]
    private GameObject Tank;
    [SerializeField]
    private GameObject Medic;
    [SerializeField]
    private GameObject Battery;
    [SerializeField]
    private GameObject Special01;
    [SerializeField]
    private GameObject Special02;
    [SerializeField]
    private GameObject Special03;
    [SerializeField]
    private GameObject Shooter;
    private Alice_UI AMU;
    private TenguUi TUI;
    private FairyUi FUI;
    private MIKO_UI MUI;
    private HANIWA_UI HUI;
    private Animator Anim;
    private Rigidbody Rb;
    private bool isGround = false;
    [SerializeField]
    private float DownForce;
    private bool Flag01 = true;
    private bool Flag02 = true;
    private bool Flag03 = true;
    private bool Flag04 = false;
    private bool Flag05 = true;
    private bool Flag06 = true;
    //ゴリアテ
    private bool Flag07 = false;
    private bool LandingFlag = false;
    private int TakeOffMode = 0;
    private float TakeOff_y;
    private float Ground_y;
    private Quaternion rot;
    private float Leap_t = 0.0f;
    private List<Transform> TankTriList = new List<Transform>();
    private List<Transform> mBList = new List<Transform>();
    private GameObject[] solarray;
    private GameObject[] unitarray;
    private GameObject[] mbarray01;
    private GameObject[] mbarray02;
    //private List<Transform> tList = new List<Transform>();
    //private List<Transform> SolidList = new List<Transform>();
    private float BoxHitPoint;
    private int BoxMissint = 0;
    private int BoxAcint = 0;
    private int BoxTankint = 0;
    private int BoxMediint = 0;
    private int BoxSolint = 0;
    private int BoxBattint = 0;
    private int BoxSpint = 0;
    [SerializeField]
    private GameObject ReleasePos;
    [SerializeField]
    private float unit_dis;
    private int CommandMode = 0;
    private float v = 0;
    private List<string> LunchList = new List<string>();
    //private EnemyCacher EC;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private Slider Sld;
    [SerializeField]
    private float MaxHP;
    [System.NonSerialized]
    public float HitPoint;
    private Vector3 FirstPoint;
    //private string LunchString;
    [SerializeField]
    private float MissDef = 0.6f;
    //private BoxCollider BC;
    private Ray ray;
    private RaycastHit hit;
    private float raydist = 1.5f;
    [System.NonSerialized]
    public bool Flying = true;
    private float TakeOff_t = 0;
    [SerializeField]
    private float MaxOil;
    private float Oil;
    [SerializeField]
    private Slider OilSld;
    [SerializeField]
    private Slider BoxSld;
    private float BoxMaxHp;
    [SerializeField]
    private GameObject HBase;
    [SerializeField]
    private float SelectDis = 15;
    [SerializeField]
    private float EnemysearchDis = 150;
    [System.NonSerialized]
    public bool Obs;
    private string LunchSpecial = null;
    private float limit_x = 1000;
    private float limit_mx = 0;
    private float limit_z = 1000;
    private float limit_mz = 0;
    private MobKarasu MbK;
    private MobbFairy MbF;
    private List<int> LooksintList = new List<int>();
    private List<Material> MatList = new List<Material>();
    private List<GameObject> ChGList = new List<GameObject>();
    [SerializeField]
    private Material AlphaZeroMat;
    private bool CtlFlag = true;
    private int DeadMode = 0;
    private float[] DeadTime = new float[2] {30,30};
    private float DeadTimer;
    [System.NonSerialized]
    public bool UICtl;
    [System.NonSerialized]
    public bool Life = true;
    // Start is called before the first frame update
    async UniTask Start()
    {
        await GetChil(this.gameObject);
        foreach (GameObject obj in ChGList)
        {
            Renderer Rnd = obj.GetComponent<Renderer>();
            if(Rnd != null)
            {
                MatList.Add(Rnd.material);
            }
        }
        LooksintList.Clear();
        //CC = GetComponent<CharacterController>();
        //BC = GetComponent<BoxCollider>();
        Box.SetActive(false);
        Shooter.SetActive(false);
        //GS = UI.transform.Find("GameSystem").gameObject.GetComponent<GameSystem>();
        //EC = this.transform.Find("EnemyCacher").gameObject.GetComponent<EnemyCacher>();
        AMU = UI.GetComponent<Alice_UI>();
        TUI = UI.GetComponent<TenguUi>();
        FUI = UI.GetComponent<FairyUi>();
        MUI = UI.GetComponent<MIKO_UI>();
        HUI = UI.GetComponent<HANIWA_UI>();
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody>();
        Rb.isKinematic = true;
        TakeOff_y = transform.position.y;
        rot = transform.rotation;
        speed = fly_speed;
        HitPoint = MaxHP;
        Sld.value = 1f;
        FirstPoint = transform.position;
        TankTag = Tank.tag;
        SolTag = Hohei.tag;
        BoxSld.value = 1f;
        BoxSld.gameObject.SetActive(false);
        OilSld.value = 1f;
        Oil = MaxOil;
        if (AMU != null)
        {
            Head = AMU.Head;
            UICtl = AMU.CtlFlag;
        }
        else if (TUI != null)
        {
            Head = TUI.Head;
            UICtl = TUI.CtlFlag;
        }
        else if(FUI != null)
        {
            Head = FUI.Head;
        }
        else if(MUI != null)
        {
            Head = MUI.Head;
        }
        else if(HUI != null)
        {
            Head = HUI.Head;
        }
        await StartUnit();
    }

    // Update is called once per frame
    async UniTask Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Z))
        {
            Debug.Log("Pushed Z");
        }*/
        //Debug.Log(transform.position);
        //Debug.Log(StayBase);
        //Debug.Log(isGround);
        await InputKBJ();
        await ChkHP();
        await Release();
        await Lanch();
        await TargetEnemy();
        await KeyDownDeath();
    }
    async UniTask StartUnit()
    {
        Vector3 UnitPos = HBase.transform.position + new Vector3(8, 0, 35);
        GameObject tank01 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Warrior Wr = tank01.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = HBase.transform.position + new Vector3(-8, 0, 35);
        GameObject tank02 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank02.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = HBase.transform.position + new Vector3(35, 0, 8);
        GameObject tank03 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        if (Wr != null)
        {
            Wr = tank03.GetComponent<Warrior>();
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = HBase.transform.position + new Vector3(35, 0, -8);
        GameObject tank04 = (GameObject)Instantiate(Tank, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank04.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "tank";
        }
        UnitPos = HBase.transform.position + new Vector3(35, 0, 0);
        GameObject tank05 = (GameObject)Instantiate(missilew, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank05.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "missile";
        }
        UnitPos = HBase.transform.position + new Vector3(0, 0, 35);
        GameObject tank06 = (GameObject)Instantiate(missilew, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank06.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 0;
            Wr.UnitType = "missile";
        }
        UnitPos = HBase.transform.position + new Vector3(35, 0, 35);
        GameObject tank07 = (GameObject)Instantiate(Medic, UnitPos, Quaternion.Euler(0, 0, 0));
        Wr = tank07.GetComponent<Warrior>();
        if (Wr != null)
        {
            Wr.CommandMode = 4;
            Wr.UnitType = "medic";
        }
    }
    async UniTask TargetEnemy()
    {
        if (!Flying)
        {
            if (Input.GetKeyUp(KeyCode.Tab)|| Input.GetAxis("Axis 3") < 0f)
            {
                await GetEList();
                if (EList.Count > 0)
                {
                    transform.LookAt(EList[0].transform.position);
                }
            }
        }
    }
    async UniTask GetEList()
    {
        EList.Clear();
        EUs01 = GameObject.FindGameObjectsWithTag(EU01);
        EUs02 = GameObject.FindGameObjectsWithTag(EU02);
        EUs03 = GameObject.FindGameObjectsWithTag(EU03);
        foreach (GameObject obj in EUs01)
        {
            float dis_e = Vector3.Distance(obj.transform.position, Rb.position);
            if (dis_e < EnemysearchDis)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in EUs02)
        {
            float dis_e = Vector3.Distance(obj.transform.position, Rb.position);
            if (dis_e < EnemysearchDis)
            {
                EList.Add(obj);
            }
        }
        foreach (GameObject obj in EUs03)
        {
            float dis_e = Vector3.Distance(obj.transform.position, Rb.position);
            CPU Cpu00 = obj.GetComponent<CPU>();
            Fighter Fgt00 = obj.GetComponent<Fighter>();
            if (Cpu00 != null)
            {
                if (dis_e < EnemysearchDis && !Cpu00.Flying)
                {
                    EList.Add(obj);
                }
            }
            if (Fgt00 != null)
            {
                if (dis_e < EnemysearchDis && !Fgt00.Flying)
                {
                    EList.Add(obj);
                }
            }
        }
        EList.Sort(delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(Rb.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(Rb.position, b.transform.position));
        });

    }
    async UniTask ChkHP()
    {
        if (Sld.gameObject.activeSelf)
        {
            Sld.value = HitPoint / MaxHP;
        }
        if (OilSld.gameObject.activeSelf)
        {
            OilSld.value = Oil / MaxOil;
        }
        if (BoxSld.gameObject.activeSelf)
        {
            BoxSld.value = BoxHitPoint / BoxMaxHp;
        }
        if (HitPoint <= 0.0f)
        {
            await Dead();
        }
        if (StayBase && HitPoint < MaxHP && DeadMode == 0)
        {
            HitPoint += 0.1f * 30 * Time.deltaTime;
            BoxHitPoint += 0.1f * 30 * Time.deltaTime;
        }
        if (StayBase && Oil < MaxOil && DeadMode == 0)
        {
            Oil += 0.1f * (MaxOil / 2) * Time.deltaTime;
        }
    }
    async UniTask InputKBJ()
    {
        if (CtlFlag)
        {
            if (Flying && transform.position.y != TakeOff_y)
            {
                transform.position = new Vector3(transform.position.x, TakeOff_y, transform.position.z);
            }
            if (isGround)
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
                if (Oil > 0f)
                {
                    speed = fly_speed;
                }
                else
                {
                    speed = fly_speed / 5f;
                }
                //EC.Flag = false;
            }
            if (Input.GetKey(KeyCode.RightControl) || Input.GetAxis("Axis 3") > 0f)
            {
                Anim.SetBool("Shot", true);
                if (!Anim.IsInTransition(0))
                {
                    Shooter.SetActive(true);
                }
            }
            else if (Input.GetKeyUp(KeyCode.RightControl) || Input.GetAxis("Axis 3") == 0f)
            {
                Anim.SetBool("Shot", false);
                Shooter.SetActive(false);
            }

            if ((Input.GetKey(KeyCode.Keypad4) || Input.GetAxis("Axis 6") < 0f && Input.GetAxis("Axis 7") == 0f) && Rb.position.x > limit_mx)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad6) || Input.GetAxis("Axis 6") > 0f && Input.GetAxis("Axis 7") == 0f) && Rb.position.x < limit_x)
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
                    if (isGround)
                    {
                        transform.localRotation = Quaternion.LookRotation(Vector3.right);
                        /*if (!CC.enabled)
                        {
                            CC.enabled = true;
                        }*/
                        Flag01 = true;
                        Rb.isKinematic = false;
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }

            }
            else if ((Input.GetKey(KeyCode.Keypad8) || Input.GetAxis("Axis 7") > 0f && Input.GetAxis("Axis 6") == 0f) && Rb.position.z < limit_z)
            {
                if (transform.rotation.eulerAngles.y > 180)
                {
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
                }
                else
                {
                    Flag03 = true;
                    if (isGround)
                    {
                        transform.localRotation = Quaternion.LookRotation(Vector3.forward);
                        /*if (!CC.enabled)
                        {
                            CC.enabled = true;
                        }*/
                        Flag01 = true;
                        Rb.isKinematic = false;
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad2) || Input.GetAxis("Axis 7") < 0f && Input.GetAxis("Axis 6") == 0f) && Rb.position.z > limit_mz)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad3) || Input.GetAxis("Axis 7") < 0f && Input.GetAxis("Axis 6") > 0f) && Rb.position.x < limit_x && Rb.position.z > limit_mz)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad9) || Input.GetAxis("Axis 6") > 0f && Input.GetAxis("Axis 7") > 0f) && Rb.position.x > limit_mx && Rb.position.z < limit_z)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad1) || Input.GetAxis("Axis 6") < 0f && Input.GetAxis("Axis 7") < 0f) && Rb.position.x > limit_mx && Rb.position.z > limit_mz)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else if ((Input.GetKey(KeyCode.Keypad7) || Input.GetAxis("Axis 7") > 0f && Input.GetAxis("Axis 6") < 0f) && Rb.position.x > limit_mx && Rb.position.z < limit_z)
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
                        Anim.SetBool("Run", true);
                        rot = transform.rotation;
                    }
                    Move();
                }
            }
            else
            {
                if (isGround)
                {
                    if (Flag01)
                    {
                        Rb.isKinematic = true;
                        Rb.isKinematic = false;
                        //CC.enabled = false;
                        //BC.enabled = false;
                    }

                }
                Anim.SetBool("Run", false);
            }
            if (isGround && !Flag02)
            {
                LandingFlag = true;
                Flag02 = true;
            }
            await TakeOff();
            await GroundRay();
            Landing();
            if (LandingFlag && !isGround)
            {
                transform.rotation = rot;
            }
        }
    }
    void Move()
    {
        //Debug.Log(transform.forward);
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        //CC.Move(transform.forward * speed * Time.deltaTime);
        Oil -= (speed / 100) * 30 * Time.deltaTime;
    }
    async UniTask TakeOff()
    {
        if (LandingFlag && CtlFlag)
        {
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.JoystickButton0))
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
            }
            if (TakeOffMode > 0)
            {
                Anim.SetBool("Idle", false);
                if (transform.position.y != TakeOff_y)
                {
                    TakeOff_t += 0.05f * 60 * Time.deltaTime;
                    transform.position = Vector3.Lerp(new Vector3(transform.position.x, Ground_y, transform.position.z), new Vector3(transform.position.x, TakeOff_y, transform.position.z), TakeOff_t);
                }
                if (TakeOff_t >= 1.0f)
                {
                    if (FLYOption != null) { FLYOption.SetActive(true); }
                    Flag02 = true;
                    //CC.enabled = true;
                    LandingFlag = false;
                    TakeOffMode = 0;
                    Flying = true;
                    Rb.isKinematic = true;
                    //Rb.velocity = Vector3.zero;
                }
            }
        }
    }
    void Landing()
    {
        if (!LandingFlag && !Box.activeSelf && !StayBase && !Obs && CtlFlag)
        {
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.JoystickButton0))
            {
                if (Flag02)
                {
                    if(FLYOption != null)
                    {
                        FLYOption.SetActive(false);
                    }
                    //CC.enabled = false;
                    Rb.isKinematic = false;
                    Anim.CrossFade("Idle", 4.7f);
                    //Anim.SetBool("Landing",true);
                    Rb.useGravity = true;
                    //Rb.AddForce(Vector3.down * DownForce); 
                    Rb.velocity = Vector3.down * DownForce;
                    //GTtag.tag = AUnitTag;
                    Flag02 = false;
                    Flying = false;
                }
            }
        }
    }
    async UniTask Lanch()
    {
        if (StayBase && CtlFlag)
        {
            if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.JoystickButton0))
            {
                //Debug.Log(Head.Count.ToString() + Box.activeSelf.ToString()); ;
                if (!Box.activeSelf)
                {
                    if (Head.Count > 0)
                    {
                        if (Head[0] == "goliath" && !Flag07)
                        {
                            if (AMU != null)
                            {
                                AMU.CommandOn();
                                AMU.UnitClear();
                            }
                            Flag07 = true;
                        }
                        //Debug.Log(GS.Head[GS.Head.Count - 1]);
                        if (Head[0] == "tank" && !Flag07 || Head[0] == "missile" && !Flag07 || Head[0] == "acar" || Head[0] == "momiji" || Head[0] == "hatate" || Head[0] == "luna" || Head[0] == "sunny" || Head[0] == "cirno" || Head[0] == "crown" || Head[0] == "ete" || Head[0] == "dai1" || Head[0] == "dai2" || Head[0] == "dai3" || Head[0] == "mayumi" || Head[0] == "marisa" || Head[0] == "aun" && !Flag07)
                        {
                            //LunchString = "tank";
                            //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                            Box.SetActive(true);
                            if (AMU != null)
                            {
                                AMU.CommandOn();
                                AMU.UnitClear();
                            }
                            else if (TUI != null)
                            {
                                TUI.CommandOn();
                                TUI.UnitClear();
                            }
                            else if(FUI != null)
                            {
                                FUI.CommandOn();
                                FUI.UnitClear();
                            }
                            else if (MUI != null)
                            {
                                MUI.CommandOn();
                                MUI.UnitClear();
                            }
                            else if (HUI != null)
                            {
                                HUI.CommandOn();
                                HUI.UnitClear();
                            }
                        }
                        if (Head[0] == "hohei" && !Flag07 || Head[0] == "medic" && !Flag07 || Head[0] == "battery" && !Flag07)
                        {
                            //LunchString = "hohei";
                            //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                            Box.SetActive(true);
                            if (AMU != null)
                            {
                                //AMU.CommandOn();
                                AMU.UnitClear();
                            }
                            else if (TUI != null)
                            {
                                //TUI.CommandOn();
                                TUI.UnitClear();
                            }
                            else if(FUI != null)
                            {
                                FUI.UnitClear();
                            }
                            else if (MUI != null)
                            {
                                MUI.UnitClear();
                            }
                            else if (HUI != null)
                            {
                                HUI.UnitClear();
                            }
                        }
                    }
                }
            }
        }
        else if (LunchList.Count > 0)
        {
            string LunchUnit = LunchList[LunchList.Count - 1];
            if (!Box.activeSelf && BoxTankint > 0 && LunchUnit == "tank" || !Box.activeSelf && BoxMissint > 0 && LunchUnit == "missile" || !Box.activeSelf && BoxAcint > 0 && LunchUnit == "acar")
            {
                Box.SetActive(true);
                if (AMU != null)
                {
                    AMU.CommandOn();
                }
                else if (TUI != null)
                {
                    TUI.CommandOn();
                }
                else if(FUI != null)
                {
                    FUI.UnitClear();
                }
                Flag04 = true;
                LunchList.RemoveAt(LunchList.Count - 1);
            }
            else if (!Box.activeSelf && BoxSolint > 0 && LunchUnit == "hohei" || !Box.activeSelf && BoxMediint > 0 && LunchUnit == "medic" || !Box.activeSelf && BoxBattint > 0 && LunchUnit == "battery")
            {
                Box.SetActive(true);
                //AMU.CommandOn();
                Flag04 = true;
                LunchList.RemoveAt(LunchList.Count - 1);
            }
        }
    }
    async UniTask Release()
    {
        await GroundLunch();
        if (!Flag07 && CtlFlag)
        {
            if (!StayBase && !Obs)
            {
                if (AMU != null)
                {
                    //Debug.Log(AMU.CommandState());
                    if (AMU.CommandState())
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                        {
                            CommandMode = 1;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                        {
                            CommandMode = 0;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                        {
                            CommandMode = 2;
                            await UnitGen();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKey(KeyCode.JoystickButton0))
                        {
                            await UnitGen();
                        }
                    }
                }
                else if (TUI != null)
                {
                    if (TUI.CommandState())
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                        {
                            CommandMode = 1;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                        {
                            CommandMode = 0;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                        {
                            CommandMode = 2;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxis("Axis 2") > 0f && Input.GetAxis("Axis 1") == 0f)
                        {
                            CommandMode = 3;
                            await UnitGen();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKey(KeyCode.JoystickButton0))
                        {
                            await UnitGen();
                        }
                    }
                }
                else if (FUI != null)
                {
                    if (FUI.CommandState())
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                        {
                            CommandMode = 1;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                        {
                            CommandMode = 0;
                            await UnitGen();
                        }
                        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                        {
                            CommandMode = 2;
                            await UnitGen();
                        }
                    }
                    else
                    {
                        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKey(KeyCode.JoystickButton0))
                        {
                            await UnitGen();
                        }
                    }
                }
            }
        }
        else
        {
            if (AMU != null)
            {
                if (AMU.CommandState())
                {
                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                    {
                        CommandMode = 1;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                    {
                        CommandMode = 0;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                    {
                        CommandMode = 2;
                        await GenGoliath();
                    }
                }
            }
            else if (FUI != null)
            {
                if (FUI.CommandState())
                {
                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                    {
                        CommandMode = 1;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                    {
                        CommandMode = 0;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                    {
                        CommandMode = 2;
                        await GenGoliath();
                    }
                }
            }
            else if (MUI != null)
            {
                if (MUI.CommandState())
                {
                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                    {
                        CommandMode = 1;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                    {
                        CommandMode = 0;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                    {
                        CommandMode = 2;
                        await GenGoliath();
                    }
                }
            }
            else if (HUI != null)
            {
                if (HUI.CommandState())
                {
                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Axis 2") < 0f && Input.GetAxis("Axis 1") == 0f)
                    {
                        CommandMode = 1;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") < 0f)
                    {
                        CommandMode = 0;
                        await GenGoliath();
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetAxis("Axis 2") == 0f && Input.GetAxis("Axis 1") > 0f)
                    {
                        CommandMode = 2;
                        await GenGoliath();
                    }
                }
            }
        }
        //await GroundLunch();
    }
    async UniTask UnitGen()
    {
        if (Head.Count > 0 && BoxTankint < 1 && BoxSolint < 1 && BoxMissint < 1 && BoxAcint < 1 && BoxMediint < 1)
        {
            if (Box.activeSelf)
            {
                //Debug.Log(GS.Head[0]);
                Box.SetActive(false);
                // unit = GS.Head[GS.Head.Count - 1];
                if (Head[0] == "battery")
                {
                    GameObject tank = (GameObject)Instantiate(Battery, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = 5;
                    Wr.UnitType = "battery";
                    await Wr.SetTag();
                }
                if (Head[0] == "medic")
                {
                    GameObject tank = (GameObject)Instantiate(Medic, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = 4;
                    Wr.UnitType = "medic";
                    await Wr.SetTag();
                }
                else if (Head[0] == "hohei")
                {
                    //GS.hoheiint -= 1;
                    //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                    GameObject hohei = (GameObject)Instantiate(Hohei, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    //Rigidbody hoheitRb = hohei.GetComponent<Rigidbody>();
                    //hoheitRb.velocity = Vector3.down * fallspeed;
                }
                else if (Head[0] == "acar")
                {
                    //GS.tankint -= 1;
                    //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                    GameObject tank = (GameObject)Instantiate(acar, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "acar";
                    await Wr.SetTag();
                    //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    //tankRb.AddForce(Vector3.down * fallspeed);
                }
                else if (Head[0] == "missile")
                {
                    //GS.tankint -= 1;
                    //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                    GameObject tank = (GameObject)Instantiate(missilew, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "missile";
                    await Wr.SetTag();
                    //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    //tankRb.AddForce(Vector3.down * fallspeed);
                }
                else if (Head[0] == "tank")
                {
                    //GS.tankint -= 1;
                    //GS.Head.Remove(GS.Head[GS.Head.Count - 1]);
                    GameObject tank = (GameObject)Instantiate(Tank, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "tank";
                    await Wr.SetTag();
                    //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
                    //tankRb.AddForce(Vector3.down * fallspeed);
                }
                else if (Head[0] == "momiji")
                {
                    GameObject tank = (GameObject)Instantiate(Special01, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "momiji";
                    await Wr.SetTag();
                }
                else if (Head[0] == "hatate")
                {
                    GameObject tank = (GameObject)Instantiate(Special02, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "hatate";
                    await Wr.SetTag();
                }
                else if (Head[0] == "luna")
                {
                    GameObject obj = Special01.transform.Find("lunachild").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "luna";
                    await Wr.SetTag();
                }
                else if (Head[0] == "sunny")
                {
                    GameObject obj = Special01.transform.Find("sunny").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "sunny";
                    await Wr.SetTag();
                }
                else if (Head[0] == "cirno")
                {
                    GameObject obj = Special02.transform.Find("cirno").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "cirno";
                    await Wr.SetTag();
                }
                else if (Head[0] == "crown")
                {
                    GameObject obj = Special02.transform.Find("cirno").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "cirno";
                    await Wr.SetTag();
                }
                else if (Head[0] == "ete")
                {
                    GameObject obj = Special02.transform.Find("eternity").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "ete";
                    await Wr.SetTag();
                }
                else if (Head[0] == "dai1")
                {
                    GameObject obj = Special03.transform.Find("dai1").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "dai1";
                    await Wr.SetTag();
                }
                else if (Head[0] == "dai2")
                {
                    GameObject obj = Special03.transform.Find("dai2").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "dai2";
                    await Wr.SetTag();
                }
                else if (Head[0] == "dai3")
                {
                    GameObject obj = Special03.transform.Find("dai3").gameObject;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "dai3";
                    await Wr.SetTag();
                }
                else if (Head[0] == "mayumi")
                {
                    GameObject obj = Special01;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "mayumi";
                    await Wr.SetTag();
                }
                else if (Head[0] == "marisa")
                {
                    GameObject obj = Special01;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "marisa";
                    await Wr.SetTag();
                }
                else if (Head[0] == "aun")
                {
                    GameObject obj = Special01;
                    GameObject tank = (GameObject)Instantiate(obj, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
                    Warrior Wr = tank.GetComponent<Warrior>();
                    Wr.CommandMode = CommandMode;
                    Wr.UnitType = "aun";
                    await Wr.SetTag();
                }
                Head.RemoveAt(0);
            }
        }
        else if (BoxTankint > 0 && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            BoxTankint -= 1;
            GameObject tank = (GameObject)Instantiate(Tank, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "tank";
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();            
            if(tMbk != null)
            {
                foreach(int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = tank.GetComponent<MobbFairy>();
            if(tMbF != null)
            {
                foreach(int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (BoxMissint > 0 && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            BoxMissint -= 1;
            GameObject tank = (GameObject)Instantiate(missilew, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "missile";
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();
            if (tMbk != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = tank.GetComponent<MobbFairy>();
            if (tMbF != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (BoxAcint > 0 && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            BoxAcint -= 1;
            GameObject tank = (GameObject)Instantiate(acar, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "acar";
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();
            if (tMbk != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = tank.GetComponent<MobbFairy>();
            if (tMbF != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "momiji" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "momiji";
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "hatate" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "hatate";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "luna" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01.transform.Find("lunachild").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "luna";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "sunny" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01.transform.Find("sunny").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "sunny";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "cirno" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special02.transform.Find("cirno").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "cirno";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "crown" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special02.transform.Find("crown").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "crown";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "ete" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special02.transform.Find("eternity").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "ete";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "dai1" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special03.transform.Find("dai1").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "dai1";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "dai2" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special03.transform.Find("dai2").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "dai2";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "dai3" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special03.transform.Find("dai3").gameObject, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "dai3";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "mayumi" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "mayumi";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "marisa" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special01, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "marisa";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (LunchSpecial == "aun" && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            //BoxAcint -= 1;
            LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Special02, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = CommandMode;
            Wr.UnitType = "aun";
            await Wr.SetTag();
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (BoxBattint > 0 && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            BoxBattint -= 1;
            //LunchSpecial = null;
            GameObject tank = (GameObject)Instantiate(Battery, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = 5;
            Wr.UnitType = "battery";
            await Wr.SetTag();
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();
            if (tMbk != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = tank.GetComponent<MobbFairy>();
            if (tMbF != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (BoxMediint > 0 && Flag04 && Box.activeSelf)
        {
            //Debug.Log("WWW");
            BoxMediint -= 1;
            GameObject tank = (GameObject)Instantiate(Medic, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Warrior Wr = tank.GetComponent<Warrior>();
            Wr.HitPoint = BoxHitPoint;
            Wr.CommandMode = 4;
            Wr.UnitType = "medic";
            await Wr.SetTag();
            MobKarasu tMbk = tank.GetComponent<MobKarasu>();
            if (tMbk != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = tank.GetComponent<MobbFairy>();
            if (tMbF != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            //Rigidbody tankRb = tank.GetComponent<Rigidbody>();
            //tankRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        else if (BoxSolint > 0 && Flag04 && Box.activeSelf)
        {
            BoxSolint -= 1;
            GameObject Sol = (GameObject)Instantiate(Hohei, ReleasePos.transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Soldier So = Sol.GetComponent<Soldier>();
            MobKarasu tMbk = Sol.GetComponent<MobKarasu>();
            if (tMbk != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbk.Numbers.Add(l);
                }
                await tMbk.ChangeLooks();
            }
            MobbFairy tMbF = Sol.GetComponent<MobbFairy>();
            if (tMbF != null)
            {
                foreach (int l in LooksintList)
                {
                    tMbF.Numbers.Add(l);
                }
                tMbF.ChangeColor();
            }
            So.HitPoint = BoxHitPoint;
            //Rigidbody SolRb = Sol.GetComponent<Rigidbody>();
            //SolRb.AddForce(Vector3.down * fallspeed);
            Box.SetActive(false);
            Flag04 = false;
            Flag05 = true;
        }
        if (AMU != null)
        {
            AMU.CommandOff();
        }
        else if (TUI != null)
        {
            TUI.CommandOff();
        }
        else if(FUI != null)
        {
            FUI.CommandOff();
        }
        else if(MUI != null)
        {
            MUI.CommandOff();
        }
        else if(HUI != null)
        {
            HUI.CommandOff();
        }
        if (BoxSld.gameObject.activeSelf)
        {
            BoxSld.gameObject.SetActive(false);
        }
    }
    async UniTask GroundRay()
    {
        //Debug.Log(isGround);
        //RaycastHit hit;
        ray = new Ray(transform.position + new Vector3(0,0.5f,0), Vector3.down);
        if (Physics.Raycast(ray, out hit, raydist))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                isGround = true;
            }
        }
        else
        {
            isGround = false;
        }
        Debug.DrawRay(ray.origin, ray.direction * raydist, Color.red, 5, false);
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
                TankTriList.Add(obj.transform);
            }
        }
        foreach (GameObject obj in unitarray)
        {
            float dis00 = Vector3.Distance(obj.transform.position, Rb.position);
            if (dis00 < unit_dis)
            {
                TankTriList.Add(obj.transform);
            }
        }
        TankTriList.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(this.gameObject.transform.position, b.transform.position));
        });
    }
    async UniTask GetmBList()
    {
        mBList.Clear();
        mbarray01 = GameObject.FindGameObjectsWithTag(ABtag01);
        mbarray02 = GameObject.FindGameObjectsWithTag(ABtag02);
        foreach (GameObject obj in mbarray01)
        {
            mBList.Add(obj.transform);
        }
        foreach (GameObject obj in mbarray02)
        {
            mBList.Add(obj.transform);
        }
        mBList.Add(HBase.transform);
        if (mBList.Count > 1)
        {
            mBList.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.gameObject.transform.position, b.transform.position));
            });
        }
    }
    async UniTask GenGoliath()
    {
        await GetmBList();
        Vector3 GenPos = mBList[0].transform.position + new Vector3(18, 8, 18);
        GameObject tank = (GameObject)Instantiate(Special01, GenPos, Quaternion.Euler(0, 0, 0));
        Warrior Wr = tank.GetComponent<Warrior>();
        Wr.CommandMode = CommandMode;
        Wr.UnitType = "goliath";
        await Wr.SetTag();
        Rigidbody tankRb = tank.GetComponent<Rigidbody>();
        tankRb.AddForce(Vector3.down * fallspeed);
        if (AMU != null)
        {
            AMU.CommandOff();
        }
        Flag07 = false;
        Head.RemoveAt(0);
    }
    async UniTask GroundLunch()
    {
        if (!isGround && !Flag07)
        {
            await TriSort();
            if (TankTriList.Count > 0)
            {
                foreach (Transform ts in TankTriList)
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
                    float dis00 = Vector3.Distance(Rb.position, TankTriList[0].position);
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
                        if (Input.GetKeyUp(KeyCode.Z) && !Box.activeSelf && !StayBase && !Flag04)
                        {
                            MbK = null;
                            MbF = null;
                            LooksintList.Clear();
                            LunchSpecial = null;
                            string type;
                            if (Wr != null)
                            {                                
                                type = Wr.UnitType;
                                if (type == "tank")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();                                        
                                        if (MbK != null)
                                        {
                                            foreach(int l in MbK.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                        if (MbF != null)
                                        {
                                            foreach(int l in MbF.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        Flag04 = true;
                                        LunchList.Add("tank");
                                        BoxTankint += 1;
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "acar")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();
                                        if (MbK != null)
                                        {
                                            foreach (int l in MbK.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                        if (MbF != null)
                                        {
                                            foreach (int l in MbF.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        Flag04 = true;
                                        LunchList.Add("acar");
                                        BoxAcint += 1;
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "momiji")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("momiji");
                                        LunchSpecial = "momiji";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "hatate")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("hatate");
                                        LunchSpecial = "hatate";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "luna")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("luna");
                                        LunchSpecial = "luna";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "sunny")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("sunny");
                                        LunchSpecial = "sunny";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "crown")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("crown");
                                        LunchSpecial = "crown";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "dai1")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("dai1");
                                        LunchSpecial = "dai1";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "dai2")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("dai2");
                                        LunchSpecial = "dai2";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "dai3")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("dai3");
                                        LunchSpecial = "dai3";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "mayumi")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("mayumi");
                                        LunchSpecial = "mayumi";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "marisa")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("marisa");
                                        LunchSpecial = "marisa";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "aun")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        Flag04 = true;
                                        LunchList.Add("aun");
                                        LunchSpecial = "aun";
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "missile")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();
                                        if (MbK != null)
                                        {
                                            foreach (int l in MbK.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                        if (MbF != null)
                                        {
                                            foreach (int l in MbF.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        Flag04 = true;
                                        LunchList.Add("missile");
                                        BoxMissint += 1;
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                        BoxSld.gameObject.SetActive(true);
                                    }
                                }
                                else if (type == "medic")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();
                                        if (MbK != null)
                                        {
                                            foreach (int l in MbK.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                        if (MbF != null)
                                        {
                                            foreach (int l in MbF.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        BoxSld.gameObject.SetActive(true);
                                        Flag04 = true;
                                        LunchList.Add("medic");
                                        BoxMediint += 1;
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                    }
                                }
                                else if (type == "battery")
                                {
                                    if (Flag05 && Wr.ReTurnSelect())
                                    {
                                        MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();
                                        if (MbK != null)
                                        {
                                            foreach (int l in MbK.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                        if (MbF != null)
                                        {
                                            foreach (int l in MbF.Numbers)
                                            {
                                                LooksintList.Add(l);
                                            }
                                        }
                                        BoxSld.gameObject.SetActive(true);
                                        Flag04 = true;
                                        LunchList.Add("battery");
                                        BoxBattint += 1;
                                        Flag05 = false;
                                        BoxHitPoint = Wr.HitPoint;
                                        BoxMaxHp = Wr.MaxHitPoint;
                                        Destroy(TankTriList[0].gameObject);
                                    }
                                }
                            }
                            else if (So != null)
                            {
                                if (Flag05 && So.ReTurnSelect())
                                {
                                    MbK = TankTriList[0].gameObject.GetComponent<MobKarasu>();
                                    if (MbK != null)
                                    {
                                        foreach (int l in MbK.Numbers)
                                        {
                                            LooksintList.Add(l);
                                        }
                                    }
                                    MbF = TankTriList[0].gameObject.GetComponent<MobbFairy>();
                                    if (MbF != null)
                                    {
                                        foreach (int l in MbF.Numbers)
                                        {
                                            LooksintList.Add(l);
                                        }
                                    }
                                    BoxSld.gameObject.SetActive(true);
                                    Flag04 = true;
                                    LunchList.Add("hohei");
                                    BoxSolint += 1;
                                    Flag05 = false;
                                    BoxHitPoint = So.HitPoint;
                                    BoxMaxHp = So.MaxHitPoint;
                                    Destroy(TankTriList[0].gameObject);

                                }
                            }
                        }
                    }
                }
            }
        }
    }
    async UniTask Dead()
    {
        Debug.Log(DeadTimer.ToString() + " " + DeadMode.ToString());
        if (DeadMode == 0)
        {
            CtlFlag = false;
            foreach (GameObject obj in ChGList)
            {
                Renderer Rnd = obj.GetComponent<Renderer>();
                if (Rnd != null)
                {
                    Rnd.material = AlphaZeroMat;
                }
            }
            Sld.gameObject.SetActive(false);
            OilSld.gameObject.SetActive(false);
            if (Box.activeSelf)
            {
                if (Head.Count > 0 && BoxTankint < 1 && BoxSolint < 1 && BoxMissint < 1 && BoxAcint < 1 && BoxMediint < 1)
                {
                    Head.RemoveAt(0);
                }
                else if (LunchSpecial != null)
                {
                    LunchSpecial = null;
                    Flag04 = false;
                    Flag05 = true;
                }
                else
                {
                    BoxTankint = 0;
                    BoxSolint = 0;
                    BoxMissint = 0;
                    BoxAcint = 0;
                    BoxMediint = 0;
                    Flag04 = false;
                    Flag05 = true;
                }
                Box.SetActive(false);
            }
            UICtl = false;
            /*if(TUI != null)
            {
                TUI.CtlFlag = false;
            }*/
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
        else if(DeadMode == 1 && DeadTimer > 0)
        {
            DeadTimer -= 0.1f * 60 * Time.deltaTime;
        }
        else if (DeadMode == 1 && DeadTimer <= 0)
        {
            Rb.useGravity = false;
            Rb.isKinematic = true;
            transform.position = FirstPoint;
            if (FLYOption != null)
            {
                if (!FLYOption.activeSelf)
                {
                    FLYOption.SetActive(true);
                }
            }
            isGround = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            Anim.SetBool("PoseOff", true);
            DeadTimer = DeadTime[1];
            DeadMode = 2;
        }
        else if(DeadMode == 2 && DeadTimer > 0)
        {

            DeadTimer -= 0.1f * 60 * Time.deltaTime;
        }
        else if(DeadMode == 2 && DeadTimer <= 0)
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
            foreach(GameObject obj in ChGList)
            {
                Renderer Rnd = obj.GetComponent<Renderer>();
                if(Rnd != null)
                {
                    Rnd.material = MatList[i];
                    i++;
                }
            }
            Sld.gameObject.SetActive(true);
            OilSld.gameObject.SetActive(true);
            HitPoint = MaxHP;
            Oil = MaxOil;
            Flag02 = true;
            LandingFlag = false;
            TakeOffMode = 0;
            Flying = true;
            CtlFlag = true;
            /*if(TUI != null)
            {
                TUI.CtlFlag = true;
            }*/
            DeadMode = 0;
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
