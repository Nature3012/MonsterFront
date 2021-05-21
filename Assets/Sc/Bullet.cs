using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private GameObject explosion;
    Rigidbody b_rigidbody;
    [SerializeField]float pw;

    private void Start()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("core");
        b_rigidbody = GetComponent<Rigidbody>();
        Vector3 dir = enemy.transform.position - this.transform.position;
        dir = dir.normalized;

        // プレイヤーに向かって飛ばす
        b_rigidbody.velocity = dir * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public float Po() 
    {
        return pw;
    }
}
