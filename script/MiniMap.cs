using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    private Sprite BLUE;
    [SerializeField]
    private Sprite RED;
    [SerializeField]
    private Sprite BLACK;
    [SerializeField]
    private Sprite StarBlue;
    [SerializeField]
    private Sprite StarRed;
    private Dictionary<string, Transform> BaseDict = new Dictionary<string, Transform>();
    [SerializeField]
    private Image[] Imgs;
    private GameObject[] mbs;
    private GameObject[] bbs;
    private GameObject[] bmbs;
    private GameObject[] rbs;
    private GameObject[] rmbs;
    private GameObject[] BPs;
    private GameObject[] RPs;
    [SerializeField]
    private GameObject Hblue;
    [SerializeField]
    private GameObject Hred;
    private int i = 0;
    // Start is called before the first frame update
    async UniTask Start()
    {
        foreach(Image img in Imgs)
        {
            img.rectTransform.sizeDelta = new Vector2(5, 5);
        }
    }

    // Update is called once per frame
    async UniTask Update()
    {
        await Reflash();
    }
    async UniTask Reflash()
    {
        i = 0;
        mbs = GameObject.FindGameObjectsWithTag("minibase");
        bbs = GameObject.FindGameObjectsWithTag("BminiBase");
        bmbs = GameObject.FindGameObjectsWithTag("BMiniBase_Minus");
        rbs = GameObject.FindGameObjectsWithTag("RminiBase");
        rmbs = GameObject.FindGameObjectsWithTag("RMiniBase_Minus");
        BPs = GameObject.FindGameObjectsWithTag("BPlayer");
        RPs = GameObject.FindGameObjectsWithTag("RPlayer");
        if(mbs.Length > 0)
        {
            foreach(GameObject obj in mbs)
            {
                Imgs[i].sprite = BLACK;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.parent.transform.position.x / 10, obj.transform.parent.position.z / 10);
                i++;
            }
        }
        if(bbs.Length > 0)
        {
            foreach (GameObject obj in bbs)
            {
                Imgs[i].sprite = BLUE;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.parent.transform.position.x / 10, obj.transform.parent.position.z / 10);
                i++;
            }
        }
        if (bmbs.Length > 0)
        {
            foreach (GameObject obj in bmbs)
            {
                Imgs[i].sprite = BLUE;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.parent.transform.position.x / 10, obj.transform.parent.position.z / 10);
                i++;
            }
        }
        if (rbs.Length > 0)
        {
            foreach (GameObject obj in rbs)
            {
                Imgs[i].sprite = RED;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.parent.transform.position.x / 10, obj.transform.parent.position.z / 10);
                i++;
            }
        }
        if (rmbs.Length > 0)
        {
            foreach (GameObject obj in rmbs)
            {
                Imgs[i].sprite = RED;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.parent.transform.position.x / 10, obj.transform.parent.position.z / 10);
                i++;
            }
        }
        Imgs[i].rectTransform.anchoredPosition = new Vector2(Hblue.transform.position.x / 10, Hblue.transform.position.z / 10);
        Imgs[i].sprite = BLUE;
        i++;
        Imgs[i].rectTransform.anchoredPosition = new Vector2(Hred.transform.position.x / 10, Hred.transform.position.z / 10);
        Imgs[i].sprite = RED;
        i++;
        if(BPs.Length > 0)
        {
            foreach(GameObject obj in BPs)
            {
                Imgs[i].rectTransform.sizeDelta = new Vector2(8, 8);
                Imgs[i].sprite = StarBlue;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.position.x / 10, obj.transform.position.z / 10);
                i++;
            }
        }
        //Debug.Log(i);
        if (RPs.Length > 0)
        {
            foreach (GameObject obj in RPs)
            {
                Imgs[i].rectTransform.sizeDelta = new Vector2(8, 8);
                Imgs[i].sprite = StarRed;
                Imgs[i].rectTransform.anchoredPosition = new Vector2(obj.transform.position.x / 10, obj.transform.position.z / 10);
                //Debug.Log(Imgs[i].rectTransform.sizeDelta);
                i++;
            }
        }
        while(i < Imgs.Length)
        {
            if (Imgs[i].gameObject.activeSelf)
            {
                Imgs[i].gameObject.SetActive(false);
            }
            i++;
        }
        //Debug.Log(i);
    }
}
