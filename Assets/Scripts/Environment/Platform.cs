using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Collider2D collider;
    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D player = collision.gameObject.GetComponent<Collider2D>();
        if (player != null)
        {
            Physics2D.IgnoreCollision(collider, player, false);
        }
    }
}
