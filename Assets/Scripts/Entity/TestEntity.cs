using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntity : BaseEntity
{
    // Start is called before the first frame update
    
   public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void Move(int moveX, int moveY)
    {
        base.Move(moveX, moveY);
    }
    public override void LookAt(int lookX, int lookY)
    {
        base.LookAt(lookX, lookY);
    }
}
