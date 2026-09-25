using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    public KeyHole keyHole;
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            if (droppedItem != null)
            {
                droppedItem.GetComponent<DraggableItem>().ParentAfterDrag = transform;
                keyHole?.OnDrop(droppedItem.GetComponent<KeyObjectData>());
                Debug.Log("Dropped item: " + droppedItem.name + " into slot: " + gameObject.name);
            }
        }
    }
}
