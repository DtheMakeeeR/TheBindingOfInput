using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player != null )
        {
            doorAnimator.SetTrigger("Interact");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player != null )
        {
            doorAnimator.SetTrigger("Interact");
        }
    }
}
