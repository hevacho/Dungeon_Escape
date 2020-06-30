
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    #if UNITY_IOS
        private string gameId = "";
    #elif UNITY_ANDROID
        private string gameId = "";
    #endif

    public string myPlacementId = "rewardedVideo";
    public bool ready = false;

    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("finalizado correctamente");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("el tio ha skipeado");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("error al reproducir");
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("anuncio listo");
        ready = true;
    }

    public void ShowRewardedAd()
    {
        Debug.Log("hola");
        if(Advertisement.IsReady())
        {
            Advertisement.Show(myPlacementId);
        }
        else
        {
            Debug.Log("not ready");
        }
    }
}
