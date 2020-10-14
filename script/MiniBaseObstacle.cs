using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniBaseObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject MiniBase;
    private NavMeshObstacle NMO;
    [SerializeField]
    private string SolTag01 = "Bhohei";
    [SerializeField]
    private string SolTag02 = "Rhohei";
    // Start is called before the first frame update
    void Start()
    {
        NMO = MiniBase.GetComponent<NavMeshObstacle>();
        NMO.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(SolTag01)|| other.gameObject.CompareTag(SolTag02))
        {
            NMO.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(SolTag01) || other.gameObject.CompareTag(SolTag02))
        {
            NMO.enabled = true; ;
        }
    }
}
 