using Godot;

public partial class AbilityHitbox : Area3D, IHitbox
{
    public float GetDamage() => GetOwner<Bomb>().Damage;
}
