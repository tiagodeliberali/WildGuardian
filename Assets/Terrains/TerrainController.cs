using UnityEngine;

public class TerrainController : MonoBehaviour
{
	public int Amount = 200;

	public BuyTerrainUI buyTerrainUI;

	public void Buy()
	{
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		buyTerrainUI.Associate(this);
	}
}
