using RPG.Scripts.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Scripts.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class InputSystem : SystemBase
    {
        private InputAction _moveAction;

        protected override void OnCreate()
        {
            _moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick>");
            _moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            _moveAction.Enable();
        }

        protected override void OnUpdate()
        {
            var moveInput = _moveAction.ReadValue<Vector2>();
            var moveInputFloat2 = new float2(moveInput.x, moveInput.y);

            foreach (var playerInputData in SystemAPI.Query<RefRW<PlayerInputData>>())
            {
                playerInputData.ValueRW.MoveInput = moveInputFloat2;
            }
        }

        protected override void OnDestroy()
        {
            _moveAction.Disable();
            _moveAction.Dispose();
        }
    }
}