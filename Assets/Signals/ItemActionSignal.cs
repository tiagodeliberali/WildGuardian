namespace Assets.Signals
{
	public class ItemActionSignal
	{
		public ItemActionSignal(Item item, ItemAction action)
		{
			this.Item = item;
			this.Action= action;
		}

		public Item Item { get; set; }

		public ItemAction Action { get; set; }

		public static ItemActionSignal Pickup(Item item) => new ItemActionSignal(item, ItemAction.Pickup);

		public static ItemActionSignal Sell(Item item) => new ItemActionSignal(item, ItemAction.Sell);

		public static ItemActionSignal Drop(Item item) => new ItemActionSignal(item, ItemAction.Drop);

		public static ItemActionSignal Use(Item item) => new ItemActionSignal(item, ItemAction.Use);
	}

	public enum ItemAction
	{
		Pickup,
		Sell,
		Drop,
		Use
	}
}
