using UnityEngine;

namespace Assets.Scripts.SpriteAnims
{
    class WalkWobble : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float resetTime, loopTime;
        private Vector2 startingScale;
        private bool isWobble;

        private void Awake()
        {
            startingScale = spriteGameObject.transform.localScale;
        }

        public void StartWobble()
        {
            isWobble = true;
            LeanTween.scale(spriteGameObject, scaleTo, loopTime)
                .setLoopPingPong();
        }

        public void StopWobble()
        {
            //Cancel and scale back to starting
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, startingScale, resetTime);
            isWobble = false;
        }

        public bool IsWobble() => isWobble;
    }
}