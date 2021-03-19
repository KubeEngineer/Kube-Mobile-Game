using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AltinKontrol : MonoBehaviour
{
    [SerializeField]
    private Text altin;

    private void Start()
    {
        altin.text = paraText();
    }
    private void Update()
    {

        altin.text = paraText();

    }
   

    public static string paraText()
    {
        return "Coins:" + " " + PlayerPrefs.GetInt("Paramiz") + " AE";
    }
    public static int parayiGetir()
    {
        return PlayerPrefs.GetInt("Paramiz");
    }
    public static void paraGuncelle(int yeniPara)
    {

       
        PlayerPrefs.SetInt("Paramiz", yeniPara);
       
    }

   

}
