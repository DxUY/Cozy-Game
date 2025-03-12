using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static event Action OnStartPressed;

    private PlayerInput playerInput;
    private InputAction startAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        startAction = playerInput.actions["Start"];
    }

    private void Update()
    {
        if (startAction.WasPressedThisFrame())
        {
            OnStartPressed?.Invoke();
            EventBus.OnSceneChangeRequest?.Invoke("main");
        }
    }
}
