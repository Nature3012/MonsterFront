using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaceEnemy : MonoBehaviour
{
    private string enemyName;//敵の名前
    private float enemyHp;//敵のHP
    private float enemySpeed;//敵の移動速度
    private float enemyAttackPow;//敵の攻撃力
    private float enemyFreeze;//敵の硬直時間
    private Rigidbody enemyRigidBody;//敵のRigidBody
    private Transform enemyTransform;//敵のTransform
    private Transform targetTransform;//プレイヤーのTransform
    private Animator enemyAnimator;//敵のアニメーター

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

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
