using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] AudioSource soundAS;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player != null )
        {
            doorAnimator.SetTrigger("Interact");
            soundAS.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponentInParent<PlayerController>();
        if(player != null )
        {
            doorAnimator.SetTrigger("Interact");
            soundAS.Play();
        }
    }
}
