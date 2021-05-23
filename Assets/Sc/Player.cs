using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip gunAudio;
    [SerializeField] private AudioClip xxx;
    private AudioSource audioSource;
    enum EnumState 
    {
        AliveMove,
        AliveAttack,
        Die
    }
    EnumState ens;
    [SerializeField] float HP = 100;
    [SerializeField] float speed = 2.0f;
    [SerializeField] float jump = 2.0f;
    [SerializeField] float kinn = 5.0f;
    [SerializeField] float groundedLength = 1f;
    Rigidbody p_rigidbody;
    Animator p_animator;
    bool ran = false;
    [SerializeField] GameObject shotPoint;
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bullet2;
    [SerializeField] GameObject bullet3;
    GameObject bulletins;
    int buco;
    GameObject beem;
    GameObject enemy;
    bool attackNow = false;
    bool m_at = false;
    [SerializeField] Image HPBR;
    [SerializeField] Animation defeatAnimation;
    
    void Start()
    {
        p_rigidbody = GetComponent<Rigidbody>();
        p_animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("enemy");
        ens = EnumState.AliveMove;
        audioSource = GetComponent<AudioSource>();
        buco = 2;
        bulletins = bullet2;
        if (GameObject.Find("BulletCont").GetComponent<BulletContllor>().GetBullet() == 3)
        {
            bulletins = bullet3;
            buco = 3;
        }
        else if (GameObject.Find("BulletCont").GetComponent<BulletContllor>().GetBullet() == 2)
        {
            bulletins = bullet2;
            buco = 2;
        }
        else
        {
            bulletins = bullet1;
            buco = 1;
        }
        Debug.Log(buco);
    }

    void Update()
    {
        switch (ens)
        {
            case EnumState.AliveMove:
                if (!m_at)
                {
                    if (p_animator)
                    {
                        if (buco == 1 || buco == 2)
                        {
                            if (Input.GetButton("Fire1"))
                            {
                                transform.LookAt(enemy.transform);
                                p_animator.Play("Attack1");
                                ens = EnumState.AliveAttack;
                                audioSource.PlayOneShot(gunAudio);
                                break;
                            }
                        }
                        else if (buco == 3)
                        {
                            if (Input.GetButtonDown("Fire1"))
                            {
                                beem = ShotGun2();
                                ens = EnumState.AliveAttack;
                                break;
                            }
                        }
                    }

                    if (IsGrounded())
                    {
                        float vertical = Input.GetAxisRaw("Vertical");
                        float horizon = Input.GetAxisRaw("Horizontal");
                        if (Input.GetKey("left shift"))
                        {
                            ran = true;
                        }
                        else if (Input.GetKeyUp("left shift"))
                        {
                            ran = false;
                        }
                        Vector3 ve3 = Vector3.forward * vertical + Vector3.right * horizon;
                        if (!attackNow)
                        {
                            if (ve3 == Vector3.zero)
                            {
                                p_rigidbody.velocity = new Vector3(0f, p_rigidbody.velocity.y, 0f);
                                p_animator.SetFloat("Speed", 0);
                            }
                            else
                            {
                                ve3 = Camera.main.transform.TransformDirection(ve3);
                                ve3.y = 0;
                                this.transform.forward = ve3;

                                if (ran)
                                {
                                    Vector3 vertical2 = this.transform.forward * speed * 2;
                                    vertical2.y = 0;
                                    p_rigidbody.velocity = vertical2;
                                    p_animator.SetFloat("Speed", vertical2.sqrMagnitude);
                                }
                                else
                                {
                                    Vector3 vertical2 = this.transform.forward * speed;
                                    vertical2.y = 0;
                                    p_rigidbody.velocity = vertical2;
                                    p_animator.SetFloat("Speed", vertical2.sqrMagnitude);
                                }
                            }
                        }


                        if (IsGrounded())
                        {
                            p_animator.SetBool("Jump", true);
                        }
                        else if (!IsGrounded())
                        {
                            p_animator.SetBool("Jump", false);
                        }

                        if (Input.GetKeyDown("space") && IsGrounded())
                        {
                            p_rigidbody.AddForce(Vector3.up * jump); ;
                            p_animator.SetBool("Jump", false);
                        }
                    }
                }
                break;
            case EnumState.AliveAttack:
                if (buco ==1 || buco == 2)
                {
                    AttackEnd("Attack1");
                }
                else if (buco == 3)
                {
                    transform.LookAt(enemy.transform);
                    p_animator.Play("Attack2");
                    if (Input.GetButtonUp("Fire1"))
                    {
                        Destroy(beem);
                        ens = EnumState.AliveMove;
                    }
                }
                break;
            case EnumState.Die:
                break;
            default:
                break;
        }
        
    }

    void AttackEnd(string s) 
    {
        float animeInfo = p_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animeInfo < 1.0f)
        {

        }
        else
        {
            ens = EnumState.AliveMove;
        }
    }

    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: 体の中心
        Vector3 end = start + Vector3.down * (col.center.y + col.height / 2 + kinn);  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }

    public void ShotGun() 
    {
        Instantiate(bulletins, shotPoint.transform.position, Quaternion.identity);
    }

    public GameObject ShotGun2()
    {
        GameObject a = Instantiate(bulletins, shotPoint.transform.position, Quaternion.identity);
        return a;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            HP -= 10;
            HPBR.fillAmount = HP / 100;
            p_rigidbody.AddForce(Vector3.up * 1000.0f);
            p_animator.SetTrigger("Knock");
            
            Debug.Log("Player"+HP);
            if (HP <= 0)
            {
                p_animator.Play("Die");
                ens = EnumState.Die;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Damageoccurred1(other.gameObject.GetComponent<EnemyDamage>().EP());
            
            if (HP <= 0)
            {
                defeatAnimation.Play();
                p_animator.Play("Die");
                enemy.GetComponent<Enemy1>().Win();
                ens = EnumState.Die;
            }
        }
    }

    public void Damageoccurred(float a) 
    {
        HP -= a;
        HPBR.fillAmount = HP / 100;
        if (HP <= 0)
        {
            p_animator.Play("Die");
            enemy.GetComponent<Enemy1>().Win();
            ens = EnumState.Die;
        }
    }

    public void Damageoccurred1(float a)
    {
        HP -= a;
        HPBR.fillAmount = HP / 100;
        p_rigidbody.AddForce(Vector3.up * 1000.0f);
        p_animator.SetTrigger("Knock");
    }
}
