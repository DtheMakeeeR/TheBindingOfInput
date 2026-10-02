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
            PlayerActionMap = InputSystem.actions.FindActionMap("Player");
            PlayerActionMap.Enable();
            ClearAllBindings(PlayerActionMap);
            PlayerActionMap.FindAction("Move").performed += OnMove;
            PlayerActionMap.FindAction("Move").canceled += OnMove;
            PlayerActionMap.FindAction("Jump").performed += OnJumpPerformed;
            PlayerActionMap.FindAction("Crouch").performed += OnCrouchPerformed;
            PlayerActionMap.FindAction("Attack").performed += OnAttackPerformed;
            PlayerActionMap.FindAction("Run").performed += OnRunPerformed;
            PlayerActionMap.FindAction("Run").canceled += OnRunCanceled;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void ClearAllBindings(InputActionMap map)
    {
        foreach (var action in map.actions)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                var binding = action.bindings[i];

                // Пропускаем заголовок композита — его путь трогать нельзя
                if (binding.isComposite) continue;

                // Обнуляем путь (и для обычных, и для частей композита)
                action.ApplyBindingOverride(i, "");
            }
        }
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        playerController.SetIsAttacking(true);
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
        Debug.Log("JUMP PERFORMED");
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
        Debug.Log("MOVE INPUT: " + context.ReadValue<Vector2>());
        moveInput = context.ReadValue<Vector2>();        
    }

    private void Update()
    {
        playerController?.SetMoveInput(moveInput);
    }
}
