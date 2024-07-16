using Unity.VisualScripting;
using UnityEngine;

public class PlayerDownedState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        // should just be able to disable them
        player.controls.Disable();
        player.controls.Player.TESTGainHealth.Enable();
        player.controls.Player.TESTTakeDamage.Enable();

        player.velocity = Vector3.zero;
        player.sprite.transform.Rotate(Vector3.forward, 180);
        Debug.Log("They've fallen, and they can't get up!");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.health > 0) player.SwitchState(player.idleState);
    }
}
