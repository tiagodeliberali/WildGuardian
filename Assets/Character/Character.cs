using Assets;

using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
	private Vector2 movement;
	private Rigidbody2D characterRigidBody;
	private Animator animator;

	private void Awake()
	{
		characterRigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
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
}
