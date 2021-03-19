using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hareket : MonoBehaviour
{


    private float hiz = 15f;
    
    private void Start()
    {
        if(PlayerPrefs.GetInt("EducationFirst") == 0)
        {
            enabled = false;
        }
        else if (PlayerPrefs.GetInt("Education") == 0)
        {
            hiz = 10f;
            StartCoroutine(hizlan());
        }else
        {
            hiz = 10f;
            StartCoroutine(hizlan());
        }
        

    }

    void Update()
    {
        if (KarakterKontrolcü.OyunAktif)
        {
            float hareket_hizi = hiz * 1* Time.deltaTime;
            
            transform.Translate(0f, 0f, hareket_hizi);
        }
        else
        {
            StopAllCoroutines();
            
        }

    }
    IEnumerator hizlan()
    {
        float bekle = 2f;
        while (true)
        {
            bekle += 0.2f;
            hiz += 0.4f;
           
            yield return new WaitForSeconds(bekle);
        }
    }

    public void setHiz(float hiz)
    {
        this.hiz = hiz;
    }
    public float getHiz()
    {
        return this.hiz;
    }



}