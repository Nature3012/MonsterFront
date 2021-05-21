using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider1;
    [SerializeField] BoxCollider boxCollider2;
    [SerializeField] BoxCollider boxCollider3;

    public void TriggerON2()
    {
        boxCollider1.enabled = true;
    }

    public void TrrigerOff2() { boxCollider1.enabled = false; }

    public void TriggerON3()
    {
        boxCollider2.enabled = true;
    }

    public void TrrigerOff3() { boxCollider2.enabled = false; }

    public void TriggerON4()
    {
        boxCollider3.enabled = true;
        
    }

    public void TrrigerOff4() { boxCollider3.enabled = false; }
}
