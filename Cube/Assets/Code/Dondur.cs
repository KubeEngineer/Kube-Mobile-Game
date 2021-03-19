using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dondur : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(transform.position,Vector3.up,50f*Time.deltaTime);
    }
}
