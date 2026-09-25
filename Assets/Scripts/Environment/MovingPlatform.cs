using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] Transform[] targetPositions;
    [SerializeField] float speed;
    Vector2 startPosition = Vector2.zero;
    int targetIndex = 0;
    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if (targetPositions == null)
        {
            Debug.LogWarning($"{gameObject.name}: No targets!");
            return;
        }
        MoveToTarget();
        if (CheckIfClose())
        {
            SetNextTarget();
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPositions[targetIndex].position, speed * Time.deltaTime);        
    }

    private bool CheckIfClose()
    {
        return Vector2.Distance(transform.position, targetPositions[targetIndex].position) < 0.1f;
    }

    private void SetNextTarget()
    {
        targetIndex++;
        if (targetIndex >= targetPositions.Length)
        {
            targetIndex = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
