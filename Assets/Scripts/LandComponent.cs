using Assets.Scripts.SpriteAnims;
using UnityEngine;

namespace Assets.Scripts
{
    class LandComponent : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float scaleDuration;

        private ParticleSystem ps;

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            Player.OnLand += HandleLand;
        }

        private void OnDisable()
        {
            Player.OnLand -= HandleLand;
        }

        private void HandleLand()
        {
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, scaleTo, scaleDuration)
                .setEaseOutCubic()
                .setLoopPingPong(1);

            ps.Play();
        }
    }
}