using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdmob : MonoBehaviour
{
    private BannerView bannerView;
    void Start()
    {   
        //Initialize the GoogleMobile Ads SDK
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5636374378106028/7348797006";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        //Clean up banner ad before creating a new one
        if(this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);

        //Create an empty ad request
        AdRequest request = new AdRequest.Builder().Build();

        //Load the banner with the request
        this.bannerView.LoadAd(request);
    }

    private void OnDisable()
    {
        this.bannerView.Destroy();
    }
}
