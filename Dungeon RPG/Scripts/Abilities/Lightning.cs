using Godot;

public partial class Lightning : Ability
{
    public override void _Ready()
    {
        // despawn lightning when animation finished
        playerNode.AnimationFinished += (animName) => QueueFree();
    }
}
