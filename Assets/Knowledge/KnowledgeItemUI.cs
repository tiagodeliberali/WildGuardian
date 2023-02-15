using Assets.Knowledge;

using UnityEngine;

public class KnowledgeItemUI : MonoBehaviour
{
	private KnowledgeItemInstance item;
	private KnowledgeUI knowledgeManager;

	public void Click()
    {
        knowledgeManager.SelectItem(item);
	}

	public void AssociateItem(KnowledgeItemInstance item, KnowledgeUI knowledgeManager)
	{
		this.item = item;
		this.knowledgeManager = knowledgeManager;
	}

	public void Remove()
	{
		Destroy(gameObject);
	}
}
