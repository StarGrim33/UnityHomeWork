using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce = 1f;

    private const float MOVEMENT_THRESHOLD = 0.01f;

    private Rigidbody2D _rigidbody2d;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0,0) * Time.deltaTime * _moveSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            _rigidbody2d.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }

        if(movement > MOVEMENT_THRESHOLD)
        {
            transform.localScale = Vector3.one;
        }
        else if(movement < -MOVEMENT_THRESHOLD)
        {
            transform.localScale = new Vector3(-1, 1,1);
        }
    }
}
