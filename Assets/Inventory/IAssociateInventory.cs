namespace Assets.InventorySystem
{
    using Assets.Items;

    public interface IAssociateInventory
    {
        void CloseWindow();
        bool SelectItem(Item item);
        ItemType GetItemType();

    }
}
