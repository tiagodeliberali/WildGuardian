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
        SignalBusInstaller.Install(this.Container);

        this.Container.Bind<CharacterData>()
            .FromNew()
            .AsSingle();

        this.Container.Bind<TimeManager>()
            .FromInstance(timeManager);

        this.Container.Bind<InventoryUI>()
            .FromInstance(inventoryManager);

        this.Container.Bind<KnowledgeUI>()
            .FromInstance(knowledgeManager);

        this.Container.DeclareSignal<UISignal>();
        this.Container.DeclareSignal<ItemActionSignal>();
    }
}