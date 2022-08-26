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

                    if (playerState.GetCurrentState() == (int)PlayerStates.Attack2)
                    {
                        var enemy = collision.transform.GetComponentInChildren<TopDownHurtComponent>();

                        //Normal needs to be reversed because the player is the object which detects the contact point
                        //This would be different if the enemy detected the contact
                        enemy.SetRecentContactNormal(-collision.contacts[0].normal);
                        playerState.SetState((int)PlayerStates.Idle);
                    }
                    else if (hurtComponent.CanTakeDamage())
                    {
                        var enemySm = collision.transform.GetComponentInChildren<PlayerStateMachine>();
                        enemySm.SetState((int)PlayerStates.Idle);
                        hurtComponent.SetRecentContactNormal(collision.contacts[0].normal);
                        playerState.SetState((int)PlayerStates.Hurt);
                    }
                }
            }
        }
    }
}