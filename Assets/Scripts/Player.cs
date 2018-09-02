using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Speed  { get; set; }

	// Use this for initialization
	void Start ()
	{
	    Speed = 0.1f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
        Attack();
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
            Debug.Log("Attack", gameObject);
        }
    }
}
