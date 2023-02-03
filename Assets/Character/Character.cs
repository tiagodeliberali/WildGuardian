using Assets;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
	public static Character Instance;

	public int MoneyAmount = 300;
	public TextMeshProUGUI moneyAmountUI;

	private Vector2 movement;
	private Rigidbody2D characterRigidBody;
	private Animator animator;

	private void Awake()
	{
		Instance = this;
		characterRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		SetMoneyAmount();
	}

	private void OnMovement(InputValue value)
	{
		movement = value.Get<Vector2>();

		if (movement.magnitude > 0)
		{
			animator.SetFloat("X", movement.x);
			animator.SetFloat("Y", movement.y);
			animator.SetBool("IsWalking", true);
		}
		else
		{
			animator.SetBool("IsWalking", false);
		}
	}

	private void FixedUpdate()
	{
		characterRigidBody.MovePosition(
			characterRigidBody.position +
			Time.fixedDeltaTime * GameConfiguration.DeltaTimeVelocity * movement);
	}

	public bool CanSpendMoney(int amount)
	{
		return amount < MoneyAmount;
	}

	public bool SpendMoney(int amount)
	{
		if (!CanSpendMoney(amount))
		{
			return false;
		}

		MoneyAmount -= amount;
		SetMoneyAmount();
		return true;
	}

	private void SetMoneyAmount()
	{
		moneyAmountUI.text = MoneyAmount.ToString();
	}
}
