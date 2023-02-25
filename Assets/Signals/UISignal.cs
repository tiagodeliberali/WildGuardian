namespace Assets.Signals
{
    public class UISignal
    {
        public bool IsOpen { get; set; }

        public static object Closed() => new UISignal() { IsOpen = false };

        public static object Opened() => new UISignal() { IsOpen = true };
    }
}
