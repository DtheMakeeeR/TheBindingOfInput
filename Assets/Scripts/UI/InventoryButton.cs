using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void OnClick()
    {
        animator.SetTrigger("Click");
    }
}
