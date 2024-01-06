using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for reading input from the player.
/// </summary>
public class InputReader : MonoBehaviour, PlayerControls.IPlayerActions
{
    /// <summary>
    /// The player controls.
    /// </summary>
    public PlayerControls playerControls { get; private set; } = null;

    /// <summary>
    /// The player's move input.
    /// </summary>
    public Vector2 MoveInput { get; private set; }

    /// <summary>
    /// The player's hook input.
    /// </summary>
    public event Action OnHookEvent;

    /// <summary>
    /// The player's attack input.
    /// </summary>
    public event Action OnAttackEvent;

    private void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Player.SetCallbacks(this);
        playerControls.Player.Enable();
    }

    private void OnDestroy()
    {
        playerControls.Player.Disable();
    }

    /// <summary>
    /// Read the player's move input.
    /// <paramref name="context"/>: The context of the input.
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Read the player's hook input.
    /// <paramref name="context"/>: The context of the input.
    /// </summary>
    /// <param name="context"></param>
    public void OnHook(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnHookEvent?.Invoke();
    }

    /// <summary>
    /// Read the player's attack input.
    /// <paramref name="context"/>: The context of the input.
    /// </summary>
    /// <param name="context"></param>
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        OnAttackEvent?.Invoke();
    }
}
