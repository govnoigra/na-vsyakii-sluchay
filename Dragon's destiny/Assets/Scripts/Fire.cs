using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float speed = 10.0f;
    private SpriteRenderer fireSprite;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private void Awake()
    {
        fireSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.5F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
