using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemystatus
{
    private float enemyLife;
    private float enemyAttackPower;
    private float enemyDefencivePower;
    private float enemyMovingSpeed;

    public Enemystatus(float enemyLife, float enemyAttackPower, float enemyDefencivePower, float enemyMovingSpeed) 
    {
        this.enemyLife = enemyLife;
        this.enemyAttackPower = enemyAttackPower;
        this.enemyDefencivePower = enemyDefencivePower;
        this.enemyMovingSpeed = enemyMovingSpeed;
    }

    public float EnemyLife() 
    {
        return enemyLife;
    }

    public float EnemyAttackPower() 
    {
        return enemyAttackPower;
    }

    public float EnemyDefencivePower() 
    {
        return enemyDefencivePower;
    }

    public float EnemyMovingSpeed() 
    {
        return enemyMovingSpeed;
    }

    public virtual void Yuumei(string sitteruka) 
    {
        Debug.Log(sitteruka);
    }

    public abstract void End();
}
