using System;

namespace Assets.Items
{
	public class ItemInstance
	{
		public Guid Id { get; }
		
		public Item Definition { get; }

		public ItemInstance(Guid guid, Item item) 
		{
			Id = guid;
			Definition = item;
		}		
	}
}
