using UnityEngine;

public class IncubatorSprite : MonoBehaviour
{
	public IncubatorUI IncubatorUI;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		IncubatorUI.OpenUI();
	}
}
