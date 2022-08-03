using Assets.Scripts.SpriteAnims;
using UnityEngine;

namespace Assets.Scripts
{
    class LandComponent : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float scaleDuration;
        [SerializeField] private AudioSource audioSource;

        private ParticleSystem ps;

        private void Awake() => ps = GetComponent<ParticleSystem>();

        public void HandleLand()
        {
            audioSource?.Play();
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, scaleTo, scaleDuration)
                .setEaseOutCubic()
                .setLoopPingPong(1);

            ps.Play();
        }
    }
}