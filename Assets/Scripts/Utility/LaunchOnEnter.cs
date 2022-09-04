using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    class LaunchOnEnter : MonoBehaviour
    {
        [SerializeField] private List<string> launchTags;
        [SerializeField] private float rbLaunchMultiplier;
        [SerializeField] private float launchSpeed1, launchSpeed2;
        [SerializeField] private float rb2dMagnitudeThreshold;

        public Action OnTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (launchTags.Contains(collision.tag))
            {
                var rb2d = collision.GetComponent<Rigidbody2D>();

                if (rb2d != null)
                {
                    OnTrigger?.Invoke();
                    Debug.Log(rb2d.velocity.y);
                    rb2d.velocity = Mathf.Abs(rb2d.velocity.y) >= rb2dMagnitudeThreshold ? new Vector2(rb2d.velocity.x, launchSpeed2) : new Vector2(rb2d.velocity.x, launchSpeed1);
                }
            }
        }
    }
}