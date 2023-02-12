using UnityEngine;

using Zenject;

public class KnowledgeItemController : MonoBehaviour
{
    private Item item;
	private KnowledgeManager knowledgeManager;

	public void Click()
    {
        Debug.Log($"Clicked on {item.itemName}");
		knowledgeManager.AssociateItem(item);
	}

	public void AssociateItem(Item item, KnowledgeManager knowledgeManager)
	{
		this.item = item;
		this.knowledgeManager = knowledgeManager;
	}
}
