using System.Collections.Generic;

using Assets.MessageSystem;

using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;

	public List<IMessageSubscriber> Subscribers = new List<IMessageSubscriber>();

	private void Awake()
	{
		Instance = this;
	}

	public void AlertSubscribers(Message message)
	{
		foreach (var item in Subscribers)
		{
			item.MessageReceived(message);
		}
	}

	public void Subscribe(IMessageSubscriber subscriber)
	{
		Subscribers.Add(subscriber);
	}
}
