using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Scriptables.Lists
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Lists/TransformListVariable")]
    class TransformListVariable : ListVariable<Transform>
    {
        private void Awake() => Value = new List<Transform>();
    }
}