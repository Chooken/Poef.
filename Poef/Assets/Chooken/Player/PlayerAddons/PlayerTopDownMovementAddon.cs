using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTopDownMovementAddon : PlayerAddon
{
    private TopDownMovementCache m_movementCache;
    private Rigidbody2D m_rb;

    [SerializeField] private float m_walkSpeed = 5f;

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("OnEnableCalled");
        m_player.SetPerformedCallback(m_player.Controls.Gameplay.Move, OnMove);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Callback Fired");
        m_movementCache.movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = Time.fixedDeltaTime * m_walkSpeed * m_movementCache.movement ;
    }
}

public struct TopDownMovementCache
{
    public Vector2 movement;
}
