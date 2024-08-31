using Godot;

public abstract partial class Ability : Node3D
{
    [Export(PropertyHint.Range, "0,20,1")]
    public float Damage { get; private set; } = 10;
    [Export] protected AnimationPlayer playerNode;
}
