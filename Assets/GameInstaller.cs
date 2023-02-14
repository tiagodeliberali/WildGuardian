using Assets.Character;
using Assets.Signals;

using Zenject;

public class GameInstaller : MonoInstaller
{
	public InventoryUI inventoryManager;
	
	public KnowledgeUI knowledgeManager;

	public override void InstallBindings()
	{
		SignalBusInstaller.Install(Container);

		Container.Bind<CharacterData>()
			.FromNew()
			.AsSingle();

		Container.Bind<InventoryUI>()
			.FromInstance(inventoryManager);

		Container.Bind<KnowledgeUI>()
			.FromInstance(knowledgeManager);

		Container.DeclareSignal<UISignal>();
		Container.DeclareSignal<ItemActionSignal>();
	}
}