using UnityEngine;

namespace Assets.Scripts.SpriteAnims
{
    class LungeWobble : MonoBehaviour
    {
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private float startDuration, lungeDuration, resetDuration;

        private Vector2 startingScale;

        private void Awake() => startingScale = spriteGameObject.transform.localScale;

        public void StartSquash()
        {
            LeanTween.scaleY(spriteGameObject, scaleTo.y, startDuration);
        }

        public void LungeForward()
        {
            LeanTween.scale(spriteGameObject, scaleTo, lungeDuration)
                .setLoopPingPong(1);
        }

        public void Reset()
        {
            LeanTween.scale(spriteGameObject, startingScale, resetDuration);
        }
    }
}