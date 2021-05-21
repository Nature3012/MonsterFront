using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billding : MonoBehaviour
{
    [SerializeField] GameObject particleSystems;
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        //ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Instantiate(particleSystems, this.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        //ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Instantiate(particleSystems, this.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void X() 
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        //ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Instantiate(particleSystems, this.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
