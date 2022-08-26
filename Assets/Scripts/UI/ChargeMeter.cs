using Assets.Scripts.Scriptables.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class ChargeMeter : MonoBehaviour
    {
        [SerializeField] private ChargeMeterEvent onChargeReady, onChargeUsed;
        [SerializeField] private Image chargeMeter;
        [SerializeField] private AudioSource chargeReady;

        private void Start()
        {
            onChargeReady.AddAction(HandleChargeReady);
            onChargeUsed.AddAction(HandleChargeUsed);
        }

        private void OnDisable()
        {
            onChargeReady.RemoveAction(HandleChargeReady);
            onChargeUsed.RemoveAction(HandleChargeUsed);
        }

        private void HandleChargeUsed()
        {
            chargeMeter.color = new Color(chargeMeter.color.r, chargeMeter.color.g, chargeMeter.color.b, .3f);
        }

        private void HandleChargeReady()
        {
            chargeReady?.Play();
            chargeMeter.color = new Color(chargeMeter.color.r, chargeMeter.color.g, chargeMeter.color.b, 1);
        }
    }
}