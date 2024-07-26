using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{
    float maxWaddleAngle = 45f;

    public override void EnterState(Player player)
    {
        Debug.Log("Hello from the moving state :)");
        player.controls.Enable();
        player.sprite.transform.rotation = Quaternion.identity;
        player.controls.Player.Movement.performed += ctx =>
        {
            player.velocity = ctx.ReadValue<Vector2>();
        };
    }

    public override void UpdateState(Player player)
    {
        Waddle(player);

        if (player.health <= 0) player.SwitchState(player.downedState);
        if (!player.controls.Player.Movement.IsInProgress()) player.SwitchState(player.idleState);
        if (player.controls.Player.Roll.IsPressed()) player.SwitchState(player.rollingState);
    }

    void Waddle(Player player)
    {
        float waddleAngle = maxWaddleAngle > 0 ? player.velocity.magnitude : -player.velocity.magnitude;
        player.sprite.transform.Rotate(Vector3.forward, waddleAngle);

        if (Mathf.Abs(player.sprite.transform.rotation.eulerAngles.z) >= Mathf.Abs(maxWaddleAngle)) maxWaddleAngle *= -1;
    }
}
