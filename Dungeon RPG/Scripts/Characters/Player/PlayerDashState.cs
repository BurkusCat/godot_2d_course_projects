using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private PackedScene bombScene;
    [Export] private Timer cooldownTimerNode;

    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10;

    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;

        CanTransition = () => cooldownTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    private void HandleDashTimeout()
    {
        cooldownTimerNode.Start();

        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    protected override void EnterState()
    {
        base.EnterState();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);

        characterNode.Velocity = new(
            characterNode.direction.X, 0, characterNode.direction.Y
        );

        if (characterNode.Velocity == Vector3.Zero)
        {
            // if we are idling, dash the direction we are facing
            characterNode.Velocity = characterNode.SpriteNode.FlipH
                ? Vector3.Left
                : Vector3.Right;
        }

        characterNode.Velocity *= speed;

        dashTimerNode.Start();

        // spawn a bomb
        Bomb bomb = bombScene.Instantiate<Bomb>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.GlobalPosition;
    }
}
