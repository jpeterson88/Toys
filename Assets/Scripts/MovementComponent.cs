using Assets.Scripts.SpriteAnims;
using Assets.Scripts.State;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private float moveSpeed, maxForce;

        private WalkWobble walkWobble;
        private Rigidbody2D rb2d;
        private Player player;

        private void Awake()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            walkWobble = GetComponent<WalkWobble>();
            player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            PlayerStates state = player.GetState();

            if (state == PlayerStates.Idle && walkWobble.IsWobble())
                walkWobble.StopWobble();
        }

        public void Move(Vector2 input)
        {
            if (!walkWobble.IsWobble())
                walkWobble.StartWobble();

            if (rb2d.velocity.magnitude < maxForce)
                rb2d.AddForce(input * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
        }
    }
}