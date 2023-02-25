using Assets.Character;

using TMPro;

using UnityEngine;

using Zenject;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyUI;

    [Inject]
    public void Contruct(CharacterData character)
    {
        character.OnMoneyAmountChanged += this.OnMoneyAmountChanged;
    }

    private void OnMoneyAmountChanged(int amount) => moneyUI.text = $"$ {amount}";
}
