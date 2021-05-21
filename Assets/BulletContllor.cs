using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContllor : MonoBehaviour
{
    private int Bullet = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetBullet(int a) 
    {
        Bullet = a;
        Debug.Log(a);
    }

    public int GetBullet() 
    {
        return Bullet;
    }
}
