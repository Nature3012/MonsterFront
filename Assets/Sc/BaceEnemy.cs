using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaceEnemy : MonoBehaviour
{
    public string enemyName { private set; get; }//敵の名前
    public float enemyHp { private set; get; }//敵のHP
    public float enemySpeed { private set; get; }//敵の移動速度
    public float enemyAttackPow { private set; get; }//敵の攻撃力
    public float enemyFreeze { private set; get; }//敵の硬直時間
    public Rigidbody enemyRigidBody { private set; get; }//敵のRigidBody
    public Transform enemyTransform { private set; get; }//敵のTransform
    public Transform targetTransform { private set; get; }//プレイヤーのTransform
    public Animator enemyAnimator { private set; get; }//敵のアニメーター

    public virtual void SetEnemyName(string s) {enemyName = s;}
    public virtual string GetEnemyName() {return enemyName;}

    public virtual void SetEnemyHp(float o) { enemyHp = o; }
    public virtual float GetEnemyHp() { return enemyHp; }

    public virtual void SetEnemySpeed(float o) { enemySpeed = o; }
    public virtual float GetEnemySpeed() { return enemySpeed; }

    public virtual void SetEnemyAttack(float o) { enemyAttackPow = o; }
    public virtual float GetEnemyAttack() { return enemyAttackPow; }

    public virtual void SetEnemyFreeze(float o) { enemyFreeze = o; }
    public virtual float GetEnemyFreeze() { return enemyFreeze; }

    public virtual void SetEnemyAnime(Animator a) { enemyAnimator = a; }
    public virtual Animator GetEnemyAnime() { return enemyAnimator; }

    enum ENEMYSTATE 
    {
        IDOL,
        LOOKTARGET,
        ATTACK,
        FREEZE
    }

    public virtual void SetEnemy(string name, float hp, float speed, 
        float power, float freeze, Animator animator, Rigidbody rigidbody, Transform enemyTrans, Transform targetTrans)
    {
        enemyName = name;
        enemyHp = hp;
        enemySpeed = speed;
        enemyAttackPow = power;
        enemyFreeze = freeze;
        enemyRigidBody = rigidbody;
        enemyTransform = enemyTransform;
        targetTransform = targetTrans;
        enemyAnimator = animator;
    }

    private void Update()
    {
        
    }
}
