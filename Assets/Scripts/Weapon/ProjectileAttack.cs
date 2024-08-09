using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : BaseAttack
{
    int velocity;
    float rangeRemaining;
    int collisionsRemaining;

    public void Update()
    {
        if (rangeRemaining <= 0) OnDeath();
    }

    protected void OnCollisionEnter2D(Collision2D hit)
    {
        // does hit inherit from BaseEntity? If so, just call TakeDamage...

        collisionsRemaining--;

        if (collisionsRemaining <= 0) OnDeath();
    }

    protected override void OnDeath()
    {
        // do the stuff
        base.OnDeath();
    }
}
