using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject a;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            a.SetActive(false);
        }
        else
        {
            a.SetActive(true);
        }
    }
}
