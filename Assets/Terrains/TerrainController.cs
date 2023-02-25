using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public int Amount = 200;

    public BuyTerrainUI buyTerrainUI;

    public void Buy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        buyTerrainUI.Select(this);
    }
}
