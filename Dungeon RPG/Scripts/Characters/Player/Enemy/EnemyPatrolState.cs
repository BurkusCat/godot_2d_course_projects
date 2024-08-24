using System;
using Godot;

public partial class EnemyPatrolState : EnemyState
{
    private int pointIndex = 0;

    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        pointIndex = 1;

        // go to the 2nd point in the path, the point after
        // the return point
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    private void HandleNavigationFinished()
    {
        pointIndex = Mathf.Wrap(
            pointIndex + 1,
            0,
            characterNode.PathNode.Curve.PointCount);
        destination = GetPointGlobalPosition(pointIndex);
        characterNode.AgentNode.TargetPosition = destination;
    }
}