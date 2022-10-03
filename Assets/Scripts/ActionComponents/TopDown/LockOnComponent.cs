using Assets.Scripts.Input;
using Assets.Scripts.Scriptables.Lists;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.ActionComponents.TopDown
{
    public class LockOnComponent : MonoBehaviour
    {
        [SerializeField] private TransformListVariable detectedObjects;
        [SerializeField] private PlayerInputMap playerInputMap;

        private Transform currentLockedTarget;

        private void Start()
        {
            playerInputMap.LockOnPressed += HandleLockPressed;
        }

        private void HandleLockPressed()
        {
            //var detected = detectedObjects.Get();
            //var ordered = detected.OrderBy(x => Vector2.Distance(transform.position, x.position)).ToList();
            var firstDetected = detectedObjects.Get().FirstOrDefault();
            if (firstDetected != null && (currentLockedTarget == null || currentLockedTarget != firstDetected))
            {
                Debug.Log("Set Target");
                currentLockedTarget = firstDetected;
            }
            else if (currentLockedTarget != null && firstDetected == currentLockedTarget)
            {
                ClearLockOn();
                Debug.Log("Clear Lock");
            }
        }

        public void ClearLockOn() => currentLockedTarget = null;

        public Transform GetLockedTarget() => currentLockedTarget;
    }
}