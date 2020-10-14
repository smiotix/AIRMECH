using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;

public class TenguUi : MonoBehaviour
{
    [SerializeField]
    private bool Half = false;
    [SerializeField]
    private Image Select;
    private RectTransform selectRT;
    private float RTx = 225;
    [SerializeField]
    private Sprite tankbuild;
    [SerializeField]
    private Sprite hoheibuild;
    [SerializeField]
    private Sprite missilebuild;
    [SerializeField]
    private Sprite acarbuild;
    [SerializeField]
    private Sprite medicbuild;
    [SerializeField]
    private Sprite batterybuild;
    [SerializeField]
    private Sprite special01;
    [SerializeField]
    private Sprite special02;
    //[SerializeField]
    //private Sprite special03;
    [SerializeField]
    private Image build;
    private int mode = 0;
    private float AlphaTime = 30;
    private float AlphaTimer = 30;
    private float Alpha = 0.0f;
    private bool Flag01 = false;
    private bool Flag02 = true;
    private bool Flag03 = false;
    private bool Flag04 = true;
    //はたて生産
    private bool Flag05 = true;
    //椛生産
    private bool Flag06 = true;
    private int SpMode = 0;
    //private GameSystem GS;
    [System.NonSerialized]
    public List<string> Head = new List<string>();
    //[SerializeField]
    //private Image CommandSelect;
    //private RectTransform CmSelect;
    [SerializeField]
    private Image Up;
    [SerializeField]
    private Image Down;
    [SerializeField]
    private Image Left;
    [SerializeField]
    private Image Right;
    [SerializeField]
    private Image[] Builded;
    public Queue<string> Builds;
    [SerializeField]
    private float SolBuildTime;
    [SerializeField]
    private float SolCost;
    [SerializeField]
    private float AcBuildTime;
    [SerializeField]
    private float AcCost;
    [SerializeField]
    private float MissBuildTime;
    [SerializeField]
    private float MisCost;
    [SerializeField]
    private float TankBuildTime;
    [SerializeField]
    private float TankCost;
    [SerializeField]
    private float MediBuildTime;
    [SerializeField]
    private float MedCost;
    [SerializeField]
    private float BattBuidTime;
    [SerializeField]
    private float BattCost;
    [SerializeField]
    private float SP_Time;
    [SerializeField]
    private float S2_Time;
    [SerializeField]
    private float SP_Cost;
    [SerializeField]
    private float S2_Cost;
    [SerializeField]
    private float basic_income;
    [SerializeField]
    private float mono_income;
    private float float_income;
    private int int_income;
    [SerializeField]
    private TextMeshProUGUI money_txt;
    private GameObject[] bbs;
    private GameObject[] bmbs;
    private GameObject[] bunits;
    [System.NonSerialized]
    public bool CtlFlag = true;
    // Start is called before the first frame update
    async UniTask Start()
    {
        selectRT = Select.GetComponent<RectTransform>();
        selectRT.anchoredPosition = new Vector2(225, -200);
        build.color = new Color32(255, 255, 255, 0);
        Up.color = new Color32(255, 255, 255, 0);
        Down.color = new Color32(255, 255, 255, 0);
        Left.color = new Color32(255, 255, 255, 0);
        Right.color = new Color32(255, 255, 255, 0);
        //GS = transform.Find("GameSystem").gameObject.GetComponent<GameSystem>();
        foreach (Image bdi in Builded)
        {
            bdi.color = new Color32(255, 255, 255, 0);
        }
    }

