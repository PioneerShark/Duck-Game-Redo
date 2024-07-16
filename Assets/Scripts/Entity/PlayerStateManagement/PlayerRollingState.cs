using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    bool rollComplete;
    float amountRolled;
    readonly float rollIncrement = -2f;

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Hello from the rolling state >:)");
        rollComplete = false;
        amountRolled = 0f;
        player.TriggerDash(player.velocity, 3f, 5f);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        Roll(player);

        // add condition for downed state being entered
        if (player.health <= 0) player.SwitchState(player.downedState);
        if (rollComplete)
        {
            if (player.velocity == Vector2.zero) player.SwitchState(player.idleState);
            else player.SwitchState(player.movingState);
        }
    }

    void Roll(PlayerStateManager player)
    {
        player.sprite.transform.Rotate(Vector3.forward, rollIncrement);
        amountRolled += rollIncrement;

        if (Mathf.Abs(amountRolled) >= 360) rollComplete = true;
    }
}
