using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public GameObject Bullet;
    private GameObject Target;
    public Vector3 aimDirection;

    //Stats
    public float shotSpeed = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Aim towards target
        //Temporary solution to finding target (Search EnemySpawnController's list for gameobjects later)
        Target = GameObject.FindWithTag("Enemy");
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        aimDirection = new Vector3(0, 0, angle - 90);
        transform.eulerAngles = aimDirection;
    }

    private void Update()
    {
        //Space is a placeholder
        if (Input.GetKeyDown(KeyCode.Space))
        {
           GameObject bul = Instantiate(Bullet,transform.position,Quaternion.identity);
           bul.GetComponent<BulletScript>().SpawnBullet(aimDirection, shotSpeed);
        }

        if (Input.GetKey(KeyCode.X))
        {
            GameObject bul = Instantiate(Bullet, transform.position, Quaternion.identity);
            bul.GetComponent<BulletScript>().SpawnBullet(aimDirection, shotSpeed * 2.5f);
        }
    }
}
