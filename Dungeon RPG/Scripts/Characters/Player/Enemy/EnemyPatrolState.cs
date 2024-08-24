using Godot;

public partial class EnemyPatrolState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        // go to the 2nd point in the path, the point after
        // the return point
        destination = GetPointGlobalPosition(1);
        characterNode.AgentNode.TargetPosition = destination;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        Move();
    }
}
