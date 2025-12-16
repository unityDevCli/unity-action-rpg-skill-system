using RPG.Scripts.Skills;
using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPGDots.Scripts
{
    public class SkillInput : MonoBehaviour
    {
        public void CastSkill(Entity caster, float damage)
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var request = entityManager.CreateEntity();
            entityManager.AddComponentData(request, new SkillCastRequest
            {
                Caster = caster, Damage = damage
            });
        }
    }
}