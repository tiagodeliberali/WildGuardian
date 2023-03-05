namespace Assets.InventorySystem
{
    using System;

    using Assets.Items;

    public interface IAssociateInventory
    {
        void CloseWindow();
        bool SelectItem(Item item);
        ItemType GetItemType();

        void AssociateCloseCall(Action closeWindow);
    }
}
