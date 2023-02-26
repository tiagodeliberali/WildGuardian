using Assets;
using Assets.Knowledge;

using UnityEngine;
using UnityEngine.UI;

public class KnowledgeItemUI : MonoBehaviour, IGenerateGameObject
{
    private KnowledgeItemInstance item;
    private KnowledgeDetailsUI knowledgeDetails;

    public void Click()
    {
        knowledgeDetails.SelectItem(item);
    }

    public void AssociateItem(KnowledgeDetailsUI knowledgeManager)
    {
        this.knowledgeDetails = knowledgeManager;
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    public GameObject Build<T>(Transform placeholder, T instance)
    {
        if (instance is KnowledgeItemInstance knowledgeInstance)
        {
            GameObject obj = Instantiate(this.gameObject, placeholder);

            var itemName = obj.transform.Find("Icon").GetComponent<Image>();
            itemName.sprite = knowledgeInstance.Definition.icon;

            obj.GetComponent<KnowledgeItemUI>().item = knowledgeInstance;

            return obj;
        }
     
        return null;
    }
}
