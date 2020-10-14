using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class EBase : MonoBehaviour
{
    public string Ally;
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
    private int[] CommandArray;
    [SerializeField]
    private int[] BZCommandArray;
    [SerializeField]
    private float ZoneFloat = 250;
    private int Commandint = 0;
    private int Commandint2 = 0;
    private GameObject[] Units01;
    private GameObject[] Units02;
    private GameObject[] Units03;
    private GameObject[] Units04;
    // Start is called before the first frame update
    async UniTask Start()
    {

    }

    // Update is called once per frame
    async UniTask Update()
    {
        //Debug.Log(Commandint.ToString() + " " + CommandArray.Length.ToString());
    }
    void GetBattleZone()
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
        BZList.Sort(delegate (Transform a, Transform b)
        {
            return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
            .CompareTo(
              Vector3.Distance(this.gameObject.transform.position, b.transform.position));
        });
        if (BZList.Count > 0)
        {
            float dis = Vector3.Distance(BZList[0].position, transform.position);
            //Debug.Log(dis);
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
        GetBattleZone();
        if (BZList.Count > 0)
        {
            float dis = Vector3.Distance(BZList[0].position, transform.position);
            if (dis < ZoneFloat)
            {
                if (BZCommandArray.Length - 1 > Commandint2)
                {
                    int i = BZCommandArray[Commandint2];
                    Commandint2++;
                    return i;
                }
                else if (BZCommandArray.Length - 1 > Commandint2)
                {
                    int i = BZCommandArray[Commandint2];
                    Commandint2 = 0;
                    return i;
                }
                else
                {
                    Commandint = 0;
                    return 0;
                }
            }
            else
            {
                if (CommandArray.Length - 1 > Commandint)
                {
                    int i = CommandArray[Commandint];
                    Commandint++;
                    return i;
                }
                else if (CommandArray.Length - 1 == Commandint)
                {
                    int i = CommandArray[Commandint];
                    Commandint = 0;
                    return i;
                }
                else
                {
                    Commandint = 0;
                    return 0;
                }
            }
        }
        else
        {
            if (CommandArray.Length - 1 > Commandint)
            {
                int i = CommandArray[Commandint];
                Commandint++;
                return i;
            }
            else if (CommandArray.Length - 1 <= Commandint)
            {
                int i = CommandArray[Commandint];
                Commandint = 0;
                //Debug.Log(CommandArray.Length);
                return i;
            }
            else
            {
                Commandint = 0;
                return 0;
            }
        }
    }
    private async UniTask OnTriggerEnter(Collider other)
    {
        //Debug.Log("c");
        if (other.CompareTag("BPlayer") || other.CompareTag("RPlayer"))
        {

            CPU cpu = other.GetComponent<CPU>();
            if (cpu != null)
            {
                cpu.StayBase = true;
            }
            Fighter jiki = other.GetComponent<Fighter>();
            if (jiki != null)
            {
                jiki.StayBase = true;
            }
        }
    }
    private async UniTask OnTriggerExit(Collider other)
    {
        //Debug.Log("v");
        if (other.CompareTag("BPlayer") || other.CompareTag("RPlayer"))
        {
            CPU cpu = other.GetComponent<CPU>();
            if (cpu != null)
            {
                cpu.StayBase = false;
            }
            Fighter jiki = other.GetComponent<Fighter>();
            if (jiki != null)
            {
                jiki.StayBase = false;
            }
        }
    }
}
