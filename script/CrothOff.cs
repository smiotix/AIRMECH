using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrothOff : MonoBehaviour
{
    private GameObject[] Cams;
    private List<GameObject> trses = new List<GameObject>();
    private List<GameObject> allChildren = new List<GameObject>();
    private List<Cloth> clothes = new List<Cloth>();
    private float dis = 0;
    private float dis2 = 0;
    [SerializeField]
    private float Camdis = 15;
    // Start is called before the first frame update
    void Start()
    {
        Cams = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (Transform childTransform in this.transform)
        {
            trses.Add(childTransform.gameObject);
        }
        GetChildren(this.gameObject);
        foreach(GameObject obj in allChildren)
        {
            Cloth c = obj.GetComponent<Cloth>();
            if(c != null)
            {
                clothes.Add(c);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, Cams[0].transform.position);
        foreach(GameObject obj in Cams)
        {
            dis2 = Vector3.Distance(transform.position, obj.transform.position);
            if(dis2 < dis)
            {
                dis = dis2;
            }
        }
        if(dis > Camdis)
        {
            foreach(Cloth c in clothes)
            {
                c.enabled = false;
            }
        }
        else
        {
            foreach (Cloth c in clothes)
            {
                c.enabled = true;
            }
        }
        //Debug.Log(dis);
    }
    void GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
            GetChildren(ob.gameObject);
        }
    }
}
