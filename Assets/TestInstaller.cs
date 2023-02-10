using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
	public InventoryManager inventoryManager;

	public override void InstallBindings()
    {
		Container.Bind<InventoryManager>().FromInstance(inventoryManager);
	}
}