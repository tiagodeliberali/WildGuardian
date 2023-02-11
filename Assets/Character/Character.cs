using Assets;
using Assets.Signals;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;

public class Character : MonoBehaviour
{
	public int MoneyAmount = 300;
	public TextMeshProUGUI moneyAmountUI;

	private SignalBus signalBus;

	private Vector2 movement;
	private Rigidbody2D characterRigidBody;
	private Animator animator;

	private bool paused = false;


	[Inject]
	public void Contruct(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	private void Awake()
	{
		characterRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		SetMoneyAmount();
	}

	private void OnEnable()
	{
		signalBus.Subscribe<UISignal>(this.OnUIStateChange);
	}

	private void OnUIStateChange(UISignal signal)
	{
		paused = signal.IsOpen;
	}

	private void OnDisable()
	{
		signalBus.Unsubscribe<UISignal>(this.OnUIStateChange);
	}

	private void OnMovement(InputValue value)
	{
		movement = paused ? new Vector2() : value.Get<Vector2>();

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
		return amount <= MoneyAmount;
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
