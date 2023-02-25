using Assets.Items;

namespace Assets.InventorySystem
{
    public interface IAssociateInventory
    {
        void CloseWindow();
        bool SelectItem(Item item);
        ItemType GetItemType();

    }
}
