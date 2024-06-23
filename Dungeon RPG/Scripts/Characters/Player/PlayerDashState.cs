using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;

    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    private void HandleDashTimeout()
    {
        characterNode.Velocity = Vector3.Zero;
        characterNode.stateMachineNode.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.animPlayerNode.Play(GameConstants.ANIM_DASH);

        characterNode.Velocity = new(
            characterNode.direction.X, 0, characterNode.direction.Y
        );

        if (characterNode.Velocity == Vector3.Zero)
        {
            // if we are idling, dash the direction we are facing
            characterNode.Velocity = characterNode.spriteNode.FlipH
                ? Vector3.Left
                : Vector3.Right;
        }

        characterNode.Velocity *= speed;

        dashTimerNode.Start();
    }
}
