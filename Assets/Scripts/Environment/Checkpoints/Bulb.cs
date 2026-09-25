using UnityEngine;

public class Bulb : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool isActive = false;
    private void Awake()
    {
        if (isActive) spriteRenderer.color = Color.green;
        else spriteRenderer.color = Color.red;
    }
    public void TurnBulb()
    {
        if (isActive)
        {
            isActive = false;
            spriteRenderer.color = Color.red;
        }
        else
        {
            isActive = true;
            spriteRenderer.color = Color.green;
        }
    }
}
