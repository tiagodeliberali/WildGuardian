using Assets.Items;

namespace Assets.Signals
{
	public class ItemActionSignal
	{
		public ItemActionSignal(ItemInstance item, ItemAction action)
		{
			this.Item = item;
			this.Action= action;
		}

		public ItemInstance Item { get; set; }

		public ItemAction Action { get; set; }

		public static ItemActionSignal Pickup(ItemInstance item) => new ItemActionSignal(item, ItemAction.Pickup);

		public static ItemActionSignal Sell(ItemInstance item) => new ItemActionSignal(item, ItemAction.Sell);

		public static ItemActionSignal Drop(ItemInstance item) => new ItemActionSignal(item, ItemAction.Drop);

		public static ItemActionSignal Use(ItemInstance item) => new ItemActionSignal(item, ItemAction.Use);
	}

	public enum ItemAction
	{
		Pickup,
		Sell,
		Drop,
		Use
	}
}
