using Assets.Signals;

using Zenject;

public class GameInstaller : MonoInstaller
{
	public InventoryManager inventoryManager;

	public override void InstallBindings()
    {
		SignalBusInstaller.Install(Container);

		Container.Bind<InventoryManager>()
			.FromInstance(inventoryManager);

		Container.DeclareSignal<UISignal>();
	}
}