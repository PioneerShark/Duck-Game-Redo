using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    public int health, maxHealth, moveSpeed;
    public float lookSpeed;

    [HideInInspector]
    public bool turning;

    protected float turnAngle;

    [HideInInspector]
    public Vector2 velocity, dashVelocity;

    protected Transform moveTarget;

    protected Transform waypoint;

    [HideInInspector]
    public float dashDistance;

    [HideInInspector]
    public GameObject sprite;

    [HideInInspector]
    public Transform armSprite;


    // Start is called before the first frame update
    public virtual void Start()
    {
        //TriggerDash(90, 5f, 10);
        sprite = transform.Find("Sprite").gameObject;
        try
        {
            armSprite = transform.Find("Arm");
        }
        catch
        {
            Debug.LogWarning("No child arm gameobject");
        }
        armSprite = transform.Find("Arm");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (dashDistance > 0) dashDistance = Dash(dashDistance);
        else TriggerMove();

        if (turning) turnAngle = Rotate(turnAngle);
        
    }

    
    public virtual void TriggerAttack()
    {
      
    }
    public virtual void TriggerLookAt(Vector2 look)
    {
        Vector2 lookDirection = look - (Vector2)transform.position;
        
        float resultantAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if (armSprite) resultantAngle -= armSprite.rotation.eulerAngles.z;
        else resultantAngle -= transform.rotation.eulerAngles.z;
        if (resultantAngle > 180)
        {
            resultantAngle -= 360;
        }
        else if (resultantAngle < -180) resultantAngle += 360;
        turnAngle = resultantAngle;
        turning = true;
    }

    float Rotate(float angleFull)
    {
        bool positiveAngle = angleFull < 0 ? false : true;
        float turnSegment = lookSpeed * Time.deltaTime;

        if (turnSegment > Mathf.Abs(angleFull)) turnSegment = angleFull;
        else if(!positiveAngle) turnSegment *= -1;
        if (armSprite)
        {
            armSprite.transform.Rotate(Vector3.forward, turnSegment);
        }
        else
        {
            transform.Rotate(Vector3.forward, turnSegment);
        }
        
        angleFull -= turnSegment;

        if (angleFull == 0) turning = false;
        if (armSprite)
        {
            if (Mathf.Abs(armSprite.transform.rotation.eulerAngles.z) < 91 || Mathf.Abs(armSprite.transform.rotation.eulerAngles.z) > 270)
            {
                sprite.GetComponent<SpriteRenderer>().flipX = false;
                armSprite.GetComponent<SpriteRenderer>().flipY = false;
            }
            else { 
                sprite.GetComponent<SpriteRenderer>().flipX = true;
                armSprite.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        return angleFull;
    }

    protected void TriggerMove()
    {
        velocity.Normalize();
        Vector3 moveVector = new Vector3(velocity.x * moveSpeed, velocity.y * moveSpeed, 0);
        moveVector *= Time.deltaTime;
        transform.Translate(moveVector, Space.World);
    }

    protected void TriggerMoveTo(Vector2 targetPos) {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed*Time.deltaTime);
    }

    public void TriggerDash(Vector2 dashDir, float speedMult, float dashDis) {
        //Vector2 dashDir = new Vector2(Mathf.Sin(dashAngleDeg), Mathf.Cos(dashAngleDeg));
        if (dashDir != Vector2.zero) dashDir.Normalize();
        else dashDir = new Vector2(1, 0);

        dashVelocity = dashDir * speedMult * moveSpeed;
        dashDistance = dashDis;
    }
    float Dash(float dashDis)
    {
        Vector2 dashSegment = Time.deltaTime * dashVelocity;
        transform.Translate(dashSegment, Space.World);
        dashDis -= dashSegment.magnitude;
        if (dashDis < 0)
        {
            return 0;
        }
        return dashDis;
    }
}
