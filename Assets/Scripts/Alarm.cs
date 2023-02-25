using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Color))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Player _player;

    public event UnityAction PlayerEntered;
    public event UnityAction PlayerExited;

    public bool IsPlayerInside { get; private set; } = false;

    private Color _colorRed = Color.red;
    private Color _initialColor;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _initialColor = _renderer.color;
    }

    public void OnPlayerEntered()
    {
        IsPlayerInside = true;
        PlayerEntered?.Invoke();
    }

    public void OnPlayerExited()
    {
        IsPlayerInside= false;
        PlayerExited?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayerInside)
            return;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _renderer.color = _colorRed;
            OnPlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            OnPlayerExited();
            _renderer.color = _initialColor;
        }
    }
}
