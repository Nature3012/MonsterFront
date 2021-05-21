using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContens : BaceEnemy
{

    //敵の被ダメージ関数
    public bool Damege(Collision collision)
    {
        float a = GetEnemyHp();
        a -= collision.gameObject.GetComponent<Bullet>().Po();
        SetEnemyHp(a);
        if (a <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //敵の硬直関数
    private float currentSeconds;
    public bool Freeze()
    {
        currentSeconds += Time.deltaTime;
        if (currentSeconds >= GetEnemyFreeze())
        {
            currentSeconds = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    //敵のアニメーターの終了判定
    public bool AnimationEnd(string s)
    {
        float a = GetEnemyAnime().GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (a < 1.0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override float GetEnemyHp()
    {
        return base.GetEnemyHp();
    }
    public override void SetEnemyHp(float o)
    {
        base.SetEnemyHp(o);
    }

    public override float GetEnemyFreeze()
    {
        return base.GetEnemyFreeze();
    }
    public override void SetEnemyFreeze(float o)
    {
        base.SetEnemyFreeze(o);
    }

    public override Animator GetEnemyAnime()
    {
        return base.GetEnemyAnime();
    }
    public override void SetEnemyAnime(Animator a)
    {
        base.SetEnemyAnime(a);
    }
}
