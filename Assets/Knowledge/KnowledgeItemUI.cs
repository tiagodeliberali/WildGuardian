using UnityEngine;

public class KnowledgeItemUI : MonoBehaviour
{
	private string itemName;
	private KnowledgeUI knowledgeManager;

	public void Click()
    {
        knowledgeManager.SelectItem(itemName);
	}

	public void AssociateItem(string itemName, KnowledgeUI knowledgeManager)
	{
		this.itemName = itemName;
		this.knowledgeManager = knowledgeManager;
	}
}
