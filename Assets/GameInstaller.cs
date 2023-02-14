using Assets.Character;
using Assets.Signals;

using Zenject;

public class GameInstaller : MonoInstaller
{
	public InventoryUI inventoryManager;
	
	public KnowledgeManager knowledgeManager;

	public override void InstallBindings()
	{
		SignalBusInstaller.Install(Container);

		Container.Bind<CharacterData>()
			.FromNew()
			.AsSingle();

		Container.Bind<InventoryUI>()
			.FromInstance(inventoryManager);

		Container.Bind<KnowledgeManager>()
			.FromInstance(knowledgeManager);

		Container.DeclareSignal<UISignal>();
		Container.DeclareSignal<ItemActionSignal>();
	}
}