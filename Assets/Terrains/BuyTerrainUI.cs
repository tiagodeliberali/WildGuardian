using Assets.MessageSystem;

using TMPro;

using UnityEngine;

public class BuyTerrainUI : MonoBehaviour
{
    public GameObject buyTerrainUI;

	public MessageManager messageManager;
	public Character character;

	private TextMeshProUGUI buyTerrainAmount;
	private Transform okButton;
	private Transform notEnoughMoneyUI;

	private TerrainController terrain;

    private void Awake()
	{
        okButton = buyTerrainUI.transform.Find("OkButton");
		notEnoughMoneyUI = buyTerrainUI.transform.Find("NotEnoughMoney");
		buyTerrainAmount = buyTerrainUI.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
	}

	public void Associate(TerrainController terrain)
    { 
        this.terrain = terrain;
		buyTerrainAmount.text = terrain.Amount.ToString();

		bool canBuyTerrain = character.CanSpendMoney(terrain.Amount);
		okButton.gameObject.SetActive(canBuyTerrain);
		notEnoughMoneyUI.gameObject.SetActive(!canBuyTerrain);

		ShowUI();
	}

    public void Close()
    {
		HideUI();
	}

    public void Buy()
    {
		character.SpendMoney(terrain.Amount);

		terrain?.Buy();
        terrain = null;

		HideUI();
	}

    private void ShowUI()
    {
		buyTerrainUI.SetActive(true);
		messageManager.AlertSubscribers(new Message(MessageType.UIWindowOpened));
	}

	private void HideUI()
	{
		buyTerrainUI.SetActive(false);
		messageManager.AlertSubscribers(new Message(MessageType.UIWindowClosed));
	}
}
