
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject complate, uiSceene,RekorKirildi;
    //public GameObject oyunBitti;
    //AltinKontrol altinKontrol;
    public Text coins, skorSonuc, skor, EnyuksekSkor, CoinSonuc,rekorKirildiPanelininSkor, rekorKirildiPanelininAltini;
    //public string sonrakiLevel;
    int CoinsArttır;
    float scoreArttir;
    public AudioClip altin;
    public AudioClip engel;
    private int para;
   // private GameObject gameScene;
    

    public GameObject pauseMenuUI;
    public static bool OyunDur = false;
    public static bool isEduDone = false;
    public static bool isDevelopment = false;
    bool tamam;

    void Start()
    {
        isEduDone =PlayerPrefs.GetInt("Education") == 1 && PlayerPrefs.GetInt("EducationFirst") == 1;
        if (isEduDone)
        {
            //altinKontrol = GameObject.Find("_script").GetComponent<AltinKontrol>();
            OyunDur = false;
            CoinsArttır = 0;
            scoreArttir = 0;
            skorHesap(CoinsArttır);
            EnyuksekSkor.text = "HIGH SCORE\n" + (int)PlayerPrefs.GetInt("skor");
            //gameScene = GameObject.FindGameObjectWithTag("Game");
        }else
        {
            uiSceene.SetActive(false);
            enabled = false;
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (KarakterKontrolcü.OyunBitti)
        {
            skorGuncelle(scoreArttir);
            enabled = false;
        }
        else
        {
            scoreArttir += Time.deltaTime;
            skor.text = "Score:" + (int)scoreArttir;
        }



    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        uiSceene.SetActive(true);
        Time.timeScale = 1f;

        OyunDur = false;

    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        uiSceene.SetActive(false);
        Time.timeScale = 0f;
        OyunDur = true;
    }

    public void skorGuncelle(float yeniSkor)
    {

        int eskiSkor = PlayerPrefs.GetInt("skor");
        if (yeniSkor > eskiSkor)
        {
            uiSceene.SetActive(false);
            rekorKirildiPanelininSkor.text = "Skor:"+(int)yeniSkor;
            rekorKirildiPanelininAltini.text = "COİNS:" + CoinsArttır;
            EnyuksekSkor.text = "HIGH SCORE\n" + (int)yeniSkor;
            RekorKirildi.SetActive(true);
            PlayerPrefs.SetInt("skor", (int)yeniSkor);
        }
        else
        {
            PlayerPrefs.SetInt("skor", (int)eskiSkor);
            EnyuksekSkor.text = "" + PlayerPrefs.GetInt("skor");
            Tamamlandi();
        }
    }


    private void Tamamlandi()
    {
        skorSonuc.text = "SCORE:" + (int)(scoreArttir);
        CoinSonuc.text = "COİNS:" + CoinsArttır;
        coins.text = "";
        skor.text = "";
        AltinKontrol.paraGuncelle(CoinsArttır + AltinKontrol.parayiGetir());
        //gameScene.SetActive(false);
        complate.SetActive(true);
        uiSceene.SetActive(false);
    }

    public void skorHesap(int altin)
    {
        CoinsArttır += altin;
        coins.text = "COİNS:" + CoinsArttır;
        


    }
    public void RekorPanelGorunurluguKapa()
    {
        RekorKirildi.SetActive(false);
        Tamamlandi();
    }
    public void Menuye()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
        OyunDur = false;

    }

}
