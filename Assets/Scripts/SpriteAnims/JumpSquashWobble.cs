using UnityEngine;

namespace Assets.Scripts.SpriteAnims
{
    public class JumpSquashWobble : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] public float squashDuration, wobbleDuration, resetDuration;
        [SerializeField] private float squashAdjustment, wobbleAdjustment;
        [SerializeField] private int wobbleCount;

        private Vector2 startingScale;

        private void Awake()
        {
            startingScale = spriteGameObject.transform.localScale;
        }

        public void InitializeSquash()
        {
            LeanTween.cancel(spriteGameObject);
            LeanTween.scale(spriteGameObject, new Vector2(startingScale.x + squashAdjustment, startingScale.y - squashAdjustment), squashDuration)
                .setEaseInCubic()
                .setOnComplete(Wobble);
        }

        public void Wobble()
        {
            LeanTween.scale(spriteGameObject, new Vector2(startingScale.x - wobbleAdjustment, startingScale.y + wobbleAdjustment), wobbleDuration)
                .setEaseInOutCubic()
                .setLoopPingPong(wobbleCount)
                .setOnComplete(() =>
                {
                    LeanTween.scale(spriteGameObject, startingScale, resetDuration)
                        .setEaseInCubic();
                });
        }
    }
}