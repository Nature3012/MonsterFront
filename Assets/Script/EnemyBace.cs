using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBace : MonoBehaviour
{
    [SerializeField] public string enemyName { get; private set; } = "";//敵の名前
    [SerializeField] public float enemyHp { get; private set; } = 100f;//敵のHP
    [SerializeField] public float enemySpeed { get; private set; } = 10f;//敵の移動速度
    [SerializeField] public float enemyAttackPow { get; private set; } = 30f;//敵の攻撃力
    [SerializeField] public float enemyFreeze { get; private set; } = 10f;//敵の硬直時間
    [SerializeField] public float enemyLongDistans { get; private set; } = 15f;//敵の攻撃の遠近判定
    [SerializeField] public float enemyAttackDistans { get; private set; } = 5f;//敵の近接範囲
    public Rigidbody enemyRigidBody { get; private set; } = null;//敵のRigidBody
    public Transform enemyTransform { get; private set; } = null;//敵のTransform
    public Transform targetTransform { get; private set; } = null;//プレイヤーのTransform
    public Animator enemyAnimator { get; private set; } = null;//敵のアニメーター
    public AudioClip audioClip { get; private set; } = null;//敵のAudioClip
    public NavMeshAgent enemyNavMeshAgent { get; private set; } = null;//敵のNavMeshAgent
    private float currentSeconds = 0;//敵のフリーズ経過時間のための変数
    private float targetDistans = 0;//標的との距離の二乗
    private float longDistans = 0;//遠近判定の二乗
    private bool afterLongDistance = false;//敵の遠距離後かどうかを確認するためのフラグ

    enum EnemyState
    {
        Idle,
        Attack,
        Freeze,
        Die
    }
    EnemyState enemyState = EnemyState.Idle;

    public virtual void FirstCreation()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRigidBody = GetComponent<Rigidbody>();
        enemyTransform = transform;
        audioClip = GetComponent<AudioClip>();
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        if (GetComponent<Animator>())
        {
            enemyAnimator = GetComponent<Animator>();
        }
        longDistans = enemyLongDistans * enemyLongDistans;
    }

    /// <summary>
    /// 敵は遠近の攻撃をもつ
    /// 基本的には近3遠2
    /// 判定は距離を比較してrandomで実行
    /// 遠距離した後は必ず近距離とする
    /// </summary>
    public virtual void SelfUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                //ここでこいつとPlayerの距離を比較する、遠距離後はこの判定はなしとする。
                if (!afterLongDistance)
                {
                    targetDistans = (targetTransform.position - enemyTransform.position).sqrMagnitude;
                    if (targetDistans >= longDistans)
                    {
                        afterLongDistance = true;
                    }
                }
                enemyState = EnemyState.Attack;
                break;
            case EnemyState.Attack:
                //遠距離ならば即座に攻撃、近距離なら近づいてから攻撃
                if (afterLongDistance)
                {
                    AttackStart();
                }
                else
                {
                    if (targetDistans > enemyAttackDistans)
                    {
                        MoveEnemy();
                    }
                    else
                    {
                        AttackStart();
                    }
                }
                break;
            case EnemyState.Freeze:
                FreezeTime();
                break;
            case EnemyState.Die:
                break;
            default:
                break;
        }
    }

    private void AttackStart()
    {
        if (afterLongDistance)
        {
            //遠距離
            if (0.5f >= RandomAttack(0.0f, 2.0f, 2))
            {
                Attack1();
            }
            else
            {
                Attack2();
            }
        }
        else
        {
            float a = RandomAttack(0.0f, 3.0f, 3);
            //近距離
            if (0.333 >= a)
            {
                Attack3();
            }
            else if (0.333 <= a && 0.666 >= a)
            {
                Attack4();
            }
            else if(0.666 <= a)
            {
                Attack5();
            }

            afterLongDistance = false;
        }

        enemyState = EnemyState.Freeze;
    }

    public virtual void Attack1() { }

    public virtual void Attack2() { }

    public virtual void Attack3() { }

    public virtual void Attack4() { }

    public virtual void Attack5() { }

    public float RandomAttack(float min, float max, float c) 
    {
        float a = Random.Range(min, max);
        a = a / 3;
        return a;
    }

    private void FreezeTime() 
    {
        currentSeconds += Time.deltaTime;
        if (currentSeconds > enemyFreeze)
        {
            enemyState = EnemyState.Idle;
            currentSeconds = 0;
        }
    }

    private void MoveEnemy() 
    {
        enemyNavMeshAgent.SetDestination(targetTransform.position);
        //enemyAnimator.SetFloat("", enemyNavMeshAgent.velocity.magnitude);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            CreateDamege();
        }
    }

    public void CreateDamege() 
    {
        float hp = enemyHp;//ここに弾のダメージ参照と計算
        if (hp <= 0)
        {
            enemyState = EnemyState.Die;
        }
        enemyHp = hp;
    }

    public void CreateDamege(float dmg) 
    {
        float hp = enemyHp - dmg;//ここに弾のダメージ参照と計算
        if (hp <= 0)
        {
            enemyState = EnemyState.Die;
        }
        enemyHp = hp;
    }
}
