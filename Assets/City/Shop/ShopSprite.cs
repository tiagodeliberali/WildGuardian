using System.Collections.Generic;

using Assets.Items;

using UnityEngine;

using Zenject;

public class ShopSprite : MonoBehaviour
{
    private ShopUI shopUI;
    public List<Item> shopInventory;

    public float BuyFactor = 0.8f;

    public float SellFactor = 2f;

    public ItemType ItemType;

    [Inject]
    public void Contruct(ShopUI shopUI)
    {
        this.shopUI = shopUI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopUI.OpenUI(this.shopInventory, BuyFactor, SellFactor, ItemType);
        }
    }
}
