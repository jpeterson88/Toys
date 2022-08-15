using Assets.Scripts.Scriptables.Events;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    class CamShakeListener : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource camShaker;
        [SerializeField] private CamShakeEvent camShakeEvent;

        private void Start() => camShakeEvent.AddAction(HandleShake);

        private void HandleShake(float force) => camShaker.GenerateImpulseWithForce(force);

        private void OnDisable() => camShakeEvent.RemoveAction(HandleShake);
    }
}