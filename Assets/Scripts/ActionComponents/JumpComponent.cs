using Assets.Scripts.Input;
using Assets.Scripts.SpriteAnims;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class JumpComponent : MonoBehaviour
    {
        [SerializeField] private FacingDirection facingDirection;
        [SerializeField] private Vector2 force;
        [SerializeField] private CircleCollider2D environmentInteractor;

        private JumpSquashWobble jumpSquashWobble;
        private Rigidbody2D rb2d;

        private IInput input;

        private void Awake()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            jumpSquashWobble = GetComponent<JumpSquashWobble>();

            input = transform.root.GetComponent<IInput>();
            if (input == null)
                Debug.LogError("failed to find input for Locomotion handler");
        }

        public void InitiateJump()
        {
            jumpSquashWobble.InitializeSquash();

            StartCoroutine(LaunchCountdown());
        }

        private IEnumerator LaunchCountdown()
        {
            Vector2 moveVector = input.GetMoveVector();
            yield return new WaitForSeconds(jumpSquashWobble.squashDuration);
            Launch(moveVector);
        }

        private void Launch(Vector2 moveVector)
        {
            // Enable jump collider
            environmentInteractor.enabled = true;

            if (Mathf.Abs(moveVector.x) > 0)
            {
                //Vector2 directionFacing = facingDirection.GetFacingDirection();
                Vector2 appliedForce = moveVector == Vector2.left ? new Vector2(-force.x, force.y) : new Vector2(force.x, force.y);
                rb2d.AddForce(appliedForce, ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(new Vector2(0, force.y), ForceMode2D.Impulse);
            }
        }
    }
}