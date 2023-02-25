namespace Assets.Knowledge
{
    public class KnowledgeItemInstance
    {
        public Item Definition { get; }

        public int Count { get; private set; }

        public KnowledgeItemInstance(Item Item)
        {
            this.Definition = Item;
        }

        public void Add()
        {
            this.Count++;
        }
    }
}
