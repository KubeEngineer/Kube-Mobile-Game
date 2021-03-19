
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject obje;
    public Text highScore;
    public Text goldScore;
    private void Start()
    {
        highScore.text = "" + PlayerPrefs.GetInt("skor");
        goldScore.text = AltinKontrol.parayiGetir() + " AE";
        if(Klon.areObjectsEmpty())
            Klon.nesneleriGetir();
    }
    public void GorunurYap()
    {
        obje.SetActive(true);
    }
    public void GorunurYapma()
    {
        obje.SetActive(false);
    }


}
