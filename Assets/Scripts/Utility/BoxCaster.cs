﻿using UnityEngine;

namespace Assets.Scripts.Utility
{
    class BoxCaster : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private LayerMask layer;
        [SerializeField] private Vector2 direction;
        private bool isHit;
        RaycastHit2D hit;

        private void Update()
        {
            hit = Physics2D.BoxCast(transform.position, transform.lossyScale / 1.5f, 0f, direction, distance, layer);

            isHit = hit.transform != null;
        }

        public bool IsHit() => isHit;

        void OnDrawGizmos()
        {
            if (isHit)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position, direction * hit.distance);
                Gizmos.DrawWireCube(transform.position + (Vector3)direction * hit.distance, transform.lossyScale);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, direction * distance);
            }
        }
    }
}