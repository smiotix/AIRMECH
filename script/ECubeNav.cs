using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ECubeNav : MonoBehaviour
{
    private NavMeshAgent NMA;
    private GameObject Target;
    [SerializeField]
    private float end_dis = 15;
    private float dis;
    // Start is called before the first frame update
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Target = GameObject.FindGameObjectWithTag("BLUE");
        dis = Vector3.Distance(Target.transform.position, transform.position);
        if (dis > end_dis)
        {
            NMA.SetDestination(Target.transform.position);
        }
        else
        {
            NMA.enabled = false;
        }
    }
}
