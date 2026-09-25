using Unity.Cinemachine;
using UnityEngine;

public class RoomCameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineCamera roomCamera;

    private const int RoomPriority = 5;
    private const int ActiveRoomPriority = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER TRIGGER");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("ENTER TRIGGER2");
            roomCamera.Priority = ActiveRoomPriority;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomCamera.Priority = RoomPriority;
        }
    }
}
