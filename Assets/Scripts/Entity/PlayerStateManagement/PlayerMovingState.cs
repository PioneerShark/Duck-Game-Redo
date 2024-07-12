using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Hello from the moving state :)");
    }

    public override void UpdateState(PlayerStateManager player)
    {

    }
}
