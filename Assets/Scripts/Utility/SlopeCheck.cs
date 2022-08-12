using UnityEngine;

namespace Assets.Scripts.Utility
{
    class SlopeCheck : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D cc;
        [SerializeField] private float slopeCheckDistance;
        [SerializeField] private float maxSlopeAngle;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private PhysicsMaterial2D noFriction;
        [SerializeField] private PhysicsMaterial2D fullFriction;
        [SerializeField] private GroundedDetector groundedDetector;
        [SerializeField] private Rigidbody2D rb2d;

        private float slopeDownAngle;
        private float slopeSideAngle;
        private float lastSlopeAngle;

        private bool isOnSlope;
        private bool canWalkOnSlope;
        private Vector2 slopeNormalPerp;
        private Vector2 capsuleColliderSize;

        private void Start()
        {
            capsuleColliderSize = cc.bounds.size;
        }

        public void Detect(Vector2 moveVector)
        {
            Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, capsuleColliderSize.y / 2));

            SlopeCheckHorizontal(checkPos);
            SlopeCheckVertical(checkPos, moveVector);
        }

        private void SlopeCheckHorizontal(Vector2 checkPos)
        {
            RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
            RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

            if (slopeHitFront)
            {
                isOnSlope = true;

                slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            }
            else if (slopeHitBack)
            {
                isOnSlope = true;

                slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
            }
            else
            {
                slopeSideAngle = 0.0f;
                isOnSlope = false;
            }
        }

        private void SlopeCheckVertical(Vector2 checkPos, Vector2 moveVector)
        {
            RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

            if (hit)
            {
                slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

                slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (slopeDownAngle != lastSlopeAngle)
                {
                    isOnSlope = true;
                }

                lastSlopeAngle = slopeDownAngle;

                Debug.DrawRay(hit.point, slopeNormalPerp, Color.blue);
                Debug.DrawRay(hit.point, hit.normal, Color.green);
            }

            if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
                canWalkOnSlope = false;
            else
                canWalkOnSlope = true;

            if ((isOnSlope && canWalkOnSlope && moveVector.x == 0.0f) || !isOnSlope)
                rb2d.sharedMaterial = fullFriction;
            else
                rb2d.sharedMaterial = noFriction;
        }

        public Vector2 GetSlopeNormal() => slopeNormalPerp;

        public bool IsOnSlope() => isOnSlope;

        public bool CanWalkOnSlope() => canWalkOnSlope;
    }
}