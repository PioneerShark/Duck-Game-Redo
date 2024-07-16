using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Hello from the Idle State!");
        player.controls.Enable();
        player.velocity = Vector3.zero;
        player.sprite.transform.rotation = Quaternion.identity;
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.health <= 0) player.SwitchState(player.downedState);
        if (player.controls.Player.Movement.IsPressed()) player.SwitchState(player.movingState);
        if (player.controls.Player.Roll.IsPressed()) player.SwitchState(player.rollingState);
    }
}
