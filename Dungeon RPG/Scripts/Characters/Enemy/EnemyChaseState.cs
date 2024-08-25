using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] public Timer timerNode { get; private set; }

    private CharacterBody3D target;

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
            .First() as CharacterBody3D;

        timerNode.Timeout += HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }


    protected override void ExitState()
    {
        timerNode.Timeout -= HandleTimeout;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
    }


    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleTimeout()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;
    }

    protected void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    protected void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
}
