using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera maincamera;
    private RaycastHit raycastHit;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Ray ray;
    Vector3 vh;

    void Start()
    {
        maincamera = Camera.main;   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            vh = Input.mousePosition;
            ray = maincamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))  //マウスのポジションからRayを投げて何かに当たったらhitに入れる
            {
                Debug.Log("撃ってる");
            }

            GameObject gameObject = Instantiate(bullet, gun.position, Quaternion.identity);
            gameObject.transform.parent = transform;


            //GameObject createdBullet = Instantiate(bullet) as GameObject;
            //createdBullet.transform.position = gun.position;

            //float speed = 3f;

            //Vector3 force;
            //force = gun.forward * speed;
            //createdBullet.GetComponent<Rigidbody>().AddForce(force);
        }
    }

    public Vector3 VH() 
    {
        return raycastHit.point;
    }

    public Vector3 ThisPos() 
    {
        return this.gameObject.transform.position;
    }
}