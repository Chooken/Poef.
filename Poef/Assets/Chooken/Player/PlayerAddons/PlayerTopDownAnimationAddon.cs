using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aarthificial.Reanimation;
using UnityEngine.InputSystem;

public class PlayerTopDownAnimationAddon : PlayerAddon
{
    private static class Drivers
    {
        public const string Walk = "walk";
        public const string MoveDir = "moveDir";
        public const string IsMoving = "isMoving";
        public const string StepEvent = "stepEvent";
    }

    private Reanimator m_reanimator;

    private void Awake()
    {
        m_reanimator = GetComponentInChildren<Reanimator>();
    }

    private void OnEnable()
    {
        m_player.SetPerformedCallback(m_player.Controls.Gameplay.Move, OnMovementChange);
    }

    private void OnDisable()
    {
        m_player.UnsetPerformedCallback(m_player.Controls.Gameplay.Move, OnMovementChange);
    }

    private void Update()
    {
        
    }

    private void OnMovementChange(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() == Vector2.zero)
        {
            m_reanimator.Set(Drivers.Walk, 0);
            m_reanimator.Set(Drivers.IsMoving, 0);
            return;
        }

        m_reanimator.Set(Drivers.IsMoving, 1);
        m_reanimator.Set(Drivers.MoveDir, MoveDirectionToAniNum(context.ReadValue<Vector2>()));
    }

    private int MoveDirectionToAniNum(Vector2 moveDir)
    {
        m_reanimator.Flip = false;

        if (Mathf.Abs(moveDir.x) >= Mathf.Abs(moveDir.y))
        {
            if (moveDir.x > 0) m_reanimator.Flip = true;
                return 0;
        }

        if (moveDir.y < 0) return 1;
        else return 2;
    }
}
