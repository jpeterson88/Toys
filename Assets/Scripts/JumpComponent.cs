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

        private JumpSquashWobble jumpSquashWobble;
        private Rigidbody2D rb2d;
        private Player player;

        private void Awake()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            player = GetComponentInParent<Player>();
            jumpSquashWobble = GetComponent<JumpSquashWobble>();
        }

        public void InitiateJump()
        {
            Debug.Log("Called");
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
        }
    }
}