using System;
using Godot;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;
    [Export] private PackedScene lightningScene;

    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();

        comboTimerNode.Timeout += () => comboCounter = 1;
    }

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter,
            customSpeed: 1.5f);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;

        comboTimerNode.Stop();

        characterNode.HitboxNode.BodyEntered += HandleBodyEntered;
    }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitboxNode.BodyEntered -= HandleBodyEntered;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        comboCounter++;

        // move combo count to next attack
        comboCounter = Mathf.Wrap(
            comboCounter,
            1,
            maxComboCount + 1);

        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();

        characterNode.ToggleHitbox(true);
    }

    private void PerformHit()
    {
        Vector3 newPosition = characterNode.SpriteNode.FlipH
            ? Vector3.Left
            : Vector3.Right;

        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;

        characterNode.HitboxNode.Position = newPosition;

        characterNode.ToggleHitbox(false);
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (comboCounter != maxComboCount)
        {
            // not performed the combo
            return;
        }

        Node3D lightning = lightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lightning);
        lightning.GlobalPosition = body.GlobalPosition;
    }
}
