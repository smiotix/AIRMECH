using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeBaseHP : MonoBehaviour
{
    [SerializeField]
    private Slider Sd;
    [SerializeField]
    private float MaxHitPoint;
    private float HitPoint;
    [SerializeField]
    private string Ally;
    [SerializeField]
    private TextMeshProUGUI WinText;
    private string Enemy;
    // Start is called before the first frame update
    void Start()
    {
        if (Sd != null)
        {
            Sd.value = 1f;
        }
        HitPoint = MaxHitPoint;
        if (WinText != null)
        {
            WinText.gameObject.SetActive(false);
        }
        if(Ally == "BLUE")
        {
            Enemy = "RED";
        }
        else
        {
            Enemy = "BLUE";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Sd != null && WinText != null)
        {
            DisplayHP();
        }
    }
    void DisplayHP()
    {
        if(HitPoint <= 0.0f)
        {
            Time.timeScale = 0f;
            WinText.gameObject.SetActive(true);
            WinText.text = Enemy + " WIN";
        }
        Sd.value = HitPoint / MaxHitPoint;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BULLET"))
        {
            HitPoint -= 1;
        }
    }
}
