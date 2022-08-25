using Assets.Scripts.SpriteAnims;
using Assets.Scripts.State;
using Assets.Scripts.Utility;
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

            if (Mathf.Abs(input.x) > 0 && !walkWobble.IsWobbling())
                walkWobble.StartWobble();
            else if (input.x == 0 && walkWobble.IsWobbling())
                walkWobble.StopWobble();

            if (rb2d.velocity.magnitude < maxForce)
                rb2d.AddForce(new Vector2(input.x, 0) * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
        }
    }
}