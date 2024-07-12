using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Hello from the moving state :)");

        player.controls.Player.Movement.performed += ctx =>
        {
            player.velocity = ctx.ReadValue<Vector2>();
        };
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.controls.Player.Movement.WasReleasedThisFrame()) player.SwitchState(player.idleState);
    }
}
