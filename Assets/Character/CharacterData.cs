using System.Collections.Generic;

using Assets.Items;
using Assets.Knowledge;
using Assets.Signals;

using Zenject;

namespace Assets.Character
{
    /// <summary>
    /// Close relation with UI
    /// Exposes imutable data
    /// Only mutable through methods
    /// </summary>
    public class CharacterData
    {
        public int MoneyAmount { get; private set; }

        public delegate void EventHandler(int amount);

        public event EventHandler OnMoneyAmountChanged;

        public IReadOnlyDictionary<string, KnowledgeItemInstance> Knowledge => knowledge;

        public IReadOnlyList<Item> Inventory => inventory;

        private Dictionary<string, KnowledgeItemInstance> knowledge = new Dictionary<string, KnowledgeItemInstance>();
        private List<Item> inventory = new List<Item>();

        [Inject]
        public void Contruct(SignalBus signalBus)
        {
            signalBus.Subscribe<ItemActionSignal>(this.OnItemActionHappened);
        }

        private void OnItemActionHappened(ItemActionSignal action)
        {
            if (action.Action.Equals(ItemAction.Pickup))
            {
                switch (action.Item.type)
                {
                    case ItemType.Puppy:
                    case ItemType.Egg:
                    case ItemType.Drop:
                        AddKnowledge(action.Item);
                        inventory.Add(action.Item);
                        break;
                    case ItemType.Money:
                        AddMoney(action.Item.value);
                        break;
                }
            }
            else if (action.Action.Equals(ItemAction.Drop) || action.Action.Equals(ItemAction.Use))
            {
                switch (action.Item.type)
                {
                    case ItemType.Egg:
                    case ItemType.Puppy:
                    case ItemType.Drop:
                        inventory.Remove(action.Item);
                        break;
                    case ItemType.Money:
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
