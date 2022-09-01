using Assets.Scripts.SpriteAnims;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class MovementComponentv2 : MonoBehaviour
    {
        [SerializeField] private float movementSpeed, sprintSpeed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SlopeCheck slopeCheck;
        [SerializeField] private WalkWobble walkWobble;

        private Vector2 newVelocity;

        public void ApplyMovement(Vector2 moveVector, bool isSprinting)
        {
            float speed = isSprinting ? sprintSpeed : movementSpeed;
            if (!slopeCheck.IsOnSlope())
            {
                newVelocity.Set(speed * moveVector.x, 0.0f);
                rb.velocity = newVelocity;
            }
            else if (slopeCheck.IsOnSlope() && slopeCheck.CanWalkOnSlope())
            {
                Vector2 slopeNormal = slopeCheck.GetSlopeNormal();
                newVelocity.Set(speed * slopeNormal.x * -moveVector.x, speed * slopeNormal.y * -moveVector.x);
                rb.velocity = newVelocity;
            }

            if (Mathf.Abs(moveVector.x) > 0 && !walkWobble.IsWobbling())
                walkWobble.StartWobble();
            else if (moveVector.x == 0 && walkWobble.IsWobbling())
                walkWobble.StopWobble();
        }
    }
}