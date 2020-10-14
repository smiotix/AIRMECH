using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen02 : MonoBehaviour
{
    [SerializeField]
    private GameObject Unit;
    [SerializeField]
    private float Interval;
    private float IntervalTime;
    //private bool Flag01 = true;
    [SerializeField]
    private int Command = 1;
    // Start is called before the first frame update
    void Start()
    {
        IntervalTime = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (IntervalTime <= 0.0f)
        {
            GameObject Hohei = (GameObject)Instantiate(Unit, transform.position, Quaternion.Euler(0, 0, 0));
            Warrior Wr = Hohei.GetComponent<Warrior>();
            if(Wr != null)
            {
                Wr.CommandMode = Command;
            }
            IntervalTime = Interval;
        }
        IntervalTime -= 0.1f * 60 * Time.deltaTime;
    }
}
