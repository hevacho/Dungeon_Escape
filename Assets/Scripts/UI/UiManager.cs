using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager _instance;

    public Text playerGameCountTex;
    public Text gemCount;

    [SerializeField]
    private Image selection;
       
    public static UiManager IUinstance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void openShop(int gemCountPlayer)
    {
        playerGameCountTex.text = gemCountPlayer + " G";
    }    

    public void UpdateShopSelection (int yPos)
    {
        selection.rectTransform.anchoredPosition = new Vector2(selection.rectTransform.anchoredPosition.x, yPos);
        
    }

    public void UpdateGemCount(int count)
    {
        gemCount.text = "" + count;
    }
}
