using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    enum PlayerState 
    {
        AliveIdol,
        AliveMove,
        AliveAttack,
        Die
    }
    PlayerState playerState = PlayerState.AliveAttack;
    [SerializeField] private float playerHp = 100;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float playerjumpPower = 2.0f;
    [SerializeField] private float onGroundDistans = 5.0f;
    [SerializeField] private Transform shotPointTransform;
    [SerializeField] GameObject bulletPrefab1;
    [SerializeField] GameObject bulletPrefab2;
    [SerializeField] GameObject bulletPrefab3;
    [SerializeField] Image playerHpBar;
    [SerializeField] Animation defeatAnimasion;
    Rigidbody playerRigidBody;
    Animator playerAnimator;
    GameObject selectBullet;
    int selectBulletNum = 1;
    GameObject beam;//生成したbeamを
    bool run = false;
    bool attackNow = false;

    Transform enemyTransform;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        if (GameObject.Find("BulletCont").GetComponent<BulletContllor>().GetBullet() == 3)
        {
            selectBullet = bulletPrefab3;
            selectBulletNum = 3;
        }
        else if (GameObject.Find("BulletCont").GetComponent<BulletContllor>().GetBullet() == 2)
        {
            selectBullet = bulletPrefab2;
            selectBulletNum = 2;
        }
        else
        {
            selectBullet = bulletPrefab1;
            selectBulletNum = 1;
        } 
    }

    private void Start()
    {
        enemyTransform = GameObject.FindGameObjectWithTag("enemy").transform;
    }

    private void Update()
    {
        switch (playerState)
        {
            case PlayerState.AliveIdol:
                playerState = PlayerState.AliveMove;
                break;
            case PlayerState.AliveMove:
                if (selectBulletNum == 1 || selectBulletNum == 2)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        transform.LookAt(enemyTransform);
                        playerAnimator.Play("Attack1");
                        playerState = PlayerState.AliveAttack;
                        break;
                    }
                }
                else if (selectBulletNum == 3)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        beam = ShotGun2();
                        playerState = PlayerState.AliveAttack;
                        break;
                    }
                }

                if (IsGrounded())
                {
                    float vertical = Input.GetAxisRaw("Vertical");
                    float horizon = Input.GetAxisRaw("Horizontal");
                    if (Input.GetKey("left shift"))
                    {
                        run = true;
                    }
                    else if (Input.GetKeyUp("left shift"))
                    {
                        run = false;
                    }
                    Vector3 ve3 = Vector3.forward * vertical + Vector3.right * horizon;
                    if (!attackNow)
                    {
                        if (ve3 == Vector3.zero)
                        {
                            playerRigidBody.velocity = new Vector3(0f, playerRigidBody.velocity.y, 0f);
                            playerAnimator.SetFloat("Speed", 0);
                        }
                        else
                        {
                            ve3 = Camera.main.transform.TransformDirection(ve3);
                            ve3.y = 0;
                            this.transform.forward = ve3;

                            if (run)
                            {
                                Vector3 vertical2 = this.transform.forward * playerSpeed * 2;
                                vertical2.y = 0;
                                playerRigidBody.velocity = vertical2;
                                playerAnimator.SetFloat("Speed", vertical2.sqrMagnitude);
                            }
                            else
                            {
                                Vector3 vertical2 = this.transform.forward * playerSpeed;
                                vertical2.y = 0;
                                playerRigidBody.velocity = vertical2;
                                playerAnimator.SetFloat("Speed", vertical2.sqrMagnitude);
                            }
                        }
                    }


                    if (IsGrounded())
                    {
                        playerAnimator.SetBool("Jump", true);
                    }
                    else if (!IsGrounded())
                    {
                        playerAnimator.SetBool("Jump", false);
                    }

                    if (Input.GetKeyDown("space") && IsGrounded())
                    {
                        playerRigidBody.AddForce(Vector3.up * playerjumpPower); ;
                        playerAnimator.SetBool("Jump", false);
                    }
                }
                break;
            case PlayerState.AliveAttack:
                if (selectBulletNum == 1 || selectBulletNum == 2)
                {
                    AttackEnd("Attack1");
                }
                else if (selectBulletNum == 3)
                {
                    transform.LookAt(enemyTransform);
                    playerAnimator.Play("Attack2");
                    if (Input.GetButtonUp("Fire1"))
                    {
                        Destroy(beam);
                        playerState = PlayerState.AliveIdol;
                    }
                }
                break;
            case PlayerState.Die:
                break;
            default:
                break;
        }
    }

    bool IsGrounded()
    {
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;   // start: 体の中心
        Vector3 end = start + Vector3.down * (col.center.y + col.height / 2 + onGroundDistans);  // end: start から真下の地点
        Debug.DrawLine(start, end); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
        return isGrounded;
    }

    public void ShotGun()
    {
        Instantiate(selectBullet, shotPointTransform.position, Quaternion.identity);
    }

    public GameObject ShotGun2()
    {
        GameObject a = Instantiate(selectBullet, shotPointTransform.position, Quaternion.identity);
        return a;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            playerHp -= 10;
            playerHpBar.fillAmount = playerHp / 100;
            playerRigidBody.AddForce(Vector3.up * 1000.0f);
            playerAnimator.SetTrigger("Knock");

            Debug.Log("Player" + playerHp);
            if (playerHp <= 0)
            {
                playerAnimator.Play("Die");
                playerState = PlayerState.Die;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            playerHp -= other.gameObject.GetComponent<EnemyDamage>().EP();
            playerHpBar.fillAmount = playerHp / 100;
            playerRigidBody.AddForce(Vector3.up * 1000.0f);
            playerAnimator.SetTrigger("Knock");
            
            if (playerHp <= 0)
            {
                defeatAnimasion.Play();
                playerAnimator.Play("Die");
                enemyTransform.gameObject.GetComponent<Enemy1>().Win();
                playerState = PlayerState.Die;
            }
        }
    }

    void AttackEnd(string s)
    {
        float animeInfo = playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (animeInfo < 1.0f){}
        else
        {
            playerState = PlayerState.AliveIdol;
        }
    }
}
