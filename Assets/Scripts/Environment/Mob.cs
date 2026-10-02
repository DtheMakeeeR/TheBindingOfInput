using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform[] targetPositions;
    [SerializeField] float speed;


    int targetIndex = 0;
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
        animator.SetFloat("MovingXDir", (targetPositions[targetIndex].position - transform.position).x);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Respawn();
        }
    }
}
