using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStage1 : BaceEnemy
{
    [SerializeField] private string enemyThisName;
    [SerializeField] private float enemyThisHp;
    [SerializeField] private float speed;
    [SerializeField] private float power;
    [SerializeField] private float freeze;
    [SerializeField] private GameObject attack1;
    [SerializeField] private Transform attackSporn;

    private void Awake()
    {
        base.SetEnemy(enemyThisName,enemyThisHp,speed,power,freeze,GetComponent<Animator>(), GetComponent<Rigidbody>(), this.gameObject.transform, 
            GetComponent<NavMeshAgent>());
    }



    public override void Attack1()
    {
        enemyAnimator.Play("Attack1");
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTransform.position - transform.position), 0.5f);
        Instantiate(attack1, attackSporn.position, Quaternion.identity); 
        base.Attack1();
    }

    public override void Attack2(float aida, float attackdis)
    {
        if (attackdis >= aida / 400)
        {
            navMeshAgent.velocity = Vector3.zero;
            enemyAnimator.Play("Attack2");
            AnimatorEnd("Attack2");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            navMeshAgent.SetDestination(targetTransform.position);
            enemyAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
        base.Attack2(aida, attackdis);
    }

    public override void Attack3(float aida, float attackdis)
    {
        if (attackdis >= aida / 400)
        {
            navMeshAgent.velocity = Vector3.zero;
            enemyAnimator.Play("Attack3");
            AnimatorEnd("Attack3");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            navMeshAgent.SetDestination(targetTransform.position);
            enemyAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }
        base.Attack3(aida, attackdis);
    }

    public override void Attack4(float aida, float attackdis)
    {
        if (attackdis >= aida / 400)
        {
            navMeshAgent.velocity = Vector3.zero;
            enemyAnimator.Play("Attack4");
            AnimatorEnd("Attack4");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            navMeshAgent.SetDestination(targetTransform.position);
            enemyAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        }
        base.Attack4(aida, attackdis);
    }

    public override void Attack5(float aida, float attackdis)
    {
        if (attackdis >= aida / 400)
        {
            navMeshAgent.velocity = Vector3.zero;
            enemyAnimator.Play("Attack5");
            AnimatorEnd("Attack5");
            enemyAnimator.SetFloat("Speed", 0);
        }
        else
        {
            navMeshAgent.SetDestination(targetTransform.position);
            enemyAnimator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        }
        base.Attack5(aida, attackdis);
    }
}
