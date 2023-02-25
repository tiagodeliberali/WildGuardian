using Assets.Signals;

using UnityEngine;
using UnityEngine.InputSystem;

using Zenject;

namespace Assets.Character
{
    /// <summary>
    /// Only deals with movements and animations related to that
    /// Pauses movement on UI open
    /// </summary>
    public class CharacterMovement : MonoBehaviour
    {
        private SignalBus signalBus;

        // Movement
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
            signalBus.Subscribe<UISignal>(this.OnUIStateChange);
        }

        private void OnUIStateChange(UISignal signal)
        {
            paused = signal.IsOpen;
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
    }
}