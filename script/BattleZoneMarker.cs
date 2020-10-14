using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneMarker : MonoBehaviour
{
    [SerializeField]
    private GameObject CPU;
    [SerializeField]
    private string ATAG01;
    [SerializeField]
    private string ATAG02;
    private List<Transform> UnitList = new List<Transform>();
    [SerializeField]
    private float Interval;
    private float Timer;
    private bool Flag = false;
    // Start is called before the first frame update
    void Start()
    {
        Timer = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer <= 0f)
        {
            GetObject();
            Timer = Interval;
        }
        Timer -= 0.1f * 60 * Time.deltaTime;
    }
    void GetObject()
    {
        UnitList.Clear();
        GameObject[] objA = GameObject.FindGameObjectsWithTag(ATAG01);
        GameObject[] objB = GameObject.FindGameObjectsWithTag(ATAG02);
        foreach(GameObject obj in objA)
        {
            Soldier sol = obj.GetComponent<Soldier>();
            if(sol != null)
            {
                if (sol.BattleFlag)
                {
                    UnitList.Add(obj.transform);
                }
            }
        }
        foreach(GameObject obj in objB)
        {
            Warrior Wr = obj.GetComponent<Warrior>();
            if(Wr != null)
            {
                if (Wr.BattleFlag)
                {
                    UnitList.Add(obj.transform);
                }
            }
        }
        if(UnitList.Count > 1)
        {
            UnitList.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.gameObject.transform.position, b.transform.position));
            });
            Flag = true;
        }
    }
    public Transform BattlePostion()
    {
        if (Flag)
        {
            return UnitList[0];
        }
        else
        {
            return null;
        }
    }
}
