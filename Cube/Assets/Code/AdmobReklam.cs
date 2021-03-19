using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmobReklam : MonoBehaviour
{
    private InterstitialAd Interstitial;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });   
        RequestInterstitial();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        GecisReklamlariniYoket();
    }
    private void RequestInterstitial()
    {
    #if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6260028172865745/5339786584"; 
    #endif
        this.Interstitial = new InterstitialAd(adUnitId); 
        this.Interstitial.OnAdClosed += HandleOnAdClosed;       
        AdRequest request = new AdRequest.Builder().Build();
        this.Interstitial.LoadAd(request);
    }
    
    public void InstertatialGoster()
    {
        if (this.Interstitial.IsLoaded())
        {
            this.Interstitial.Show();
        }
    }
    public void GecisReklamlariniYoket()
    {
        Interstitial.Destroy();
    }
}



