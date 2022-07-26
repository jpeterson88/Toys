using Assets.Scripts.SpriteAnims;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class JumpComponent : MonoBehaviour
    {
        [SerializeField] private FacingDirection facingDirection;
        [SerializeField] private Vector2 force;
        [SerializeField] private CircleCollider2D collider2D;

        private JumpSquashWobble jumpSquashWobble;
        private Rigidbody2D rb2d;

        private void Awake()
        {
            //TODO: This should't be a direct binding;
            Player.OnLand += HandleLand;
            rb2d = GetComponentInParent<Rigidbody2D>();
            jumpSquashWobble = GetComponent<JumpSquashWobble>();
        }

        private void HandleLand()
        {
            collider2D.enabled = false;
        }

        public void InitiateJump()
        {
            jumpSquashWobble.InitializeSquash();
            StartCoroutine(LaunchCountdown());
        }

        private IEnumerator LaunchCountdown()
        {
            yield return new WaitForSeconds(jumpSquashWobble.squashDuration);
            Launch();
        }

        private void Launch()
        {
            Vector2 directionFacing = facingDirection.GetFacingDirection();
            Vector2 appliedForce = directionFacing == Vector2.left ? new Vector2(-force.x, force.y) : new Vector2(force.x, force.y);
            rb2d.AddForce(appliedForce, ForceMode2D.Impulse);

            // Enable jump collider
            collider2D.enabled = true;
        }
    }
}