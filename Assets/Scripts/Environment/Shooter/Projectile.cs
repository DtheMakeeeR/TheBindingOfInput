using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    Coroutine coroutine;

    IEnumerator _MoveCoroutine(Vector2 direction, float maxDistance)
    {
        Vector3 targetPos = transform.position + (Vector3)(direction.normalized * maxDistance);
        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        gameObject.SetActive(false);
    }
    public void Launch(Vector2 direction, float maxDistance)
    {
        if(coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(_MoveCoroutine(direction, maxDistance));
    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stop();
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().Respawn();
        }
        gameObject.SetActive(false);
    }
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
