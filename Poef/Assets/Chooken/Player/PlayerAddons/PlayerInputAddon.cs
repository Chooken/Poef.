using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputAddon : PlayerAddon
{
    private GameControls m_controls;

    private void Awake()
    {
        m_controls = new GameControls();

        AddPressAndRelease(m_controls.Gameplay.Move, Move);
    }

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }

    private void AddPressAndRelease(InputAction input, Action<InputAction.CallbackContext> action)
    {
        input.started += action;
        input.canceled += action;
    }

    public void Move(InputAction.CallbackContext context)
    {
        //m_player.FrameInput.movement.Invoke(context.ReadValue<Vector2>());
    }
}
