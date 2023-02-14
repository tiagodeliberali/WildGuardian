using System;

using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
	public string itemName;
	public ItemType type;
	public Sprite icon;
	public string description;
	public string incubator;
	public int eggValue;
	public int puppyValue;
	public string drop;
	public int dropValue;
	public int timeToHatch;
	public int timeToGrow;
	public string food;
	public int count = 0;
}

public enum ItemType
{
	Egg,
	Drop
}

