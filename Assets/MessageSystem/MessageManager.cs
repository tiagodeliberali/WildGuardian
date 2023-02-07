using System.Collections.Generic;

using Assets.MessageSystem;

using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public List<IMessageSubscriber> Subscribers = new List<IMessageSubscriber>();

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

	internal void Unsubscribe(IMessageSubscriber subscriber)
	{
		Subscribers.Remove(subscriber);
	}
}
