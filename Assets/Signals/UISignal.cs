namespace Assets.Signals
{
	public class UISignal
	{
		public bool IsOpen { get; set; }

		internal static object Closed() => new UISignal() { IsOpen = false };

		internal static object Opened() => new UISignal() { IsOpen = true };
	}
}
