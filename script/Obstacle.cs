using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    async UniTask Start()
    {
        
    }

    // Update is called once per frame
    async UniTask Update()
    {
        
    }
    private async UniTask OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BPlayer") || other.CompareTag("RPlayer"))
        {
            CPU cpu = other.GetComponent<CPU>();
            Fighter fgr = other.GetComponent<Fighter>();
            if(cpu != null)
            {
                cpu.Obs = true;
            }
            if(fgr != null)
            {
                fgr.Obs = true;
            }
        }
    }
    private async UniTask OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BPlayer") || other.CompareTag("RPlayer"))
        {
            CPU cpu = other.GetComponent<CPU>();
            Fighter fgr = other.GetComponent<Fighter>();
            if (cpu != null)
            {
                cpu.Obs = false;
            }
            if (fgr != null)
            {
                fgr.Obs = false;
            }
        }
    }
}
