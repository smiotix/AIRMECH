using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS01 : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
