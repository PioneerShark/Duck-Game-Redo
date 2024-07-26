using Unity.VisualScripting;
using UnityEngine;

public class PlayerDownedState : PlayerBaseState
{
    /* This place is still experimental! I'm not too sure how the duck
     will revive or whatever. So this has been put together so we can move between 
    downed and the other states :)
     */
    public override void EnterState(Player player)
    {
        player.controls.Disable();
        player.controls.Player.TESTGainHealth.Enable(); // press G to Gain health
        player.controls.Player.TESTTakeDamage.Enable(); // press T to Take damage

        player.velocity = Vector3.zero;
        player.sprite.transform.Rotate(Vector3.forward, 180);
        Debug.Log("They've fallen, and they can't get up!");
    }

    public override void UpdateState(Player player)
    {
        if (player.health > 0) player.SwitchState(player.idleState);
    }
}
