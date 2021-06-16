using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plbe : MonoBehaviour
{
    [SerializeField]private float power = 1;
    float nowShot = 0;
    GameObject e_object;
    EnemyStage1 enemy1;
    
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        e_object = GameObject.FindGameObjectWithTag("enemy");
        enemy1 = e_object.GetComponent<EnemyStage1>();
        GameObject gameObject = GameObject.FindGameObjectWithTag("core");
        this.transform.LookAt(gameObject.transform);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "enemy")
        {
            enemy1.BeamDameze(power);
        }
        else if(other.gameObject.tag == "Bu")
        {
            Billding billding = other.gameObject.GetComponent<Billding>();
            billding.BrekeBillding();
        }
    }
}
