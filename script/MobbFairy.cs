using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobbFairy : MonoBehaviour
{
    [SerializeField]
    private Material[] Cols;
    [SerializeField]
    private Material[] FaceMats;
    [System.NonSerialized]
    public List<int> Numbers = new List<int>();
    private int Col_one = 0;
    private int Col_two = 0;
    private int Col_three = 0;
    private int HairCol = 0;
    private int FaceNum = 0;
    [SerializeField]
    private GameObject[] Col_ones;
    [SerializeField]
    private GameObject[] Col_twos;
    [SerializeField]
    private GameObject[] Col_threes;
    [SerializeField]
    private GameObject Hair;
    [SerializeField]
    private GameObject Face;
    // Start is called before the first frame update
    void Start()
    {
        Col_one = Random.Range(0, Cols.Length);
        do
        {
            Col_two = Random.Range(0, Cols.Length);
        }
        while (Col_one == Col_two);
        do
        {
            Col_three = Random.Range(0, Cols.Length);
        }
        while (Col_three == Col_two || Col_three == Col_one);
        do
        {
            HairCol = Random.Range(0, Cols.Length);
        }
        while (HairCol == Col_two || HairCol == Col_one || HairCol == Col_three);
        FaceNum = Random.Range(0, FaceMats.Length);
        Numbers.Add(Col_one);
        Numbers.Add(Col_two);
        Numbers.Add(Col_three);
        Numbers.Add(HairCol);
        Numbers.Add(FaceNum);
        ChangeColor();
        //Debug.Log(Col_one.ToString() + " " + Col_two.ToString() + " " + Col_three.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor()
    {
        foreach(GameObject obj in Col_ones)
        {
            Renderer Rnd = obj.GetComponent<Renderer>();
            if(Rnd != null)
            {
                Rnd.material = Cols[Numbers[0]];
            }
        }
        foreach (GameObject obj in Col_twos)
        {
            Renderer Rnd = obj.GetComponent<Renderer>();
            if (Rnd != null)
            {
                Rnd.material = Cols[Numbers[1]];
            }
        }
        foreach (GameObject obj in Col_threes)
        {
            Renderer Rnd = obj.GetComponent<Renderer>();
            if (Rnd != null)
            {
                Rnd.material = Cols[Numbers[2]];
            }
        }
        Renderer HRnd = Hair.GetComponent<Renderer>();
        HRnd.material = Cols[Numbers[3]];
        Renderer FRnd = Face.GetComponent<Renderer>();
        FRnd.material = FaceMats[Numbers[4]];
    }
}
