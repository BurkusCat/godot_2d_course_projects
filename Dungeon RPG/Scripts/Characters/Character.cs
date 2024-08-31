using System;
using System.Linq;
using Godot;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public StateMachine StateMachineNode { get; private set; }
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public CollisionShape3D HitboxShapeNode { get; private set; }
    [Export] public Timer ShaderTimerNode { get; private set; }


    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }


    public Vector2 direction = new();
    private ShaderMaterial shader;

    public override void _Ready()
    {
        if (SpriteNode.MaterialOverlay is ShaderMaterial shaderMaterial)
        {
            shader = shaderMaterial;
        }

        HurtboxNode.AreaEntered += HandleHurtboxEntered;
        SpriteNode.TextureChanged += HandleTextureChanged;
        ShaderTimerNode.Timeout += HandleShaderTimeout;
    }

    private void HandleShaderTimeout()
    {
        // stop the shader flash effect
        shader.SetShaderParameter(
            "active", false
        );
    }

    private void HandleTextureChanged()
    {
        shader.SetShaderParameter(
            "tex", SpriteNode.Texture
        );
    }

    private void HandleHurtboxEntered(Area3D area)
    {
        if (area is not IHitbox hitbox)
        {
            return;
        }

        StatResource health = GetStatResource(Stat.Health);
        float damage = hitbox.GetDamage();
        health.StatValue -= damage;

        shader.SetShaderParameter(
            "active", true
        );

        // start the timer so we can deactivate flash after a moment
        ShaderTimerNode.Start();
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.FirstOrDefault(element => element.StatType == stat);
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally)
        {
            return;
        }

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }

    public void ToggleHitbox(bool flag)
    {
        HitboxShapeNode.Disabled = flag;
    }
}
