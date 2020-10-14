using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuBarRot : MonoBehaviour
{
    private GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Cam.transform.rotation;
    }
}
