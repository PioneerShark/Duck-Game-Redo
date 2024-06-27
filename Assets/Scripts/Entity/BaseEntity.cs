using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    public int health, maxHealth, moveSpeed;
    float lookSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Move(10, 0);
        LookAt(10, 20);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // 
    public virtual void Attack()
    {
      
    }

    public virtual void LookAt(int lookX, int lookY)
    {
        Vector3 lookCurrent = transform.forward;
        Vector3 lookTo = new Vector3(lookX, lookY, transform.position.z) - transform.position;
        //transform.forward = Vector3.RotateTowards(lookCurrent, lookTo, lookSpeed * Time.deltaTime);
    }
    public virtual void Move(int moveX, int moveY)
    {
        Vector3 moveVector = new Vector3(moveX * moveSpeed, moveY * moveSpeed, 0);
        moveVector *= Time.deltaTime;
        transform.Translate(moveVector);
    }
}
