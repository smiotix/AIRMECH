using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CpuEnemyDetecter : MonoBehaviour
{
    [System.NonSerialized]
    public bool On = false;
    [System.NonSerialized]
    public List<Transform> EList = new List<Transform>();
    private List<Transform> tList = new List<Transform>();
    [SerializeField]
    private string ETag01;
    [SerializeField]
    private string ETag02;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EList.Count > 0)
        {
            tList.Clear();
            foreach(Transform ts in EList)
            {
                tList.Add(ts);
            }
            foreach(Transform ts in tList)
            {
                if(ts == null)
                {
                    EList.Remove(ts);
                }
            }
            EList.Sort(delegate (Transform a, Transform b)
            {
                return Vector3.Distance(this.gameObject.transform.position, a.transform.position)
                .CompareTo(
                  Vector3.Distance(this.gameObject.transform.position, b.transform.position));
            });
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ETag01) || other.CompareTag(ETag02))
        {
            EList.Remove(other.transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (On)
        {
            if (other.CompareTag(ETag01) || other.CompareTag(ETag02))
            {
                EList.Add(other.transform);
            }
        }
    }
}
