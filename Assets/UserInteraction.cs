using UnityEngine;

namespace Assets
{
	public static class UserInteraction
	{
		public static bool MoveUp() => Input.GetKey(KeyCode.UpArrow);
		public static bool MoveDown() => Input.GetKey(KeyCode.DownArrow);
		public static bool MoveLeft() => Input.GetKey(KeyCode.LeftArrow);
		public static bool MoveRight() => Input.GetKey(KeyCode.RightArrow);
	}
}
