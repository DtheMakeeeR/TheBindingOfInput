using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnActivate?.Invoke();
        CheckpointsManager.Instance.SetActiveCheckpoint(this);
    }
}
