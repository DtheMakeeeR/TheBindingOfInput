using UnityEngine;

public class KeyPickUp : PickUp
{
    [Header("Ссылки")]
    [SerializeField] KeyPanel panel;
    [SerializeField] KeyObjectData keyUIPrefab;
    [SerializeField] GameObject inventory;
    [SerializeField] Canvas mainCanvas;
    public override void ApplyEffect(PlayerController player)
    {
        player.SetCanMove(false);
        foreach (Transform t in inventory.transform)
        {
            if(t.childCount == 0)
            {
                ItemSlot slot = t.GetComponent<ItemSlot>();
                if(slot != null )
                {
                    KeyObjectData newKey = Instantiate(keyUIPrefab, t.position, Quaternion.identity, t);
                    panel.gameObject.SetActive(true);
                    panel.SetCurrentData(newKey);
                    break;
                }
            }
        }
    }
}
