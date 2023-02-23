using Assets.Items;

namespace Assets.InventorySystem
{
	public interface IAssociateInventory
	{
		void CloseWindow();
		bool SelectItem(ItemInstance item);
		ItemType GetItemType();

	}
}
