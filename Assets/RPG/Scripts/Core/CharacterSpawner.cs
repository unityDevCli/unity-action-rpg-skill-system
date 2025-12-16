using System;
using UnityEngine;

namespace RPG.Scripts.Core
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject characterPrefab;
        [SerializeField] protected Transform spawnPoint;

        [SerializeField] private bool spawnOnStart = true;

        public Transform CurrentCharacter { get; private set; }

        private void Start()
        {
            if (spawnOnStart) SpawnCharacter();
        }

        public void SpawnCharacter()
        {
            if(characterPrefab == null || spawnPoint ==null) return;
            
            var obj = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
            CurrentCharacter = obj.transform;
        }
    }
}