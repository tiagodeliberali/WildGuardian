using Assets.Character;

using TMPro;

using UnityEngine;

using Zenject;

public class MoneyUI : MonoBehaviour
{
    private TextMeshProUGUI moneyUI;

    [Inject]
    public void Contruct(CharacterData character)
    {
        character.OnMoneyAmountChanged += this.OnMoneyAmountChanged;
        this.moneyUI = this.transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    }

    private void OnMoneyAmountChanged(int amount) => moneyUI.text = $"$ {amount}";
}
