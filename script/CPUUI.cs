using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUUI : MonoBehaviour
{
    [SerializeField]
    private Text UnitCountTxt;
    private int solint = 0;
    private int tankint = 0;
    private int missint = 0;
    private int acarint = 0;
    private int mediint = 0;
    private int battint = 0;
    [SerializeField]
    private GameObject CPUUNIT;
    private CPU CPU0;
    // Start is called before the first frame update
    void Start()
    {
        CPU0 = CPUUNIT.GetComponent<CPU>();
    }

    // Update is called once per frame
    void Update()
    {
        solint = 0;
        tankint = 0;
        missint = 0;
        mediint = 0;
        battint = 0;
        acarint = 0;
        foreach(string st in CPU0.BuildList)
        {
            if(st == "hohei")
            {
                solint++;
            }
            if(st == "tank")
            {
                tankint++;
            }
            if(st == "missile")
            {
                missint++;
            }
            if(st == "acar")
            {
                acarint++;
            }
            if(st == "medic")
            {
                mediint++;
            }
            if(st == "battery")
            {
                battint++;
            }
        }
        UnitCountTxt.text = "TANK :" + tankint.ToString() + " " + "SOL :" + solint.ToString() + " " + "MISS :" + missint.ToString() + " " + "AC :" + acarint.ToString() + " MED:" + mediint.ToString() + " BAT:" + battint.ToString() + "MONEY:" + CPU0.int_income.ToString();
    }
}
