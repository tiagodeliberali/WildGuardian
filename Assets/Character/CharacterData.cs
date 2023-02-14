using Assets.Signals;

using ModestTree;

using Zenject;

namespace Assets.Character
{
	public class CharacterData : IInitializable
	{
		private SignalBus signalBus;

		public int MoneyAmount { get; set; }

		[Inject]
		public void Contruct(SignalBus signalBus)
		{
			this.signalBus = signalBus;
		}

		public void Initialize()
		{
			signalBus.Subscribe<ItemActionSignal>(this.OnItemActionHappened);
		}

		private void OnItemActionHappened(ItemActionSignal item)
		{
			if (item.Action.Equals(ItemAction.Pickup))
			{
				Log.Debug($"Picked {item.Item.Id}");
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
