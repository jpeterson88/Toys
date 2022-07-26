using Assets.Scripts.SpriteAnims;
using Assets.Scripts.State;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private float moveSpeed, maxForce;

        [SerializeField] private PlayerStateMachine stateMachine;

        private WalkWobble walkWobble;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            walkWobble = GetComponent<WalkWobble>();
        }

        //TODO: Remove and move to animation stuffs
        private void Update()
        {
            var state = stateMachine.GetCurrentState();

            if ((PlayerStates)state == PlayerStates.Idle && walkWobble.IsWobbling())
                walkWobble.StopWobble();
        }

        public void Move(Vector2 input)
        {
            //TODO: Move to animation stuffs
            if (!walkWobble.IsWobbling())
                walkWobble.StartWobble();

            Debug.Log($"Is wobbling ${walkWobble.IsWobbling()}");

            if (rb2d.velocity.magnitude < maxForce)
                rb2d.AddForce(input * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
        }
    }
}