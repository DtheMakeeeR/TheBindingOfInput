using Unity.VisualScripting;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    public abstract void ApplyEffect(PlayerController player);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"TriggerEnter: {collision.gameObject.name}");
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player != null)
        {
            Debug.Log("Player Founded");
            ApplyEffect(player);
            Destroy(gameObject);
        }        
    }
}
