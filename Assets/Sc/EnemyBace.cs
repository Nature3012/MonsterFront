using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBace : Enemystatus
{
    private string enemyName;
    private int enemyRank;
    NavMeshAgent meshAgent;

    public EnemyBace(string enemyName, int Rank, NavMeshAgent nav, float h, float a, float d, float m) : base(h, a, d, m)
    {
        this.enemyName = enemyName;
        this.enemyRank = Rank;
        meshAgent = nav;
    }

    public string Name()
    {
        return enemyName;
    }

    public override void Yuumei(string sitteruka = "知ってるか？")
    {
        base.Yuumei(sitteruka);
    }

    public override void End()
    {

    }

    public float RandomAttack()
    {
        return Random.Range(0.0f, 5.0f);
    }

    public NavMeshAgent returnAgent()
    {
        return meshAgent;
    }

    public int AttackNum(float a) 
    {
        int x = 1;
        if (0 <= a && a < 1.0f)
        {
            x = 1;
        }
        else if (1.0 <= a && a < 2.0f)
        {
            x = 2;
        }
        else if (2.0f <= a && a < 3.0f)
        {
            x = 3;
        }
        else if (3.0f <= a && a < 4.0f)
        {
            x = 4;
        }
        else if (4.0f <= a && a <= 5.0f)
        {
            x = 5;
        }
        return x;
    }
}
