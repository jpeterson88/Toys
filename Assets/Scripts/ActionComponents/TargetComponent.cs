using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.ActionComponents
{
    class TargetComponent : MonoBehaviour
    {
        private Transform target;

        public Transform GetTarget() => target;

        private void Awake()
        {
            //TODO: May need to refactor for finding target more dynamically.
            var gObject = FindObjectOfType<PlayerInputMap>();

            if (gObject == null)
                Debug.Log($"Unable to find player for object {transform.root.name}");
            else
                target = gObject.transform;
        }
    }
}