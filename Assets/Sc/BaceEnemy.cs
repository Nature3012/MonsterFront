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
    private bool isAttack = false;//攻撃が選択されてる状態
    int attackCase = 0;//次の攻撃
    private float attackE;

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
        float power, float freeze, Animator animator, Rigidbody rigidbody,
        Transform enemyTrans ,NavMeshAgent navMesh)
    {
        enemyName = name;
        enemyHp = hp;
        enemySpeed = speed;
        enemyAttackPow = power;
        enemyFreeze = freeze;
        enemyRigidBody = rigidbody;
        enemyTransform = enemyTrans;
        enemyAnimator = animator;
        navMeshAgent = navMesh;
    }

    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        isSet = true;
        Debug.Log("isSet:true");
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
                    if (!isAttack)
                    {
                        attackCase = RandomAttack();
                        Debug.Log("attackCase:" + attackCase);
                        isAttack = true;

                    }
                    
                    float aida = (enemyTransform.position - targetTransform.position).sqrMagnitude;
                    float attckdis = (targetTransform.gameObject.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE) +
                        (targetTransform.gameObject.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE);
                    
                    if (isAttack)
                    {
                        switch (attackCase)
                        {
                            case 0:
                                Attack1();
                                break;
                            case 1:
                                Attack2(aida, attckdis);
                                break;
                            case 2:
                                Attack3(aida, attckdis);
                                break;
                            case 3:
                                Attack4(aida, attckdis);
                                break;
                            case 4:
                                Attack5(aida, attckdis);
                                break;
                            default:
                                break;
                        }
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
        isAttack = false;
    }

    public virtual void Attack3(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
        isAttack = false;
    }

    public virtual void Attack4(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
        isAttack = false;
    }

    public virtual void Attack5(float aida, float attackdis) 
    {
        enemyState = ENEMYSTATE.FREEZE;
        isAttack = false;
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

    public void Attack1End() 
    {
        enemyState = ENEMYSTATE.FREEZE;
        isAttack = false;
    }
}
