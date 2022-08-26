using Assets.Scripts.State;
using Assets.Scripts.Utility;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    class TopDownHurtComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float hurtKnockbackSpeed;
        [SerializeField] private float hurtDuration;
        [SerializeField] private float hurtCd;
        [SerializeField] private PlayRandomFromArray randomAudio;
        [SerializeField] private PlayerStateMachine stateMachine;
        [SerializeField] private bool enableLogs, breakOnContact;

        private bool isHurt, canTakeDamage = true;
        private Vector2 recentContactNormal;

        public void SetRecentContactNormal(Vector2 contactNormal)
        {
            recentContactNormal = contactNormal;
            stateMachine.SetState((int)PlayerStates.Hurt);
        }

        public void TakeDamage()
        {
            isHurt = true;
            canTakeDamage = false;
            randomAudio.PlayRandom();
            StartCoroutine(HurtCd());
        }

        public bool IsHurt() => isHurt;

        public bool CanTakeDamage() => canTakeDamage;

        public IEnumerator HurtCd()
        {
            if (enableLogs)
            {
                Debug.Log($"Contact normal: {recentContactNormal}");
                Debug.DrawRay(transform.root.position, recentContactNormal, Color.green);
            }

            if (breakOnContact)
                Debug.Break();

            rb2d.AddForce(recentContactNormal * hurtKnockbackSpeed, ForceMode2D.Force);
            yield return new WaitForSeconds(hurtDuration);
            isHurt = false;
            yield return new WaitForSeconds(hurtCd);
            canTakeDamage = true;
        }
    }
}