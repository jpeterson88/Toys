using Assets.Scripts.Input;
using Assets.Scripts.SpriteAnims;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownHopComponent : MonoBehaviour
    {
        [SerializeField] private float launchSpeed;
        [SerializeField] private Transform leftDir, rightDir;

        private JumpSquashWobble jumpSquashWobble;
        private Rigidbody2D rb2d;
        private Vector2 hopDirection;
        private bool hasLaunched;

        private void Awake()
        {
            rb2d = GetComponentInParent<Rigidbody2D>();
            jumpSquashWobble = GetComponent<JumpSquashWobble>();
        }

        public void InitiateJump()
        {
            hasLaunched = false;
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
            // Enable jump collider
            hasLaunched = true;
            Debug.Log($"Launch Dir: {hopDirection}");
            rb2d.AddForce(hopDirection * launchSpeed, ForceMode2D.Impulse);
        }

        public void SetLaunchDir(Vector2 moveVector)
        {
            hopDirection = moveVector;

            //if (moveVector.y < 0)
            //    hopDirection = -transform.root.up;
            //else if (moveVector.x < 0)
            //    hopDirection = leftDir.up;
            //else if (moveVector.x > 0)
            //    hopDirection = rightDir.up;
        }

        public bool HasLaunched() => hasLaunched;

        public void Reset() => hopDirection = Vector2.zero;

        public Vector2 GetNextHopDirection() => hopDirection;
    }
}