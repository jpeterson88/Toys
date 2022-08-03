using UnityEngine;

namespace Assets.Scripts.Utility
{
    class BoxCaster : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layer;

        private bool isHit;
        RaycastHit2D hit;

        private void Update()
        {
            hit = Physics2D.BoxCast(transform.position, transform.lossyScale / 1.5f, 0f, Vector2.down, distance, layer);

            isHit = hit.transform != null;
        }

        public bool IsHit() => isHit;

        void OnDrawGizmos()
        {
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, Vector2.down * hit.distance);
                Gizmos.DrawWireCube(transform.position + Vector3.down * hit.distance, transform.lossyScale);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, Vector3.down * distance);
            }
        }
    }
}