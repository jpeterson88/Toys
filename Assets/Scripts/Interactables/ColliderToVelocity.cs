using UnityEngine;

namespace Assets.Scripts.Interactables
{
    //Using trigger colliders that a player interacts with instead of just it's rigidbody.
    internal class ColliderToVelocity : MonoBehaviour
    {
        [SerializeField] private AudioSource sound;
        [SerializeField] private Vector2 forceOnCollision;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private int layerNumber;
        [SerializeField] private float cooldownPerHit;
        [SerializeField] private int allowedHitCount;

        private float currentTimer;
        private bool wasHit;
        private int hitCount;

        private void Update()
        {
            if (hitCount == 0 || allowedHitCount < hitCount)
            {
                if (wasHit)
                {
                    currentTimer += Time.deltaTime;
                }

                if (currentTimer >= cooldownPerHit)
                    wasHit = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hitCount == 0 || allowedHitCount < hitCount)
            {
                if (collision.gameObject.layer == layerNumber)
                {
                    hitCount++;
                    wasHit = true;
                    rb2d.AddForceAtPosition(forceOnCollision, collision.bounds.center, ForceMode2D.Impulse);
                    sound?.Play();
                }
            }
        }

        public void ResetHitCount() => hitCount = 0;
    }
}