    // Update is called once per frame
    async UniTask Update()
    {
        /*if (Head.Count > 0)
        {
            Debug.Log(Head[0]);
        }*/
        //Debug.Log(mode.ToString() + " " + RTx.ToString());
        await SpWatch();
        await Income();
        Command();
        Buildeds();
        if (AlphaTimer <= 0.0)
        {
            build.color = new Color32(255, 255, 255, 0);
            if (mode == 0)
            {
                if (Flag02)
                {
                    Head.Add("hohei");
                    Flag02 = false;
                }
            }
            else if (mode == 1)
            {
                if (Flag02)
                {
                    Head.Add("acar");
                    Flag02 = false;
                }
            }
            else if (mode == 2)
            {
                if (Flag02)
                {
                    Head.Add("missile");
                    Flag02 = false;
                }
            }
            else if (mode == 3)
            {
                if (Flag02)
                {
                    Head.Add("tank");
                    Flag02 = false;
                }
            }
            else if (mode == 4)
            {
                if (Flag02)
                {
                    Head.Add("medic");
                    Flag02 = false;
                }
            }
            else if (mode == 5)
            {
                if (Flag02)
                {
                    Head.Add("battery");
                    Flag02 = false;
                    SpMode = 2;
                }
            }
            else if (mode == 6)
            {
                if (Flag02)
                {
                    Head.Add("hatate");
                    Flag02 = false;
                    SpMode = 2;
                }
            }
            else if (mode == 7)
            {
                if (Flag02)
                {
                    Head.Add("momiji");
                    Flag02 = false;
                    SpMode = 2;
                }
            }
            Flag01 = false;
            Flag04 = true;
        }
        if (Input.GetKeyUp(KeyCode.Q) && CtlFlag || Input.GetKeyUp(KeyCode.JoystickButton4) && CtlFlag)
        {
            if (mode > -1)
            {
                if (mode != 0)
                {
                    RTx -= 35;
                    mode--;
                }
                selectRT.anchoredPosition = new Vector2(RTx, -200);
            }
            else
            {
                mode = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) && CtlFlag || Input.GetKeyUp(KeyCode.JoystickButton5) && CtlFlag)
        {
            if (mode != 7)
            {
                RTx += 35;
                selectRT.anchoredPosition = new Vector2(RTx, -200);
                mode++;
            }
            else
            {
                mode = 7;
            }
        }
        if ((Input.GetKeyDown(KeyCode.Space) && Flag04 && CtlFlag|| Input.GetKeyUp(KeyCode.JoystickButton3)) && Flag04 && CtlFlag)
        {
            Alpha = 0.0f;
            if (mode == 0 && float_income > SolCost)
            {
                AlphaTime = SolBuildTime;
                AlphaTimer = AlphaTime;
                build.sprite = hoheibuild;
                float_income -= SolCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 1 && float_income > AcCost)
            {
                AlphaTime = AcBuildTime;
                AlphaTimer = AlphaTime;
                build.sprite = acarbuild;
                float_income -= AcCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 2 && float_income > MisCost)
            {
                AlphaTime = MissBuildTime;
                AlphaTimer = AlphaTime;
                build.sprite = missilebuild;
                float_income -= MisCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 3 && float_income > TankCost)
            {
                AlphaTime = TankBuildTime;
                AlphaTimer = AlphaTime;
                build.sprite = tankbuild;
                float_income -= TankCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 4 && float_income > MedCost)
            {
                AlphaTime = MediBuildTime;
                AlphaTimer = AlphaTime;
                build.sprite = medicbuild;
                float_income -= MedCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 5 && float_income > BattCost)
            {
                AlphaTime = BattBuidTime;
                AlphaTimer = AlphaTime;
                build.sprite = batterybuild;
                float_income -= BattCost;
                Flag01 = true;
                Flag04 = false;
            }
            if (mode == 6 && float_income > SP_Cost && Flag05)
            {
                AlphaTime = SP_Time;
                AlphaTimer = AlphaTime;
                build.sprite = special01;
                float_income -= SP_Cost;
                Flag01 = true;
                Flag04 = false;
                SpMode = 1;
            }
            if (mode == 7 && float_income > S2_Cost && Flag06)
            {
                AlphaTime = SP_Time;
                AlphaTimer = AlphaTime;
                build.sprite = special01;
                float_income -= SP_Cost;
                Flag01 = true;
                Flag04 = false;
                SpMode = 1;
            }
        }
        if (Flag01)
        {
            AlphaTimer -= 0.1f * 60 * Time.deltaTime;
            Alpha = ((AlphaTime - AlphaTimer) / AlphaTime) * 255f;
            build.color = new Color32(255, 255, 255, (byte)Alpha);
            Flag02 = true;
        }
    }
    async UniTask Income()
    {
        bbs = GameObject.FindGameObjectsWithTag("BminiBase");
        bmbs = GameObject.FindGameObjectsWithTag("BMiniBase_Minus");
        float_income += (((float)(bbs.Length + bmbs.Length) * mono_income) + basic_income) * 60 * Time.deltaTime;
        int_income = (int)float_income;
        money_txt.text = int_income.ToString();
    }
    async UniTask SpWatch()
    {
        bunits = GameObject.FindGameObjectsWithTag("BLUE");
        Flag05 = true;
        Flag06 = false;
        foreach (GameObject obj in bunits)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            if (Wr.UnitType == "hatate")
            {
                Flag05 = false;
            }
        }
        foreach (GameObject obj in bunits)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            if (Wr.UnitType == "momiji")
            {
                Flag06 = false;
            }
        }
        SpMode = 0;
    }
    void Buildeds()
    {
        int i = 0;
        foreach (Image bdi in Builded)
        {
            if (i >= Head.Count)
            {
                bdi.color = new Color32(255, 255, 255, 0);
            }
            else if (Head.Count > 0)
            {
                string unit = Head[Head.Count - i - 1];
                if ("hohei" == unit)
                {
                    bdi.sprite = hoheibuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("tank" == unit)
                {
                    bdi.sprite = tankbuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("missile" == unit)
                {
                    bdi.sprite = missilebuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("acar" == unit)
                {
                    bdi.sprite = acarbuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("medic" == unit)
                {
                    bdi.sprite = medicbuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("battery" == unit)
                {
                    bdi.sprite = batterybuild;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
                else if ("goliath" == unit)
                {
                    bdi.sprite = special01;
                    bdi.color = new Color32(255, 255, 255, 255);
                }
            }
            i++;
        }
    }
    void Command()
    {
        if (Flag03)
        {
            Up.color = new Color32(255, 255, 255, 255);
            Down.color = new Color32(255, 255, 255, 255);
            Left.color = new Color32(255, 255, 255, 255);
            Right.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            build.color = new Color32(255, 255, 255, 0);
            Up.color = new Color32(255, 255, 255, 0);
            Down.color = new Color32(255, 255, 255, 0);
            Left.color = new Color32(255, 255, 255, 0);
            Right.color = new Color32(255, 255, 255, 0);
        }
    }
    public void CommandOn()
    {
        Flag03 = true;
    }
    public void CommandOff()
    {
        Flag03 = false;
    }
    public bool CommandState()
    {
        return Flag03;
    }
    public void UnitClear()
    {
        //Debug.Log("DDDD");
        Alpha = 0.0f;
        AlphaTimer = AlphaTime;
        build.color = new Color32(255, 255, 255, 0);
    }
}
