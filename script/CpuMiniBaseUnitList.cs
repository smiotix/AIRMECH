using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuMiniBaseUnitList : MonoBehaviour
{
    private List<Transform> BCUList = new List<Transform>();
    private List<Transform> RCUList = new List<Transform>();
    [SerializeField]
    private string BUnitTag01;
    [SerializeField]
    private string BUnitTag02;
    [SerializeField]
    private string RUnitTag01;
    [SerializeField]
    private string RUnitTag02;
    [System.NonSerialized]
    public MiniBase MB;
    public int priority_A;
    public int priority_B;
    private int old_priority_A;
    private int old_priority_B;
    [System.NonSerialized]
    public Transform Ts;
    // Start is called before the first frame update
    void Start()
    {
        MB = transform.parent.gameObject.GetComponent<MiniBase>();
        old_priority_A = priority_A;
        old_priority_B = priority_B;
        Ts = transform;
    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> tList1 = new List<Transform>();
        foreach (Transform ts in RCUList)
        {
            tList1.Add(ts);
        }
        foreach (Transform ts in tList1)
        {
            if (ts == null)
            {
                RCUList.Remove(ts);
            }
        }
        List<Transform> tList2 = new List<Transform>();
        foreach (Transform ts in BCUList)
        {
            tList2.Add(ts);
        }
        foreach (Transform ts in tList2)
        {
            if (ts == null)
            {
                BCUList.Remove(ts);
            }
        }
        if(this.gameObject.tag == "BminiBase")
        {
            if(RCUList.Count > 3)
            {
                priority_B = 10;
            }
            else
            {
                priority_B = old_priority_B;
            }
        }
        if(this.gameObject.tag == "RminiBase")
        {
            if(BCUList.Count > 3)
            {
                priority_A = 10;
            }
            else
            {
                priority_A = old_priority_A;
            }
        }
    }
    public int ReBCLC()
    {
        return BCUList.Count;
    }
    public int ReRCLC()
    {
        return RCUList.Count;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(BUnitTag02))
        {
            BCUList.Add(other.transform);
        }
        if (other.gameObject.CompareTag(RUnitTag02))
        {
            RCUList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(BUnitTag02))
        {
            BCUList.Remove(other.transform);
        }
        if (other.gameObject.CompareTag(RUnitTag02))
        {
            RCUList.Remove(other.transform);
        }
    }
}
