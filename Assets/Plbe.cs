using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plbe : MonoBehaviour
{
    [SerializeField]private float po = 1;
    float nowShot = 0;
    GameObject e_object;
    Enemy1 enemy1;
    
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        e_object = GameObject.FindGameObjectWithTag("enemy");
        enemy1 = e_object.GetComponent<Enemy1>();
        GameObject gameObject = GameObject.FindGameObjectWithTag("core");
        this.transform.LookAt(gameObject.transform);
    }

    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("hit");
            enemy1.Cretedamege(po);
        }
        else if(other.gameObject.tag == "Bu")
        {
            Billding billding = other.gameObject.GetComponent<Billding>();
            billding.X();
        }
    }
}
