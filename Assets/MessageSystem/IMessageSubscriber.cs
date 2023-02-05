namespace Assets.MessageSystem
{
	public interface IMessageSubscriber
	{
		public void MessageReceived(Message message);
	}

}
