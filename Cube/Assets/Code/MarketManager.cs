
using UnityEngine;

using UnityEngine.UI;

public class MarketManager:MonoBehaviour
{
    [SerializeField]
    private Text para;
    public GameObject objeYetersizBakiye,objeEminmisiniz;
    UrunManager satinAlinacak;
    private void Start()
    {
        para.text = AltinKontrol.paraText(); 
    }

    private void Update()
    {
        para.text = AltinKontrol.paraText();
    }
   public void eminmisiniz(UrunManager urun)
    {
        objeEminmisiniz.SetActive(true);
        satinAlinacak = urun;

    }
    public void cevap(bool cevap)
    {
        if (cevap)
        {
            satinAL(satinAlinacak);
        }
        objeEminmisiniz.SetActive(false);

    }
    public void satinAL(UrunManager urun)
    {
        int para = AltinKontrol.parayiGetir();
        if(para >= urun.getFiyat()) {

          
            
            //para guncellemesi
            int yenipara = para - urun.getFiyat();
            
            AltinKontrol.paraGuncelle(yenipara);
            
            string[] alinanKarakter = urun.getMaterialIsım().Split(' ');
           
            string[] envanter = PlayerPrefs.GetString("Envanter").Split(' ');
             
            PlayerPrefs.SetInt(urun.gameObject.name, 1);
            
            //Envantere yeni karakter ekleniyor
            string EnvanterEski = PlayerPrefs.GetString("Envanter");
            PlayerPrefs.SetString("Envanter", EnvanterEski + " " + alinanKarakter[0]);

            
            
            
        }
        else{
            objeYetersizBakiye.SetActive(true);
        }
            
    }
    public void AktifDegil(GameObject obje)
    {
        obje.SetActive(false);
    }
    public bool EnvanterKontrol(string alinanKarakter,string[] Envanter) 
    {
        foreach(string karakter in Envanter){
            if(karakter.Equals(alinanKarakter)) {
                return false;
            }
        }
        return true;
    }
    

    
 
}
