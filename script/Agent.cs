using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private GameObject Goal;
    private NavMeshAgent NMA;
    // Start is called before the first frame update
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NMA.destination = Goal.transform.position;
    }
}
