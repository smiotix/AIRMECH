using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MobKarasu : MonoBehaviour
{
    [SerializeField]
    private GameObject Face;
    [SerializeField]
    private GameObject Scart;
    [SerializeField]
    private GameObject[] Hairs;
    [SerializeField]
    private GameObject[] Tis;
    [SerializeField]
    private GameObject[] Colparts;
    [SerializeField]
    private GameObject Ribbon;
    [SerializeField]
    private Material[] FaceMats;
    [SerializeField]
    private Material[] ColorMats;
    [SerializeField]
    private Material[] ScartMats;
    [SerializeField]
    private Material[] HairCols;
    [SerializeField]
    private Material[] ShdMats;
    private Renderer FRnd;
    private Renderer SRnd;
    [System.NonSerialized]
    public List<int> Numbers = new List<int>();
    private int Fint = 0;
    private int Cint = 0;
    private int Tint = 0;
    private int Hint = 0;
    private int Sint = 0;
    private int HCint = 0;
    private int SHint = 0;
    private GameObject Ti;
    [SerializeField]
    private GameObject Shild;
    // Start is called before the first frame update
    async UniTask Start()
    {
        FRnd = Face.GetComponent<Renderer>();
        SRnd = Scart.GetComponent<Renderer>();
        Fint = Random.Range(0, FaceMats.Length);
        Tint = Random.Range(0, Tis.Length);
        Sint = Random.Range(0, ScartMats.Length * 2);
        Cint = Random.Range(0, ColorMats.Length);
        Hint = Random.Range(0, Hairs.Length);
        HCint = Random.Range(0, HairCols.Length);
        SHint = Random.Range(0, ShdMats.Length);

        Numbers.Add(Fint);
        Numbers.Add(Tint);
        Numbers.Add(Cint);
        Numbers.Add(Sint);
        Numbers.Add(Hint);
        Numbers.Add(HCint);
        Numbers.Add(SHint);

        await ChangeLooks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async UniTask ChangeLooks()
    {
        int i = 0;
        int j = 0;
        foreach(Material mat in FaceMats)
        {
            if (i == Numbers[0])
            {
                FRnd.material = mat;
            }
            i++;
        }
        i = 0;
        foreach(GameObject obj in Tis)
        {
            if(i == Numbers[1])
            {
                obj.SetActive(true);
                Ti = obj;
            }
            else
            {
                obj.SetActive(false);
            }
            i++;
        }
        i = 0;        
        foreach(Material mat in ColorMats)
        {
            if(i == Numbers[2])
            {
                foreach(GameObject obj in Colparts)
                {
                    Renderer Rnd = obj.GetComponent<Renderer>();
                    if(Rnd != null)
                    {
                        Rnd.material = mat;
                    }
                }
                Renderer RRnd = Ribbon.GetComponent<Renderer>();
                RRnd.material = mat;
                j = 0;
                if (Numbers[3] < ScartMats.Length)
                {
                    foreach(Material smat in ScartMats)
                    {
                        if(j == Numbers[3])
                        {
                            SRnd.material = smat;
                        }
                        j++;
                    }
                }
                else
                {
                    SRnd.material = mat;
                }
                Renderer TRnd = Ti.GetComponent<Renderer>();
                TRnd.material = mat;
            }
            i++;
        }
        i = 0;
        foreach(GameObject obj in Hairs)
        {
            if(i == Numbers[4])
            {
                obj.SetActive(true);
                j = 0;
                foreach(Material mat in HairCols)
                {
                    if(j == Numbers[5])
                    {
                        Renderer Rnd = obj.GetComponent<Renderer>();
                        Rnd.material = mat;
                    }
                    j++;
                }
            }
            else
            {
                obj.SetActive(false);
            }
            i++;
        }
        i = 0;
        if(Shild != null)
        {
            Renderer ShRnd = Shild.GetComponent<Renderer>();
            foreach(Material mat in ShdMats)
            {
                if(i == Numbers[6])
                {
                    ShRnd.material = mat;
                }
                i++;
            }
        }
    }
}
