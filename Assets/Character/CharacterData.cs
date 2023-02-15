using Assets.Knowledge;
using System.Collections.Generic;

using Assets.Signals;

using UnityEngine;

using Zenject;
using Assets.Items;

namespace Assets.Character
{
	public class CharacterData
	{
		public Dictionary<string, KnowledgeItemInstance> knowledge = new Dictionary<string, KnowledgeItemInstance>();

		public List<ItemInstance> inventory = new List<ItemInstance>();

		public int MoneyAmount { get; set; }

		public delegate void EventHandler(int amount);

		public event EventHandler OnMoneyAmountChanged;

		[Inject]
		public void Contruct(SignalBus signalBus)
		{
			signalBus.Subscribe<ItemActionSignal>(this.OnItemActionHappened);
		}

		private void OnItemActionHappened(ItemActionSignal item)
		{
			if (item.Action.Equals(ItemAction.Pickup))
			{
				switch (item.Item.Definition.type)
				{
					case ItemType.Egg:
						AddKnowledge(item.Item.Definition);
						inventory.Add(item.Item);
						break;
					case ItemType.Drop:
						inventory.Add(item.Item);
						break;
					case ItemType.Money:
						AddMoney(item.Item.Definition.eggValue);
						break;
				}
			}
		}

		private void AddKnowledge(Item definition)
		{
			if (!knowledge.ContainsKey(definition.itemName))
			{
				knowledge.Add(definition.itemName, new KnowledgeItemInstance(definition));
			}

			knowledge[definition.itemName].Add();
		}

		public bool CanSpendMoney(int amount)
		{
			return amount <= MoneyAmount;
		}

		public bool SpendMoney(int amount)
		{
			if (!CanSpendMoney(amount))
			{
				return false;
			}

			AddMoney(-amount);

			return true;
		}

		public void AddMoney(int amount)
		{
			MoneyAmount += amount;
			OnMoneyAmountChanged?.Invoke(MoneyAmount);
		}
	}
}
