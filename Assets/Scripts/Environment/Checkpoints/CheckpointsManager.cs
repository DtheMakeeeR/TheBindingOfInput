using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckpointsManager : MonoBehaviour
{
    public static CheckpointsManager Instance;
    public Transform RespawnTransform => activeCheckpoint.transform;
    Checkpoint activeCheckpoint;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        activeCheckpoint?.OnDeactivate?.Invoke();
        activeCheckpoint = checkpoint;
        
    }
}
