using Assets.Knowledge;

using UnityEngine;

public class KnowledgeItemUI : MonoBehaviour
{
    private KnowledgeItemInstance item;
    private KnowledgeDetailsUI knowledgeDetails;

    public void Click()
    {
        knowledgeDetails.SelectItem(item);
    }

    public void AssociateItem(KnowledgeItemInstance item, KnowledgeDetailsUI knowledgeManager)
    {
        this.item = item;
        this.knowledgeDetails = knowledgeManager;
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
