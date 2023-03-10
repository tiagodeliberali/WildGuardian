using UnityEngine;

using Zenject;

public class ShopSprite : MonoBehaviour
{
    private ShopUI shopUI;

    [Inject]
    public void Contruct(ShopUI shopUI)
    {
        this.shopUI = shopUI;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shopUI.OpenUI();
    }
}
