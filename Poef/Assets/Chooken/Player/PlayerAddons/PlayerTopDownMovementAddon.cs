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
        m_camera = Camera.main;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        m_player.SetPerformedCallback(m_player.Controls.Gameplay.Move, OnMove);
        m_player.SetPerformedCallback(m_player.Controls.Gameplay.MouseSuggest, OnMouseSuggestion);
        m_player.SetPerformedCallback(m_player.Controls.Gameplay.GamepadSuggest, OnGamepadSuggestion);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_movementCache.movement = context.ReadValue<Vector2>();
        if (m_movementCache.movement != Vector2.zero) m_isPossessed = true;
    }

    private Vector2 m_gamepadVect;
    private Vector2 m_suggestionPos;
    private Camera m_camera;

    private bool m_isPossessed;

    public void OnMouseSuggestion(InputAction.CallbackContext context)
    {
        m_suggestionPos = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        m_isPossessed = false;
    }

    public void OnGamepadSuggestion(InputAction.CallbackContext context)
    {
        m_gamepadVect = context.ReadValue<Vector2>();

        if (m_gamepadVect == Vector2.zero) return;

        if (m_isPossessed) m_suggestionPos = gameObject.transform.position;
        m_isPossessed = false;
    }

    private void FixedUpdate()
    {
        if (m_isPossessed)
        {
            m_rb.velocity = Time.fixedDeltaTime * m_walkSpeed * m_movementCache.movement;
            return;
        }

        m_suggestionPos += Time.fixedDeltaTime * 15f * m_gamepadVect;
        DrawSuggestionDebug();

        Vector2 relativePos = m_suggestionPos - (Vector2)gameObject.transform.position;

        if (relativePos.magnitude < 0.05f)
        {
            gameObject.transform.position = m_suggestionPos;
            m_rb.velocity = Vector2.zero;
            return;
        }

        m_rb.velocity = Time.fixedDeltaTime * m_walkSpeed * relativePos.normalized;
    }

    private void DrawSuggestionDebug()
    {
        Vector2 debugTopLeft = new Vector2(m_suggestionPos.x - 0.1f, m_suggestionPos.y + 0.1f);
        Vector2 debugTopRight = new Vector2(m_suggestionPos.x + 0.1f, m_suggestionPos.y + 0.1f);
        Vector2 debugBottomLeft = new Vector2(m_suggestionPos.x - 0.1f, m_suggestionPos.y - 0.1f);
        Vector2 debugBottomRight = new Vector2(m_suggestionPos.x + 0.1f, m_suggestionPos.y - 0.1f);

        Debug.DrawLine(debugTopLeft, debugTopRight);
        Debug.DrawLine(debugTopRight, debugBottomRight);
        Debug.DrawLine(debugBottomRight, debugBottomLeft);
        Debug.DrawLine(debugBottomLeft, debugTopLeft);
    }
}

public struct TopDownMovementCache
{
    public Vector2 movement;
}
