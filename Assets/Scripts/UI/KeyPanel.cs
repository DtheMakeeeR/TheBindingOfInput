using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPanel : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] KeyObjectData currentData;
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] GameObject panel;
    private void OnEnable()
    {
        InputManager.Instance.CaptureAction.canceled += OnCapture;
    }
    private void OnDisable()
    {
        InputManager.Instance.CaptureAction.canceled -= OnCapture;
    }
    private void OnCapture(InputAction.CallbackContext context)
    {
        if(gameObject.activeSelf)
        {
            string path = context.control.path;
            string name = context.control.displayName;
            Debug.Log($"NAME: {name}");
            if (name == "Space") return;
            bool flowControl = CheckBinding(path);
            if (!flowControl)
            {
                return;
            }

            hintText.text = name;
            currentData.Override(path, name);
        }
    }

    private static bool CheckBinding(string path)
    {
        var newBinding = new InputBinding { path = path };
        int bindingIndex = InputManager.Instance.PlayerActionMap.FindBinding(newBinding, out InputAction existingAction);

        if (bindingIndex != -1)
        {
            Debug.Log($"Кнопка уже забинжена : {existingAction.name}");
            return false; 
        }

        return true;
    }

    public void SetCurrentData(KeyObjectData data) => currentData = data;
    public void EndBinding()
    {
        if(currentData != null && currentData.bindingPath != "")
        {
            panel.SetActive(false);
        }
    }
}
