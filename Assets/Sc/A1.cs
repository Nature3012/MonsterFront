using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1 : MonoBehaviour
{
    [SerializeField] float shottime = 3f;
    float nowShot = 0;
    GameObject e_object;
    Enemy1 enemy1;
    
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        e_object = GameObject.FindGameObjectWithTag("enemy");
        enemy1 = e_object.GetComponent<Enemy1>();
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        this.transform.LookAt(gameObject.transform);
    }

    void Update()
    {
        nowShot += Time.deltaTime;
        if (nowShot >= shottime)
        {
            Destroy(this.gameObject);
            enemy1.Attack1End();
        }
    }
}
