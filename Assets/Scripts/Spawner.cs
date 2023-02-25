using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private int _count;
    [SerializeField] private Vector2 _spawnArea;

    private Coroutine _coroutine;
    private int _time = 2;
    private float _angle = 0f;

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, _spawnArea, _angle);

        for (int i = 0; i < _count; i++)
        {
            var randomPoint = colliders[Random.Range(0, colliders.Length)].ClosestPoint(transform.position);
            Instantiate(_template, randomPoint, Quaternion.identity);
            yield return new WaitForSeconds(_time);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _spawnArea);
    }
}
