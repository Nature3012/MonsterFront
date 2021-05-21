﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //GameObject maincamera;
    //GameObject player;
    //GameObject target;
    //void Start()
    //{
    //    maincamera = Camera.main.gameObject;
    //    player = GameObject.FindGameObjectWithTag("Player");
    //    target = GameObject.FindGameObjectWithTag("core");
    //}
//// Update is called once per frame
    //void Update()
    //{
    //    transform.position = player.transform.position;

    //    if (target)
    //    {
    //        TargetEnemy(target);
    //    }
    //}

    //private void TargetEnemy(GameObject targetenemy) 
    //{
    //    transform.LookAt(targetenemy.transform, Vector3.up);
    //}
    [SerializeField]
    Transform targetTrans = null;       //< ターゲットのTransform

    [SerializeField]
    float distanceToTarget = 5.0f;      //< ターゲットからの距離

    [SerializeField]
    float heightToTarget = 3.0f;        //< ターゲットからの高さ

    [SerializeField]
    Vector3 lookAtOffset = new Vector3(0.0f, 2.0f, 0.0f);       //< みる位置のオフセット

    [SerializeField]
    float rotateSpeed = 5.0f;       //< 回転速度

    RaycastHit raycastHit = new RaycastHit();

    // Raycastでヒットして欲しいレイヤーマスク
    int raycastHitLayerMask = 0;

    private void Start()
    {
        raycastHitLayerMask = LayerMask.GetMask("Map");

        transform.localPosition = targetTrans.localPosition - (targetTrans.forward * distanceToTarget);
        transform.localPosition += Vector3.up * heightToTarget;
        transform.LookAt(targetTrans.localPosition + lookAtOffset);
    }

    /// <summary>
    /// ターゲットが動いた後に処理したいので、LateUpdateでやっている
    /// </summary>
    private void LateUpdate()
    {
        Vector3 lookTargetPos = targetTrans.localPosition + lookAtOffset;

        // 移動処理
        {
            // ターゲット座標 - (カメラの前方向 * 距離)
            Vector3 cameraPos = lookTargetPos - (transform.forward * distanceToTarget);

            // 壁や床にめり込ませないようにする処理
            {
                Vector3 targetDir = (transform.localPosition - lookTargetPos).normalized;
                float targetDist = distanceToTarget + 0.5f; //< 少し奥までRayを飛ばす

                // デバッグ表示（シーンビューで確認できる）
                Debug.DrawRay(lookTargetPos, targetDir * targetDist, Color.red);

                // Raycast
                bool isHit = Physics.Raycast(lookTargetPos, targetDir, out raycastHit, targetDist, raycastHitLayerMask);
                if (isHit)
                {
                    // 当たった座標にカメラ座標を上書きする。
                    cameraPos = raycastHit.point;
                }
            }

            transform.localPosition = cameraPos;
        }

        // プレイヤーの周りを回転する処理
        {
            Vector3 vector3 = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);
            transform.RotateAround(lookTargetPos, Vector3.up, vector3.x);

            if (transform.forward.y > 0.8 && vector3.y < 0)
            {
                vector3.y = 0;
            }

            if (transform.forward.y < -0.8 && vector3.y > 0)
            {
                vector3.y = 0;
            }

            float rotX = vector3.x * Time.deltaTime * rotateSpeed;
            float rotY = vector3.y * Time.deltaTime * rotateSpeed;

            // 回転（横）
            transform.RotateAround(lookTargetPos, Vector3.up, rotX);

            // カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
            if (transform.forward.y > 0.9f && rotY < 0)
            {
                rotY = 0;
            }
            if (transform.forward.y < -0.9f && rotY > 0)
            {
                rotY = 0;
            }
            // 回転（縦）
            transform.RotateAround(lookTargetPos, transform.right, rotY);
        }

        // LookAt
        transform.LookAt(lookTargetPos);
    }
}
