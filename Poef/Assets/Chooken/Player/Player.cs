using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Initialisation

    private Dictionary<Type, PlayerAddon> m_addons = new Dictionary<Type, PlayerAddon>();
    public GameControls m_controls;

    void Awake()
    {
        foreach (PlayerAddon addon in GetComponents<PlayerAddon>())
        {
            Debug.Log("Addon Init");
            Type type = addon.Init(this);
            if (!m_addons.ContainsKey(type)) m_addons.Add(type, addon);
        }

        m_controls = new GameControls();
        m_controls.Enable();
    }

    #endregion

    #region Control Interfaces

    public GameControls Controls
    {
        get { return m_controls; }
    }

    public void SetStartedCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.started += action;
    }

    public void SetPerformedCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.performed += action;
    }

    public void SetCanceledCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.canceled += action;
    }

    public void SetClickAndReleaseCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        Debug.Log("SetCallback");
        inputAction.started += action;
        inputAction.canceled += action;
    }

    public void UnsetStartedCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.started -= action;
    }

    public void UnsetPerformedCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.performed -= action;
    }

    public void UnsetCanceledCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.canceled -= action;
    }

    public void UnsetClickAndReleaseCallback(InputAction inputAction, Action<InputAction.CallbackContext> action)
    {
        inputAction.started -= action;
        inputAction.canceled -= action;
    }

    #endregion
}