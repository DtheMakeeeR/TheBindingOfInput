using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;


public class KeyObjectData : MonoBehaviour
{
    public string bindingPath;   
    public string displayName;
    [SerializeField] TextMeshProUGUI label;

    private void Awake()
    {
        label.text = displayName;
    }
    public void Override(string path, string name)
    {
        bindingPath = path;
        displayName = name;
        label.text = name;
    }
}
