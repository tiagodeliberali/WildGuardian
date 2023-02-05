namespace Assets.MessageSystem
{
	public class Message
	{
		public readonly MessageType type;

		public Message(MessageType type)
		{
			this.type = type;
		}
	}
}
