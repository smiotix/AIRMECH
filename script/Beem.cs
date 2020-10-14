using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class Beem : MonoBehaviour
{
    private string EnemyTag01;
    private string EnemyTag02;
    private string EnemyTag03;
    [SerializeField]
    private float PowSpeed = 60;
    [SerializeField]
    private GameObject Player;
    // Start is called before the first frame update
    async UniTask Start()
    {
        if (Player.CompareTag("BPlayer"))
        {
            EnemyTag01 = "RED";
            EnemyTag02 = "Rhohei";
            EnemyTag03 = "RPlayer";
        }
        else
        {
            EnemyTag01 = "BLUE";
            EnemyTag02 = "Bhohei";
            EnemyTag03 = "BPlayer";
        }
        //Debug.Log(EnemyTag03);
    }

    // Update is called once per frame
    async UniTask Update()
    {
        transform.rotation = Player.transform.rotation;
    }
    private async UniTask OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(EnemyTag01) || other.gameObject.CompareTag(EnemyTag02) || other.gameObject.CompareTag(EnemyTag03))
        {
            Soldier Sd = other.gameObject.GetComponent<Soldier>();
            Warrior Wr = other.gameObject.GetComponent<Warrior>();
            Fighter Fht = other.gameObject.GetComponent<Fighter>();
            CPU Cp = other.gameObject.GetComponent<CPU>();
            if (Sd != null)
            {
                Sd.HitPoint -= 0.1f * (PowSpeed * 1.4f) * Time.deltaTime;
            }
            else if (Wr != null)
            {
                Wr.HitPoint -= 0.1f * (PowSpeed * 0.4f) * Time.deltaTime;
            }
            else if(Fht != null)
            {
                Fht.HitPoint -= 0.1f * (PowSpeed * 0.4f) * Time.deltaTime;
            }
        }
    }
}
