using System;

namespace Assets.Items
{
	public class ItemInstance
	{
		public Guid Id { get; }
		
		public Item Definition { get; }

		public ItemInstance(Item item) 
		{
			Id = Guid.NewGuid();
			Definition = item;
		}		
	}
}
