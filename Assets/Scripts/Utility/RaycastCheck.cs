using UnityEngine;

namespace Assets.Scripts.Utility
{
    class RaycastCheck : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layer;
        [SerializeField] private bool isForwardVector;
        [SerializeField] private Vector2 direction;
        [SerializeField] private bool debug;

        private bool isHit;

        public void Check()
        {
            RaycastHit2D hit = Scan();

            isHit = hit.transform != null;
        }

        public bool IsHit() => isHit;

        void OnDrawGizmos()
        {
            if (debug)
            {
                RaycastHit2D hit = Scan();
                Vector2 rayDirection = isForwardVector ? transform.up : direction;

                if (hit.transform != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(transform.position, rayDirection * hit.distance);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(transform.position, rayDirection * distance);
                }
            }
        }

        private RaycastHit2D Scan()
        {
            RaycastHit2D hit;

            if (isForwardVector)
                hit = Physics2D.Raycast(transform.position, transform.up, distance, layer);
            else
                hit = Physics2D.Raycast(transform.position, direction, distance, layer);

            return hit;
        }
    }
}