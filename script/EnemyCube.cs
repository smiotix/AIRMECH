using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField]
    private int HitPoint;
    [SerializeField]
    private string EBULLET;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(HitPoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(EBULLET))
        {
            HitPoint -= 1;
        }
    }
}
