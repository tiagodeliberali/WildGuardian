using Assets.MessageSystem;

using TMPro;

using UnityEngine;

public class BuyTerrainManager : MonoBehaviour
{
    public GameObject buyTerrainUI;

	public MessageManager messageManager;

	private TextMeshProUGUI buyTerrainAmount;
	private Transform okButton;
	private Transform notEnoughMoneyUI;

	private TerrainController terrain;

    public static BuyTerrainManager Instance;

	private void Awake()
	{
        Instance = this;

		okButton = buyTerrainUI.transform.Find("OkButton");
		notEnoughMoneyUI = buyTerrainUI.transform.Find("NotEnoughMoney");
		buyTerrainAmount = buyTerrainUI.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
	}

	public void Associate(TerrainController terrain)
    { 
        this.terrain = terrain;
		buyTerrainAmount.text = terrain.Amount.ToString();

		bool canBuyTerrain = Character.Instance.CanSpendMoney(terrain.Amount);
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
		Character.Instance.SpendMoney(terrain.Amount);

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
