using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;



public class InputManager : MonoBehaviour
{
[Header("Ссылки")]
[SerializeField] PlayerController playerController;
    public static InputManager Instance { get; private set; }

    public InputAction CaptureAction;
    public string CaptureBindingPath = string.Empty;
    public string CaptureDisplayName = string.Empty;

    public InputActionMap PlayerActionMap;
    Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CaptureAction = new InputAction(binding: "<Keyboard>/*");
            CaptureAction.Enable();
            //CaptureAction.performed += OnCapture;
            DontDestroyOnLoad(gameObject);
            PlayerActionMap = InputSystem.actions.FindActionMap("Player");
            PlayerActionMap.Enable();
            PlayerActionMap.FindAction("Move").performed += OnMove;
            PlayerActionMap.FindAction("Move").canceled += OnMove;
            PlayerActionMap.FindAction("Jump").performed += OnJumpPerformed;
            PlayerActionMap.FindAction("Crouch").performed += OnCrouchPerformed;
            PlayerActionMap.FindAction("Run").performed += OnRunPerformed;
            PlayerActionMap.FindAction("Run").canceled += OnRunCanceled;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("INPUT CROUCH");
        playerController.SetIsCrouching(true);
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        playerController.SetIsRunning(false);
    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        playerController.SetIsRunning(true);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        playerController.SetIsJumping(true);
    }

    //private void OnCapture(InputAction.CallbackContext context)
    //{
    //    CaptureBindingPath = context.control.path;
    //    CaptureDisplayName = context.control.displayName;
    //}

    private void OnDisable()
    {
        CaptureAction.Disable();
        PlayerActionMap.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();        
    }

    private void Update()
    {
        playerController.SetMoveInput(moveInput);
    }
}
