using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float enemyPower = 10;
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damageoccurred(enemyPower);
        }
    }

    public float EP() 
    {
        return enemyPower;
    }
}
