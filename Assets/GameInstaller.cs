using Assets.Signals;

using Zenject;

public class GameInstaller : MonoInstaller
{
	public InventoryManager inventoryManager;
	
	public KnowledgeManager knowledgeManager;

	public override void InstallBindings()
	{
		SignalBusInstaller.Install(Container);

		Container.Bind<InventoryManager>()
			.FromInstance(inventoryManager);

		Container.Bind<KnowledgeManager>()
			.FromInstance(knowledgeManager);

		Container.DeclareSignal<UISignal>();
	}
}