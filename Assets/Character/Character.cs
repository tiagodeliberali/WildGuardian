using Assets;
using Assets.MessageSystem;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour, IMessageSubscriber
{
	public static Character Instance;

	public int MoneyAmount = 300;
	public TextMeshProUGUI moneyAmountUI;

	public MessageManager messageManager;

	private Vector2 movement;
	private Rigidbody2D characterRigidBody;
	private Animator animator;

	private bool paused = false;

	private void Awake()
	{
		Instance = this;
		
		characterRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		SetMoneyAmount();
	}

	private void OnEnable()
	{
		messageManager.Subscribe(this);
	}

	private void OnDisable()
	{
		messageManager.Unsubscribe(this);
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

	public void MessageReceived(Message message)
	{
		switch (message.type)
		{
			case MessageType.UIWindowOpened:
				paused = true;
				break;
			case MessageType.UIWindowClosed:
				paused = false;
				break;
		}
	}
}
