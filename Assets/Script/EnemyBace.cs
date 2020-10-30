using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBace : MonoBehaviour
{
    [SerializeField] public string enemyName { get; private set; } = "";//敵の名前
    [SerializeField] public float enemyHp { get; private set; } = 100f;//敵のHP
    [SerializeField] public float enemySpeed { get; private set; } = 10f;//敵の移動速度
    [SerializeField] public float enemyAttackPow { get; private set; } = 30f;//敵の攻撃力
    [SerializeField] public float enemyFreeze { get; private set; } = 10f;//敵の硬直時間
    [SerializeField] public float enemyAttackDistans { get; private set; } = 15f;//敵の攻撃の遠近判定
    public Rigidbody enemyRigidBody { get; private set; } = null;//敵のRigidBody
    public Transform enemyTransform { get; private set; } = null;//敵のTransform
    public Transform targetTransform { get; private set; } = null;//プレイヤーのTransform
    public Animator enemyAnimator { get; private set; } = null;//敵のアニメーター
    public AudioClip audioClip { get; private set; } = null;//敵のAudioClip
    private float currentSeconds = 0;//敵のフリーズ経過時間のための変数
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
        enemyRigidBody = GetComponent<Rigidbody>();
        enemyTransform = transform;
        audioClip = GetComponent<AudioClip>();
        if (GetComponent<Animator>())
        {
            enemyAnimator = GetComponent<Animator>();
        }
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
                break;
            case EnemyState.Attack:
                AttackStart();
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

        }
        else
        {

        }
    }

    public virtual void Attack1() { }

    public virtual void Attack2() { }

    public virtual void Attack3() { }

    public virtual void Attack4() { }

    public virtual void Attack5() { }

    private void FreezeTime() 
    {
        currentSeconds += Time.deltaTime;
        if (currentSeconds > enemyFreeze)
        {
            enemyState = EnemyState.Idle;
            currentSeconds = 0;
        }
    }
}
