using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class KeyHole : MonoBehaviour
{
    public int bindingIndex = 0;
    [SerializeField] bool isComposite;
    public InputActionReference actionReference; // Ссылка на действие

    public void OnDrop(KeyObjectData droppedKey)
    {
        Debug.Log("Key dropped into hole: " + gameObject.name);
        var action = actionReference.action;
        
        if (isComposite)
        {
            action.ApplyBindingOverride(bindingIndex, droppedKey.bindingPath);
        }
        else action.ApplyBindingOverride(0, droppedKey.bindingPath);
        // Сохраняем переопределения
        //string json = action.actionMap.asset.SaveBindingOverridesAsJson();
        //PlayerPrefs.SetString("input_rebinds", json);
    }
    public void OnRemove()
    {
        Debug.Log("Key removed from hole: " + gameObject.name);
        var action = actionReference.action;
        if(isComposite)
        {
            action.RemoveBindingOverride(bindingIndex);
        }
        else action.ApplyBindingOverride(0, "");
        // Сохраняем изменения
        //string json = action.actionMap.asset.SaveBindingOverridesAsJson();
        //PlayerPrefs.SetString("input_rebinds", json);
    }
}