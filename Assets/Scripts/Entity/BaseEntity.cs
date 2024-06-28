using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    public int health, maxHealth, moveSpeed;
    public float lookSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Move(10, 0);
        LookAt(-1, -1);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // 
    public virtual void Attack()
    {
      
    }
    //Looks at a position based on in world coordinates, turnrate is how fast an entity looks at an  based on degrees per second
    public virtual void LookAt(int lookX, int lookY)
    {
        Vector3 lookDirection = new Vector3 (lookX, lookY, transform.position.z) - transform.position;
        float resultantAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Debug.Log(resultantAngle);
        //transform.rotation = Quaternion.AngleAxis(resultantAngle, Vector3.forward);
        StartCoroutine(Rotate(resultantAngle));
    }
    IEnumerator Rotate(float angleFull)
    {
        bool positiveAngle = angleFull < 0 ? false : true;
        while ((angleFull > 0 && positiveAngle) || (angleFull < 0 && !positiveAngle))
        {
            //yield return new WaitForSeconds(0.1f);
            float turnSegment = lookSpeed * Time.deltaTime;
            if (turnSegment > Mathf.Abs(angleFull))
            {
                turnSegment = angleFull;
            }
            else if(!positiveAngle) { turnSegment *= -1; }
            //transform.rotation = Quaternion.AngleAxis(turnSegment, Vector3.forward);
            transform.Rotate(Vector3.forward, turnSegment);
            angleFull -= turnSegment;
            Debug.Log(angleFull);
        }
        yield return null;
    }

    public virtual void Move(int moveX, int moveY)
    {
        Vector3 moveVector = new Vector3(moveX * moveSpeed, moveY * moveSpeed, 0);
        moveVector *= Time.deltaTime;
        transform.Translate(moveVector);
    }
    
}
