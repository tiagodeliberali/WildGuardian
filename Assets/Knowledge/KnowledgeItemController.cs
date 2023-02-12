using TMPro;

using UnityEngine;

using Zenject;

public class KnowledgeItemController : MonoBehaviour
{
    public Item item;
	private KnowledgeManager knowledgeManager;

	[Inject]
	public void Contruct(KnowledgeManager knowledgeManager)
	{
		this.knowledgeManager = knowledgeManager;
	}

	public void Click()
    {
        Debug.Log($"Clicked on {item.itemName}");
		knowledgeManager.AssociateItem(item);
	}
}
