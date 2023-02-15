using Assets.Signals;

using ModestTree;

using UnityEngine;

using Zenject;

namespace Assets.Character
{
	public class CharacterData
	{
		private SignalBus signalBus;

		public int MoneyAmount { get; set; }

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

			MoneyAmount -= amount;
			return true;
		}

		
	}
}
