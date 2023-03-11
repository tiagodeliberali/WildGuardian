using Assets.Items;

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public string description;
    public int value;
    public RuntimeAnimatorController animatorController;
}
