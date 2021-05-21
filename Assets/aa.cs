using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aa : MonoBehaviour
{
    float time1 = 10;
    float ti = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ti += Time.deltaTime;
        if (time1 < ti)
        {
            GetComponent<NavMeshSurface>().BuildNavMesh();
            ti = 0;
        }
    }
}
