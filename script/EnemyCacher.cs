using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class EnemyCacher : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Shooter;
    private List<Transform> EList = new List<Transform>();
    [SerializeField]
    private string Etag01;
    [SerializeField]
    private string Etag02;
    private GameObject Target;
    [System.NonSerialized]
    public bool Flag = false;
    private bool Flag01 = false;
    private int TarNo = 0;
    private List<Transform> tList = new List<Transform>();
    // Start is called before the first frame update
    async UniTask Start()
    {
        //Shooter.SetActive(false);
    }

    // Update is called once per frame
    async UniTask Update()
    {
        if (Flag)
        {
            Attack();
        }
        else
        {
            Flag01 = false;
        }
    }
    async UniTask Attack()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //Debug.Log(EList.Count);
            if (EList.Count > 0)
            {
                EListSort();
                if (TarNo < EList.Count - 1)
                {
                    TarNo++;
                }
                else
                {
                    TarNo = 0;
                }
                Target = EList[TarNo].gameObject;
            }
            Flag01 = true;
        }
        if (Flag01)
        {
            if (Target.gameObject != null)
            {
                Player.transform.LookAt(Target.transform.position);
                Shooter.transform.LookAt(Target.transform.position);
            }
        }
    }
    void EListSort()
    {
        tList.Clear();
        foreach (Transform ts in EList)
        {
            tList.Add(ts);
        }
        foreach (Transform ts in tList)
        {
            if (ts == null)
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
    private async UniTask OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Etag01) || other.gameObject.CompareTag(Etag02))
        {
            EList.Add(other.gameObject.transform);
        }
    }
    private async UniTask OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Etag01) || other.gameObject.CompareTag(Etag02))
        {
            EList.Remove(other.gameObject.transform);
        }
    }
}
