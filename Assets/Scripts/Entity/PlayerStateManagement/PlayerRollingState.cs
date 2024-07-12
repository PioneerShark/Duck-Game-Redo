using UnityEngine;

public class PlayerRollingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Hello from the rolling state >:)");
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}
