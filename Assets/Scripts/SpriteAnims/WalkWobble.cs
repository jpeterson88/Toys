using UnityEngine;

namespace Assets.Scripts.SpriteAnims
{
    class WalkWobble : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private Vector2 scaleTo;
        [SerializeField] private float resetTime, loopTime;
        private Vector2 startingScale;
        private bool isWobbling;

        private void Awake()
        {
            startingScale = spriteGameObject.transform.localScale;
        }

        public void StartWobble()
        {
            Debug.Log("Start Wobble");
            isWobbling = true;
            LeanTween.scale(spriteGameObject, scaleTo, loopTime)
                .setLoopPingPong();
        }

        public void StopWobble()
        {
            Debug.Log("Stop Wobble");
            //Cancel and scale back to starting
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, startingScale, resetTime);
            isWobbling = false;
        }

        public bool IsWobbling() => isWobbling;
    }
}