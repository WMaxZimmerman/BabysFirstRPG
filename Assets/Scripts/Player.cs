using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Speed;
    public int Health;
    public int Damage;

    // Use this for initialization
    void Start ()
	{
	    //Speed = 0.1f;
	    //Health = 500;
	    //Damage = 50;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
        Attack();
        Die();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-Speed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(Speed, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -Speed));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, Speed));
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            var enemy = GameObject.FindWithTag("Enemy");

            if (enemy != null)
            {
                var target = enemy.transform;
                var collider = GetComponent<BoxCollider2D>();

                if (collider.IsTouching(target.GetComponent<BoxCollider2D>()))
                {
                    Debug.Log("Player Attack", gameObject);
                    var obj = target.GetComponent<Enemy>();
                    obj.Health -= Damage;
                }
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
