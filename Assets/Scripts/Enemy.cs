using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public int Health;
    public int Damage;

    // Use this for initialization
    void Start()
    {
        //Speed = 0.1f;
        //Health = 100;
        //Damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Die();
    }

    private void Move()
    {
        var player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            var target = player.transform;
            var forwardAxis = new Vector3(0, 0, -1);

            transform.LookAt(target.position, forwardAxis);
            Debug.DrawLine(transform.position, target.position);
            transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
            transform.position -= transform.TransformDirection(Vector2.up) * Speed;
        }
    }

    private void Attack()
    {
        var target = FindObjectOfType<Player>();

        if (target != null)
        {
            var collider = GetComponent<BoxCollider2D>();

            if (collider.IsTouching(target.GetComponent<BoxCollider2D>()))
            {
                Debug.Log("Enemy Attack", gameObject);
                target.Health -= Damage;
            }
        }
    }

    private void Die()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
