using TMPro;

using UnityEngine;

public class BuyTerrainManager : MonoBehaviour
{
    public GameObject buyTerrainUI;

	private TextMeshProUGUI buyTerrainAmount;
    private TerrainController terrain;

    public static BuyTerrainManager Instance;

	private void Awake()
	{
        Instance = this;
		buyTerrainAmount = buyTerrainUI.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
	}

	public void Associate(TerrainController terrain)
    { 
        this.terrain = terrain;
		buyTerrainAmount.text = terrain.Amount.ToString();

        ShowUI();
	}

    public void Close()
    {
		HideUI();
	}

    public void Buy()
    {
        terrain?.Buy();
        terrain = null;

		HideUI();
	}

    private void ShowUI()
    {
		buyTerrainUI.SetActive(true);
	}

	private void HideUI()
	{
		buyTerrainUI.SetActive(false);
	}
}
