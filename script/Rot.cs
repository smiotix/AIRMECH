using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField]
    private float x = 0;
    [SerializeField]
    private float y = 0;
    [SerializeField]
    private float z = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.1f * x * Time.deltaTime, 0.1f * y * Time.deltaTime, 0.1f * z * Time.deltaTime);
        Debug.Log(transform.localRotation.eulerAngles);
    }
}
