using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;
    public AudioSource activateAS;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnActivate?.Invoke();
        CheckpointsManager.Instance.SetActiveCheckpoint(this);
        activateAS.Play();
    }
}
