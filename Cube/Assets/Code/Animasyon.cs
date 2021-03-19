using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasyon : MonoBehaviour
{
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

    }
    //2.2f -2.27f
    // Update is called once per frame
    void Update()
    {
        
        pos = new Vector3(pos.x, pos.y, pos.z);
        pos.x = pos.x + 0.01f;
        if (pos.x > -36.38)
        {
            pos.x = -40.74f;
        }
        transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
        
    }
}
