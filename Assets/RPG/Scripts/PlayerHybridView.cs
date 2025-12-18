using System;
using RPG.Scripts.ComponentData;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace RPG.Scripts
{
    public class PlayerHybridView : MonoBehaviour
    {
        public string speedParams = "Speed";
        public float damp = 0.1f;
        private EntityManager _entityManager;
        private EntityQuery _entityQuery;
        private Entity _playerEntity;

        private void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _entityQuery = _entityManager.CreateEntityQuery(ComponentType.ReadOnly<PlayerTag>(),
                ComponentType.ReadOnly<LocalTransform>());
        }

        private void LateUpdate()
        {
            if (_playerEntity == Entity.Null || !_entityManager.Exists(_playerEntity))
            {
                if (_entityQuery.IsEmptyIgnoreFilter) return;
                if (_entityQuery.CalculateEntityCount() != 1) return;
                _playerEntity = _entityQuery.GetSingletonEntity();
            }

            var lt = _entityManager.GetComponentData<LocalTransform>(_playerEntity);

            transform.SetPositionAndRotation(lt.Position, lt.Rotation);
        }
    }
}