using UnityEngine;

namespace Assets.Scripts
{
    class TrashItem : MonoBehaviour
    {
        [SerializeField] private AudioSource soundOnHitFloor;
        [SerializeField] private int layerHit;

        private bool hasHit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!hasHit && collision.gameObject.layer == layerHit)
            {
                hasHit = true;
                soundOnHitFloor?.Play();
            }
        }
    }
}