using UnityEngine;

namespace Assets.Scripts
{
    class GroundedDetector : MonoBehaviour
    {
        [SerializeField]
        private float distance;

        [SerializeField]
        private LayerMask detectableLayers;

        private bool isGrounded;

        public bool IsGrounded() => isGrounded;

        private void Update()
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, distance, detectableLayers);

            isGrounded = hit ? hit.transform : null;

            Color castColor = Physics2D.Raycast(transform.position, Vector2.down, distance, detectableLayers) ? Color.red : Color.green;

            Debug.DrawRay(transform.position, new Vector2(0, distance), castColor);
        }
    }
}