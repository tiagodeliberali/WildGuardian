using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public int Amount = 200;

    public GameObject parent;

	public void Buy()
    {
        Destroy(parent);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		BuyTerrainManager.Instance.Associate(this);
	}
}
