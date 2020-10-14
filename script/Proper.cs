using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proper : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f * speed * Time.deltaTime, 0);
    }
}
