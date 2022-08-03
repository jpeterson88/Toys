using Assets.Scripts.SpriteAnims;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class ClimbComponent : MonoBehaviour
    {
        [SerializeField] private WalkWobble walkWobble;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float climbSpeed;
        private float startingGravity;

        private void Start() => startingGravity = rb2d.gravityScale;

        public void Initiate() => rb2d.gravityScale = 0f;

        public void Climb(Vector2 climbVector)
        {
            float inputSpeedY = Mathf.Abs(climbVector.y) > 0 ? 1 : 0;
            float inputSpeedX = Mathf.Abs(climbVector.x) > 0 ? 1 : 0;

            rb2d.velocity = new Vector2(inputSpeedX * Mathf.Sign(climbVector.x) * climbSpeed * Time.fixedDeltaTime,
                inputSpeedY * Mathf.Sign(climbVector.y) * climbSpeed * Time.fixedDeltaTime);

            if (Mathf.Abs(climbVector.y) > 0 && !walkWobble.IsWobbling())
                walkWobble.StartWobble();
            else if (walkWobble.IsWobbling())
                walkWobble.StopWobble();
        }

        public void Reset() => rb2d.gravityScale = startingGravity;
    }
}