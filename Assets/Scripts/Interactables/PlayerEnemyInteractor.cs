using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.State;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    class PlayerEnemyInteractor : MonoBehaviour
    {
        [SerializeField] private string[] enemyTags;
        [SerializeField] private PlayerStateMachine playerState;
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private AudioSource bodyHitSfx;
        [SerializeField] private TopDownHurtComponent hurtComponent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (string tag in enemyTags)
            {
                if (collision.transform.root.CompareTag(tag))
                {
                    bodyHitSfx?.Play();
                    var enemy = collision.transform.GetComponentInChildren<TopDownHurtComponent>();
                    if (playerState.GetCurrentState() == (int)PlayerStates.Attack2)
                    {
                        enemy.SetRecentContactNormal(collision.contacts[0].normal);
                    }
                    else if (hurtComponent.CanTakeDamage())
                    {
                        //enemy.DoDamage();
                        hurtComponent.SetRecentContactNormal(collision.contacts[0].normal);
                        playerState.SetState((int)PlayerStates.Hurt);
                    }
                }
            }
        }
    }
}