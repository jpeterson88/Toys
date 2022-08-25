using Assets.Scripts.Interactables;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrashCan : MonoBehaviour
    {
        [SerializeField] private GameObject[] possibleSpawnItems;
        [SerializeField] private int minSpawnAmount;
        [SerializeField] private int maxSpawnAmount;
        [SerializeField] private Transform spawnParent, localSpawnPoint;
        [SerializeField] private ColliderToVelocity colliderToVelocity;

        private Vector2 startingPosition;

        private List<GameObject> spawnedObjects = new List<GameObject>();

        private void Start()
        {
            startingPosition = gameObject.transform.position;
            SpawnItems();
        }

        [ContextMenu("Reset")]
        public void ResetStance()
        {
            foreach (var obj in spawnedObjects)
                Destroy(obj);

            spawnedObjects.Clear();
            gameObject.transform.position = startingPosition;
            gameObject.transform.rotation = Quaternion.identity;
            SpawnItems();
            colliderToVelocity.ResetHitCount();
        }

        private void SpawnItems()
        {
            int spawnCount = Random.Range(minSpawnAmount, maxSpawnAmount + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                int itemIndex = Random.Range(0, possibleSpawnItems.Length);

                var trashObject = Instantiate(possibleSpawnItems[itemIndex], localSpawnPoint.position, Quaternion.identity);
                trashObject.transform.parent = spawnParent;

                spawnedObjects.Add(trashObject);
            }
        }
    }
}