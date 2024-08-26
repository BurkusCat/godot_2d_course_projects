using Godot;

public partial class EnemyDeathState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DEATH);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        // we have to delete the parent path otherwise the remaining
        // enemy count won't properly update
        characterNode.PathNode.QueueFree();
    }
}
