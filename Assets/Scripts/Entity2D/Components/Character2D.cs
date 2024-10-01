using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2D : Entity2D
{
    public float moveSpeed = 16f;
    public Vector2 moveVector = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (rigidbody != null)
        {
            rigidbody.velocity = moveVector.normalized * moveSpeed;
        }
    }

    public void SetMoveVector(Vector2 newMoveVector)
    {
        this.moveVector = newMoveVector;
    }
}