using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour, IEnemyPattern
{
    float time1 = 10f;
    float time2 = 0;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] Text text;
    private AudioSource audioSource;
    enum EnemyStatus 
    {
        Walk,
        Attack,
        Freeze,
        Die,
        DieAfter
    }
    EnemyStatus enemystatus;
    EnemyBace enemyBace;
    Transform target;
    GameObject targetj;
    Rigidbody enemyrigidbody;
    Animator enemyAnimator;
    [SerializeField] private float hp;
    [SerializeField] private float attack;
    [SerializeField] private float defence;
    [SerializeField] private float speed;
    [SerializeField] private float frez;
    [SerializeField] GameObject attacksp;
    [SerializeField] GameObject A1;
    private float currentSeconds;
    bool a1s = false;
    Transform m_tra;
    float attackE;
    float atp;
    int ap;

    void Start()
    {
        enemyBace = new EnemyBace("α-幼体", 3, GetComponent<NavMeshAgent>() ,hp, attack, defence, speed);
        targetj = GameObject.FindGameObjectWithTag("Player");
        target = targetj.transform;
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyBace.returnAgent().speed = speed;
        m_tra = this.transform;
        atp = enemyBace.RandomAttack();
        ap = enemyBace.AttackNum(atp);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (enemystatus)
        {
            case EnemyStatus.Walk:
                Vector3 playerPos = target.position;
                playerPos.y = this.transform.position.y;
                this.transform.LookAt(playerPos);
                enemystatus = EnemyStatus.Attack;
                break;
            case EnemyStatus.Attack:
                target = targetj.transform;
                float aida = (m_tra.position - target.position).sqrMagnitude;//己と敵との間
                float attckdis = (targetj.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE) + (targetj.GetComponent<CapsuleCollider>().radius + this.GetComponent<CapsuleCollider>().radius + attackE);
                
                if (ap == 1)
                {
                    Attack1(ap);
                }
                else if (ap == 2)
                {
                    Attack2(aida, attckdis);
                }
                else if (ap == 3)
                {
                    Attack3(aida, attckdis);
                }
                else if (ap == 4)
                {
                    Attack4(aida, attckdis);
                }
                else if (ap == 5)
                {
                }
                break;
            case EnemyStatus.Freeze:
                Frezze();
                break;
            case EnemyStatus.Die:
                enemyAnimator.Play("Die");
                Die("Die");
                break;
            case EnemyStatus.DieAfter:
                time2 += Time.deltaTime;
                if (time2 > time1)
                {
                    SceneManager.LoadScene("Main menu");
                }
                break;
            default:
                break;
        }
    }

    public void Attack1(int a) 
    {
        if (!a1s)
        {
            enemyAnimator.Play("Attack1");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.5f);
            Instantiate(A1, attacksp.transform.position, Quaternion.identity);
            a1s = true;
            audioSource.PlayOneShot(audioClip);
            enemystatus = EnemyStatus.Freeze;
        }
    }

    public void Attack2(float a, float b) 
    {
        if (b >= a/400)
        {
            enemyBace.returnAgent().velocity = Vector3.zero;
            audioSource.PlayOneShot(audioClip);
            enemyAnimator.Play("Attack2");
            AnimatorEnd("Attack2");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            enemyBace.returnAgent().SetDestination(target.position);
            enemyAnimator.SetFloat("Speed", enemyBace.returnAgent().velocity.magnitude);
        }
    }

    public void Attack3(float a, float b) 
    {
        if (b >= a / 400)
        {
            enemyBace.returnAgent().velocity = Vector3.zero;
            audioSource.PlayOneShot(audioClip);
            enemyAnimator.Play("Attack3");
            AnimatorEnd("Attack3");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            enemyBace.returnAgent().SetDestination(target.position);
            enemyAnimator.SetFloat("Speed", enemyBace.returnAgent().velocity.magnitude);
        }
        
        
    }

    public void Attack4(float a, float b) 
    {
        if (b >= a / 400)
        {
            enemyBace.returnAgent().velocity = Vector3.zero;
            audioSource.PlayOneShot(audioClip);
            enemyAnimator.Play("Attack4");
            AnimatorEnd("Attack4");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            enemyBace.returnAgent().SetDestination(target.position);
            enemyAnimator.SetFloat("Speed", enemyBace.returnAgent().velocity.magnitude);

        }

    }

    public void Frezze() 
    {
        currentSeconds += Time.deltaTime;
        if (currentSeconds > frez)
        {
            enemystatus = EnemyStatus.Walk;
            atp = enemyBace.RandomAttack();
            ap = enemyBace.AttackNum(atp);
            Debug.Log("WALK移行");

            currentSeconds = 0;
        }
    }

    public void AnimatorEnd(string s) 
    {
        float animInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animInfo < 1.0f) { }
        else
        {
            enemystatus = EnemyStatus.Freeze;
        }
    }

    public void Attack1End() 
    {
        enemystatus = EnemyStatus.Freeze;
        a1s = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            hp -= collision.gameObject.GetComponent<Bullet>().Po();
            Debug.Log(hp);
            text.text = hp.ToString();
            if (hp <= 0)
            {
                enemystatus = EnemyStatus.Die;
            }
        }
    }

    public void Cretedamege(float a) 
    {
        hp -= a;
        Debug.Log(hp);
        text.text = hp.ToString();
        if (hp <= 0)
        {
            enemystatus = EnemyStatus.Die;
        }
    }

    public void Die(string stateName) 
    {
        float animInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animInfo < 1.0f) { }
        else
        {
            this.tag = "Untagged";
            
            enemystatus = EnemyStatus.DieAfter;
        }
    }

    public void Win() 
    {
        enemystatus = EnemyStatus.DieAfter;
    }
}
