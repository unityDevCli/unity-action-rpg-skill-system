using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

public class PlayerInputBridge : MonoBehaviour
{
    private EntityManager _em;
    private EntityQuery _playerQuery;

    private void Awake()
    {
        _em = World.DefaultGameObjectInjectionWorld.EntityManager;
        _playerQuery = _em.CreateEntityQuery(typeof(CharacterTag), typeof(PlayerInput));
    }

    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");

        var input = new PlayerInput
        {
            Move = new Unity.Mathematics.float2(x, z)
        };

        using var players = _playerQuery.ToEntityArray(Unity.Collections.Allocator.Temp);
        for (var i = 0; i < players.Length; i++)
            _em.SetComponentData(players[i], input);
    }
}