using UnityEngine;

namespace Assets.Scripts.Prototype
{
    class PlayerCollide : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TopDownPlayer topDownPlayer;
        [SerializeField] private ParticleSystem ps;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                var enemy = collision.GetComponent<Enemy>();
                if (topDownPlayer.isLunging)
                {
                    enemy.TakeHit();
                    audioSource?.Play();
                    ps.Play();
                    topDownPlayer.PlayScreech();
                }
                //If not attacking
                else if (topDownPlayer.canBeHurt)
                {
                    topDownPlayer.Hurt();
                    enemy.DoDamage();
                }
            }
        }
    }
}