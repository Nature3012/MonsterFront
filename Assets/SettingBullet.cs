using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBullet : MonoBehaviour
{
    [SerializeField] private int bulletNom = 1;

    public void SetBu() 
    {
        BulletContllor bulletContllor = GameObject.Find("BulletCont").GetComponent<BulletContllor>();
        bulletContllor.SetBullet(bulletNom);
    }
}
