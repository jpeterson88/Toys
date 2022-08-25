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
        private bool isHurt, canTakeDamage;

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
            rb2d.AddForce(-transform.up * hurtKnockbackSpeed, ForceMode2D.Force);
            yield return new WaitForSeconds(hurtDuration);
            isHurt = false;
            yield return new WaitForSeconds(hurtCd);
            canTakeDamage = true;
        }
    }
}