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
    public NavMeshAgent navMeshAgent { private set; get; }//
    private bool isSet = false; //Setしたかどうか 
    private float currentSeconds = 0;//Freeze時開始からの秒数
    private float attackE;

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
        FREEZE,
        Die
    }
    ENEMYSTATE enemyState = ENEMYSTATE.IDOL;

    public void SetEnemy(string name, float hp, float speed, 
        float power, float freeze, Animator animator, Rigidbody rigidbody, Transform enemyTrans, Transform targetTrans,NavMeshAgent navMesh)
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
        navMeshAgent = navMesh;
        isSet = true;
    }

    private void Update()
    {
        if (isSet)
        {
            switch (enemyState)
            {
                case ENEMYSTATE.IDOL:
                    enemyState = ENEMYSTATE.LOOKTARGET;
                    break;
                case ENEMYSTATE.LOOKTARGET:
                    Vector3 playerPos = targetTransform.position;
                    playerPos.y = this.transform.position.y;
                    this.transform.LookAt(playerPos);
                    enemyState = ENEMYSTATE.ATTACK;
                    break;
                case ENEMYSTATE.ATTACK:
                    float aida = (enemyTransform.position - targetTransform.position).sqrMagnitude;
                    float attckdis = (targetTransform.gameObject.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE) + 
                        (targetTransform.gameObject.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE);
                    int attackCase = RandomAttack();
                    switch (attackCase)
                    {
                        case 0:
                            Attack1();
                            break;
                        case 1:
                            Attack2(aida,attckdis);
                            break;
                        case 2:
                            Attack3(aida,attckdis);
                            break;
                        case 3:
                            Attack4(aida,attckdis);
                            break;
                        case 4:
                            Attack5(aida,attckdis);
                            break;
                        default:
                            break;
                    }
                    break;
                case ENEMYSTATE.FREEZE:
                    Freeze();
                    break;
                case ENEMYSTATE.Die:
                    Die();
                    break;
                default:
                    break;
            }
        }
    }

    private int RandomAttack() 
    {
        int attack = Random.Range(0,5);
        return attack;
    }

    public virtual void Attack1() 
    {
        enemyState = ENEMYSTATE.FREEZE;
    }

    public virtual void Attack2(float aida,float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
    }

    public virtual void Attack3(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
    }

    public virtual void Attack4(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
    }

    public virtual void Attack5(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
    }

    private void Freeze() 
    {
        currentSeconds += Time.deltaTime;
        if (enemyFreeze <= currentSeconds)
        {
            enemyState = ENEMYSTATE.IDOL;
            currentSeconds = 0;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            enemyHp -= collision.gameObject.GetComponent<Bullet>().Po();
            if (enemyHp <= 0)
            {
                enemyState = ENEMYSTATE.Die;
            }
        }
    }

    public void BeamDameze(float dameze) 
    {
        enemyHp -= dameze;
        if (enemyHp <= 0)
        {
            enemyState = ENEMYSTATE.Die;
        }
    }

    public void Die() 
    {
        this.tag = "Untagged";
    }

    public void AnimatorEnd(string s)
    {
        float animInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animInfo < 1.0f) { }
        else
        {
            enemyState = ENEMYSTATE.FREEZE;
        }
    }
}
