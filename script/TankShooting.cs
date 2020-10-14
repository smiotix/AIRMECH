using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private float Interval;
    private float shotInterval;
    // Start is called before the first frame update
    void Start()
    {
        shotInterval = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (shotInterval <= 0)
        {
            GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(bullet.transform.forward * shotSpeed);
            Destroy(bullet, 8.0f);
            shotInterval = Interval;
        }
        shotInterval -= 0.1f * 60 * Time.deltaTime;
    }
}
