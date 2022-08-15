using UnityEngine;

namespace Assets.Scripts.Interactables
{
    //Using trigger colliders that a player interacts with instead of just it's rigidbody.
    internal class ColliderToVelocity : MonoBehaviour
    {
        [SerializeField] private AudioSource sound;
        [SerializeField] private Vector2 forceOnCollision;
        [SerializeField] private Rigidbody2D rb2d;

        //Collides with all of null
        [SerializeField] private string collideWithTag;

        [SerializeField] private int layerNumber;
        [SerializeField] private float cooldownPerHit;
        [SerializeField] private int allowedHitCount;
        [SerializeField] [Range(0, 1)] private float stopObjectPercentage;

        private float currentTimer;
        private bool wasHit;
        private int currentHitCount;

        private void Update()
        {
            if (currentHitCount == 0 || allowedHitCount < currentHitCount)
            {
                if (wasHit)
                    currentTimer += Time.deltaTime;

                if (currentTimer >= cooldownPerHit)
                    wasHit = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (allowedHitCount > currentHitCount && (collision.CompareTag(collideWithTag) || string.IsNullOrWhiteSpace(collideWithTag)))
            {
                Debug.Log($"Collided with Tag: {collision.tag}. Compared with: {collideWithTag}");
                if (collision.gameObject.layer == layerNumber)
                {
                    currentHitCount++;
                    wasHit = true;

                    //set force in the direction of the colliding object
                    var forceDirection = new Vector2(Mathf.Sign(collision.attachedRigidbody.velocity.x) * forceOnCollision.x, forceOnCollision.y);

                    //Apply collision force to object
                    rb2d.AddForceAtPosition(forceDirection, collision.bounds.center, ForceMode2D.Impulse);

                    //Apply collision force to incoming object
                    collision.attachedRigidbody.AddForce(new Vector2(-(collision.attachedRigidbody.velocity.x * stopObjectPercentage), 0), ForceMode2D.Impulse);
                    sound?.Play();
                }
            }
        }

        public void ResetHitCount() => currentHitCount = 0;
    }
}