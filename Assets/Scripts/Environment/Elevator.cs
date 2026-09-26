using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] Collider2D doorCollider;
    [SerializeField] float speed;

    
    private void MoveToDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(_MovingCoroutine());
        doorCollider.enabled = false;
    }

    IEnumerator _MovingCoroutine()
    {
        while(Vector2.Distance(transform.position, destination.position) > 0.1f)
        {
            MoveToDestination();
            yield return null;
        }
        doorCollider.enabled = true;
    }
}
