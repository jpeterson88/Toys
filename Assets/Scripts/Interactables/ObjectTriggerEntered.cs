using Assets.Scripts.Scriptables.Lists;
using UnityEngine;

namespace Assets.Scripts.Interactables
{
    public class ObjectTriggerEntered : MonoBehaviour
    {
        [SerializeField] private TransformListVariable detectedObjects;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!detectedObjects.Get().Contains(collision.gameObject.transform))
                detectedObjects.Add(collision.gameObject.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (detectedObjects.Get().Contains(collision.gameObject.transform))
                detectedObjects.Remove(collision.gameObject.transform);
        }
    }
}