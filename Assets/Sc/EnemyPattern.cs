using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemyPattern 
{
    void Attack1(int a);
    void Attack2(float a, float b);
    void Attack3(float a, float b);
    void Attack4(float a, float b);
    //void Attack5(float a, float b);
    void Frezze();
}
