using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    public int health, maxHealth, moveSpeed;
    public float lookSpeed;
    [HideInInspector]
    public bool turning;
    [HideInInspector]
    public float turnAngle;
    [HideInInspector]
    public Vector2 velocity;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (turning) turnAngle = Rotate(turnAngle);
        TriggerMove();
    }

    // 
    public virtual void TriggerAttack()
    {
      
    }
    //Looks at a position based on in world coordinates, turnrate is how fast an entity looks at an  based on degrees per second
    public virtual void TriggerLookAt(int lookX, int lookY)
    {
        Vector3 lookDirection = new Vector3 (lookX, lookY, transform.position.z) - transform.position;
        float resultantAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        turnAngle = resultantAngle;
        turning = true;
    }

    float Rotate(float angleFull)
    {
        bool positiveAngle = angleFull < 0 ? false : true;

        float turnSegment = lookSpeed * Time.deltaTime;

        if (turnSegment > Mathf.Abs(angleFull)) turnSegment = angleFull;
        else if(!positiveAngle) turnSegment *= -1;

        transform.Rotate(Vector3.forward, turnSegment);
        angleFull -= turnSegment;

        if (angleFull == 0) turning = false;

        return angleFull;
    }

    public virtual void TriggerMove()
    {
        Vector3 moveVector = new Vector3(velocity.x * moveSpeed, velocity.y * moveSpeed, 0);
        moveVector *= Time.deltaTime;
        transform.Translate(moveVector);
    }
    
}
