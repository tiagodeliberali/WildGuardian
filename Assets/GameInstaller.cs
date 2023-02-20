using Assets.Character;
using Assets.Signals;

using Zenject;

public class GameInstaller : MonoInstaller
{
	public InventoryUI inventoryManager;
	
	public KnowledgeUI knowledgeManager;

	public TimeManager timeManager;

	public override void InstallBindings()
	{
		SignalBusInstaller.Install(Container);

		Container.Bind<CharacterData>()
			.FromNew()
			.AsSingle();

		Container.Bind<TimeManager>()
			.FromInstance(timeManager);

		Container.Bind<InventoryUI>()
			.FromInstance(inventoryManager);

		Container.Bind<KnowledgeUI>()
			.FromInstance(knowledgeManager);

		Container.DeclareSignal<UISignal>();
		Container.DeclareSignal<ItemActionSignal>();
	}
}