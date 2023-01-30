using System;

using Assets;

using UnityEngine;

public class Character : MonoBehaviour
{
    void Update()
	{
		UpdatePosition();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger happened!");
	}

	private void UpdatePosition()
	{
		var position = new Vector3();

		if (UserInteraction.MoveLeft())
		{
			position.x -= 1;
		}

		if (UserInteraction.MoveRight())
		{
			position.x += 1;
		}

		if (UserInteraction.MoveUp())
		{
			position.y += 1;
		}

		if (UserInteraction.MoveDown())
		{
			position.y -= 1;
		}

		this.transform.position += position.normalized * Time.deltaTime * GameConfiguration.DeltaTimeVelocity;
	}
}
