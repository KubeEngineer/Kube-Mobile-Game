using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class AdmobOdulluReklam : MonoBehaviour
{



    public Text odulParan;
    public GameObject OdulPanel;
    private RewardedAd rewardedAd;
    private int odulid;



    // Start is called before the first frame update
    void Start()
    {
        // MobileAds.Initialize(initStatus => { });
        RequestRewardBasedVideo();

    }



    //ODULLU REKLAMM GOSTER////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6260028172865745/5021573454";//kendi  Reklam kodumuz eklenecek
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
            string adUnitId = "unexpected_platform";
#endif


        // Called when the user should be rewarded for watching a video.



        this.rewardedAd = new RewardedAd(adUnitId);


        //reklam başarılı bir şekilde izlendiyse
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    //Reklam izlendi ödüllendir fonsiyonu

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        Reward();
    }
    //odul veriyoruz.
    private void Reward()
    {
        if (odulid == 1)
        {
            OdulPanel.SetActive(true);
            Random rastSayi = new Random();
            int rast_odul = rastSayi.Next(0, 50);
            odulParan.text = "" + rast_odul;
            int coins = AltinKontrol.parayiGetir() + rast_odul;

            AltinKontrol.paraGuncelle(coins);
        }
        else if (odulid == 2)
        {
            OdulPanel.SetActive(true);
            Random rastSayi = new Random();
            int rast_odul = rastSayi.Next(0, 50);
            odulParan.text = "" + rast_odul;
            int coins = AltinKontrol.parayiGetir() + rast_odul;
            AltinKontrol.paraGuncelle(coins);

        }

        else if (odulid == 3)
        {
            OdulPanel.SetActive(true);
            Random rastSayi = new Random();
            int rast_odul = rastSayi.Next(0, 50);
            odulParan.text = "" + rast_odul;
            int coins = AltinKontrol.parayiGetir() + rast_odul;
            AltinKontrol.paraGuncelle(coins);
        }
        

    }
    public void OdulReklaminiGoster(int OdulID)
    {


        odulid = OdulID;


        if (this.rewardedAd.IsLoaded())
        {

            this.rewardedAd.Show();

        }


    }
    public void ButonSecici(Button buton)
    {
        buton.interactable = false;
    }


    public void OdulPaneliKapat()
    {
        OdulPanel.SetActive(false);
    }












}
