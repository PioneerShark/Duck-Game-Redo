using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : BaseAttack
{
    float duration;

    public void Update()
    {
        // some duration checks, I'd imagine
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        // does hit inherit from BaseEntity? If so, just call TakeDamage...
    }

    protected override void OnDeath()
    {
        // do the thing

        base.OnDeath();
    }
}
