using Assets.Scripts.Utility;
using System;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class LedgeComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private Vector2 pullupSpeed;
        [SerializeField] private RaycastCheck groundCheck;

        private bool isPullingUp;
        private float initialGravity;

        public void Initiate()
        {
            initialGravity = rb2d.gravityScale;
            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = 0f;
        }

        public void BeginPullup()
        {
            isPullingUp = true;
            rb2d.velocity = pullupSpeed;
            Debug.Log("BeginPullup");
        }

        internal bool IsFinished()
        {
            groundCheck.Check();
            return groundCheck.IsHit();
        }

        public bool IsPullingUp() => isPullingUp;

        public void Reset()
        {
            rb2d.gravityScale = initialGravity;
            isPullingUp = false;
        }
    }
}