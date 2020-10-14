using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject Fighter;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float shotSpeed;
    [SerializeField]
    private float Interval;
    private float shotInterval;
    private Rigidbody FRb;
    private CharacterController CC;
    // Start is called before the first frame update
    void Start()
    {
        //FRb = Fighter.GetComponent<Rigidbody>();
        CC = Fighter.GetComponent<CharacterController>();
        shotInterval = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(CC.transform.forward);
        if (shotInterval <= 0)
        {
            GameObject bullet = (GameObject)Instantiate(Bullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(bullet.transform.forward * shotSpeed);
            bulletRb.velocity = bulletRb.velocity + CC.velocity;
            Destroy(bullet, 8.0f);
            shotInterval = Interval;
        }
        shotInterval -= 0.1f * 60 * Time.deltaTime;
    }
}
