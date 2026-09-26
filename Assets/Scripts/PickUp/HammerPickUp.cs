using UnityEngine;

public class HammerPickUp : PickUp
{
    [SerializeField] GameObject hammerSlot;
    public override void ApplyEffect(PlayerController player)
    {
        hammerSlot.SetActive(true);
    }
}
