using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
	public string itemName;
	public ItemType type;
	public Sprite icon;
}

public enum ItemType
{
	Egg,
	Drop
}

