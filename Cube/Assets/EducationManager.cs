using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EducationManager : MonoBehaviour
{
    public Text coins,  skor, skorSonuc, CoinSonuc;
    public GameObject restartPanel,complatePanel,ResultsPanel;
    int CoinsArttır;
    float scoreArttir;
    public AudioClip altin;
    public AudioClip engel;
    private int para;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Education") == 0 && PlayerPrefs.GetInt("EducationFirst") == 1)
        {
            ResultsPanel.SetActive(true);
            CoinsArttır = 0;
            scoreArttir = 0;
            skorHesap(CoinsArttır);
        }
        else
        {
            enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("EngelDuvar").Length ==0)
        {
            Tamamlandi();
            enabled = false;
        }
        else if (KarakterKontrolcü.OyunBitti)
        {
            Tekrar();
            enabled = false;
        }
        else
        {
            scoreArttir += Time.deltaTime;
            skor.text = "Score:" + (int)scoreArttir;
        }
    }
    
    public void Tekrar()
    {
        PlayerPrefs.SetInt("tekrar", PlayerPrefs.GetInt("tekrar") + 1);
        restartPanel.SetActive(true);
        Time.timeScale = 1f;//

    }
    private void Tamamlandi()
    {
        skorSonuc.text = "SCORE:" + (int)(scoreArttir);
        CoinSonuc.text = "COİNS:" + CoinsArttır;
        coins.text = "";
        skor.text = "";
        AltinKontrol.paraGuncelle(CoinsArttır + AltinKontrol.parayiGetir());
        //gameScene.SetActive(false);
        complatePanel.SetActive(true);
        KarakterKontrolcü.OyunAktif = false;
        KarakterKontrolcü.OyunBitti = true;
        PlayerPrefs.SetInt("Education", 1);
    }
    public void skorHesap(int altin)
    {
        CoinsArttır += altin;
        coins.text = "COİNS:" + CoinsArttır;
        
    }
}
