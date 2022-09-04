using Assets.Scripts.Input;
using Assets.Scripts.SpriteAnims;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class JumpComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 leftRightForce, verticalForce;
        [SerializeField] private CircleCollider2D environmentInteractor;

        private JumpSquashWobble jumpSquashWobble;
        private Rigidbody2D rb2d;

        private IInput input;
        private bool hasLaunched;

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
            hasLaunched = false;
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
            hasLaunched = true;

            if (Mathf.Abs(moveVector.x) > 0)
            {
                Vector2 appliedForce = new Vector2(leftRightForce.x * Mathf.Sign(moveVector.x), leftRightForce.y);
                rb2d.AddForce(appliedForce, ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(verticalForce, ForceMode2D.Impulse);
            }
        }

        public bool HasLaunched() => hasLaunched;
    }
}