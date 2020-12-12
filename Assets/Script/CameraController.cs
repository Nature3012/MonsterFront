using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //// Start is called before the first frame update
    //GameObject maincamera;
    //GameObject player;
    //GameObject target;
    [SerializeField] Transform playerTransform = null;//Playerのtransform
    [SerializeField] float playerDistans = 4.0f;      //Playerとの距離
    [SerializeField] float playerHight = 2.0f;        //Playerからの高さ
    [SerializeField] Vector3 cameraOffset = new Vector3(0, 2.0f, 0); //カメラが見る位置のオフセット
    [SerializeField] float rotatingVelocity = 3.0f; //カメラの回転速度
    private RaycastHit raycastHit = new RaycastHit();
    int hitObjectLayerMask = 0; //Rayが当たった時に反応してほしいオブジェクトのLayerを取っておく
    [SerializeField] Transform enemyCoreTransform = null;//敵の中心部分となるところのTransform

    bool RotationORFixed = true; //trueは回転カメラ、falseは固定カメラ

    private void Start()
    {
        hitObjectLayerMask = LayerMask.GetMask("Building");

        SetUpRotationCamera();
    }

    private void LateUpdate()
    {
        if (RotationORFixed)
        {
            Vector3 lookTargetPos = playerTransform.localPosition + cameraOffset;

            Vector3 cameraPosition = lookTargetPos - (transform.forward * playerDistans);

            Vector3 targetDir = (transform.localPosition - lookTargetPos).normalized;
            float targetDist = playerDistans + 0.5f;
            Debug.DrawRay(lookTargetPos, targetDir * targetDist, Color.red);
            bool isHit = Physics.Raycast(lookTargetPos, targetDir, out raycastHit, targetDist, hitObjectLayerMask);
            if (isHit)
            {
                cameraPosition = raycastHit.point;
            }
            transform.localPosition = cameraPosition;

            Vector3 vector3 = new Vector3(Input.GetAxis("Mouse X") * rotatingVelocity, Input.GetAxis("Mouse Y") * rotatingVelocity, 0);
            transform.RotateAround(lookTargetPos, Vector3.up, vector3.x);

            if (transform.forward.y > 0.7f && vector3.y < 0)
            {
                vector3.y = 0;
            }
            if (transform.forward.y < -0.7f && vector3.y > 0)
            {
                vector3.y = 0;
            }
            transform.RotateAround(lookTargetPos, transform.right, vector3.y);
        }
        else
        {
            transform.position = playerTransform.position;
            transform.LookAt(enemyCoreTransform, Vector3.up);
        }
    }

    private void SetUpRotationCamera()
    {
        transform.localPosition = playerTransform.localPosition - (playerTransform.forward * playerDistans);
        transform.localPosition += Vector3.up * playerHight;
        transform.LookAt(playerTransform.localPosition + cameraOffset);
    }

    public void Shake(float duration = 4.0f, float magnitude = 4.0f)
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        var pos = transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}
