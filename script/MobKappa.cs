using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobKappa : MonoBehaviour
{
    [SerializeField]
    private Material[] hairmats;
    [SerializeField]
    private Material[] facemats;
    [SerializeField]
    private GameObject[] Hairs;
    [SerializeField]
    private GameObject Glass;
    [SerializeField]
    private GameObject Face;
    private Renderer FRnd;
    [System.NonSerialized]
    public List<int> Numbers = new List<int>();
    private int HairNum = 0;
    private int HMint = 0;
    private int GlassNum = 0;
    private int FaceInt = 0;
    private int i = 0;
    private int j = 0;
    // Start is called before the first frame update
    void Start()
    {
        FRnd = Face.GetComponent<Renderer>();
        //Debug.Log(Hairs.Length);
        FaceInt = Random.Range(0, facemats.Length);
        HairNum = Random.Range(0, Hairs.Length);
        Numbers.Add(HairNum);
        GlassNum = Random.Range(0, 5);
        if(GlassNum == 1)
        {
            Numbers.Add(2);
        }
        else
        {
            Numbers.Add(1);
        }
        HMint = Random.Range(0, hairmats.Length);
        Numbers.Add(HMint);
        Numbers.Add(FaceInt);
        LooksChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LooksChange()
    {
        if(Numbers.Count > 3)
        {
            i = 0;
            foreach(GameObject obj in Hairs)
            {
                if(i == Numbers[0])
                {
                    j = 0;
                    Renderer Rnd = obj.GetComponent<Renderer>();
                    foreach(Material mat in hairmats)
                    {
                        if(j == Numbers[2])
                        {
                            Rnd.material = mat;
                        }
                        j++;
                    }
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(false);
                }
                i++;
            }
            if (Numbers[1] == 2)
            {
                Glass.SetActive(true);
            }
            else
            {
                Glass.SetActive(false);
            }
            i = 0;
            foreach (Material mat in facemats)
            {
                if(i == Numbers[3])
                {
                    FRnd.material = mat;
                }
                i++;
            }
        }
    }
}
