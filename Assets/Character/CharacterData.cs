using Assets.Signals;

using UnityEngine;

using Zenject;

namespace Assets.Character
{
	public class CharacterData
	{
		private SignalBus signalBus;

		public int MoneyAmount { get; set; }

		public delegate void EventHandler(int amount);

		public event EventHandler OnMoneyAmountChanged;

		[Inject]
		public void Contruct(SignalBus signalBus)
		{
			this.signalBus = signalBus;
			signalBus.Subscribe<ItemActionSignal>(this.OnItemActionHappened);
		}

		private void OnItemActionHappened(ItemActionSignal item)
		{
			if (item.Action.Equals(ItemAction.Pickup))
			{
				Debug.Log($"Picked {item.Item.Id}");

				if (item.Item.Definition.type == Items.ItemType.Money)
				{
					AddMoney(item.Item.Definition.eggValue);
				}
			}
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
