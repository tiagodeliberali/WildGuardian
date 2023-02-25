namespace Assets.Items
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Animal")]
    public class Animal : Item
    {
        public int timeToNext;
        public Item next;
        public AnimalType animalType;

        public string GetFood()
        {
            switch (animalType)
            {
                case AnimalType.basic:
                    return "Milho";
                case AnimalType.fire:
                    return "Magma";
                case AnimalType.ice:
                    return "Neve";
            }
            return "<ão definido>";
        }

        public string GetIncubator()
        {
            switch (animalType)
            {
                case AnimalType.basic:
                    return "Basico";
                case AnimalType.fire:
                    return "Vulcanico";
                case AnimalType.ice:
                    return "Polar";
            }
            return "<ão definido>";
        }
    }
}
