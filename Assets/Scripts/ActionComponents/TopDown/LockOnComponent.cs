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
        [SerializeField] private float rotationSpeed;

        private Transform currentLockedTarget;

        private void Start() => playerInputMap.LockOnPressed += HandleLockPressed;

        private void HandleLockPressed()
        {
            //var detected = detectedObjects.Get();
            //var ordered = detected.OrderBy(x => Vector2.Distance(transform.position, x.position)).ToList();
            var firstDetected = detectedObjects.Get().FirstOrDefault();
            if (firstDetected != null && (currentLockedTarget == null || currentLockedTarget != firstDetected))
            {
                currentLockedTarget = firstDetected;
            }
            //Currently doesn't handle multiple detected targets
            else if (currentLockedTarget != null)
            {
                ClearLockOn();
            }
        }

        public void Update()
        {
            var lockedTarget = GetLockedTarget();

            if (lockedTarget != null)
            {
                Vector3 vectorToTarget = lockedTarget.position - transform.root.position;

                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
                transform.root.rotation = Quaternion.RotateTowards(transform.root.rotation, toRotation, Time.deltaTime * rotationSpeed);
            }
        }

        public void ClearLockOn() => currentLockedTarget = null;

        public Transform GetLockedTarget() => currentLockedTarget;
    }
}