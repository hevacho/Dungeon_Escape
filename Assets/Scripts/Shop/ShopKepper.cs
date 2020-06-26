
using UnityEngine;

public class ShopKepper : MonoBehaviour
{

    [SerializeField]
    private GameObject _shopPanel;

    private int selectedItem=-1;
    private int currentItemCost = 0;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                int gems = player.Diamonds();
                UiManager.IUinstance.openShop(gems);
                _shopPanel.SetActive(true);
            }
        }
        
    }

    private void Update()
    {
        if (player != null)
        {
            if(Vector3.Distance(player.transform.position, transform.position) > 8)
            {
                _shopPanel.SetActive(false);
            }
        }
        
    }

    
    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0 : UiManager.IUinstance.UpdateShopSelection(69);
                selectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UiManager.IUinstance.UpdateShopSelection(-46);
                selectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UiManager.IUinstance.UpdateShopSelection(-148);
                selectedItem = 2;
                currentItemCost = 100;
                break;
        }

    }


    public void BuyItem()
    {
        
        if (player.Diamonds() >= currentItemCost)
        {
            player.addDiamond( -currentItemCost);
            if (selectedItem == 2)
            {
                GameManager.Instance.hasKeyToCastle = true;
            }
            _shopPanel.SetActive(false);
            Debug.Log("BuyItem");
        }
        else
        {
            Debug.Log("You do not have enoght gems");
            _shopPanel.SetActive(false);
        }
    }
}
