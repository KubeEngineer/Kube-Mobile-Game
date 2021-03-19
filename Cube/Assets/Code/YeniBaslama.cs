
using UnityEngine;

public class YeniBaslama 
{
    private static YeniBaslama yeniBaslama;
    public static YeniBaslama GetYeniBaslama()
    {
        if(yeniBaslama == null)
        {
            yeniBaslama = new YeniBaslama();
        }
        return yeniBaslama;
    }

    public void Start()
    {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("IlkAcilis", 1);
            PlayerPrefs.SetInt("MarketIlkGiris", 1);
            PlayerPrefs.SetInt("Paramiz", 100);
            PlayerPrefs.SetString("GecerliKarakter", "ilkEleman");
            PlayerPrefs.SetString("Envanter", "ilkEleman");
            PlayerPrefs.SetInt("MarketIlkGiris", 1);
           
    }
}
