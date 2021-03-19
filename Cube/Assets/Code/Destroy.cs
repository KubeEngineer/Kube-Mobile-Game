using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject obje;
    bool aktif = true;
    // Update is called once per frame
    void Update()
    {
        if (obje != null && aktif && transform.position.z < obje.transform.position.z)
        {
            Debug.Log(Klon._engelSayisi);
            aktif = false;
            Destroy(gameObject,1f);
            Klon._engelSayisi--;
        }
    }
}
