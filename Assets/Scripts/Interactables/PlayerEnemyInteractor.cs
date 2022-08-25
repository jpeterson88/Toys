using Assets.Scripts.ActionComponents.TopDown;
using Assets.Scripts.Prototype;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (string tag in enemyTags)
            {
                if (collision.transform.root.CompareTag(tag))
                {
                    bodyHitSfx?.Play();
                    var enemy = collision.GetComponent<Enemy>();
                    if (playerState.GetCurrentState() == (int)PlayerStates.Attack2)
                    {
                        enemy.TakeHit();
                    }
                    else if (hurtComponent.CanTakeDamage())
                    {
                        playerState.SetState((int)PlayerStates.Hurt);
                    }
                }
            }
        }
    }
}