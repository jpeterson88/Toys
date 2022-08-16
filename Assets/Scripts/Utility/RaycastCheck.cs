using UnityEngine;

namespace Assets.Scripts.Utility
{
    class RaycastCheck : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layer;
        [SerializeField] private Vector2 direction;

        private bool isHit;
        private RaycastHit2D hit;

        public void Check()
        {
            hit = Physics2D.Raycast(transform.position, direction, distance, layer);

            isHit = hit.transform != null;
        }

        public bool IsHit() => isHit;

        void OnDrawGizmos()
        {
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, direction * hit.distance);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, direction * distance);
            }
        }
    }
}