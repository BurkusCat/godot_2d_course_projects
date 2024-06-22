using Godot;
using System;

public partial class Player : CharacterBody3D
{
    public override void _PhysicsProcess(double delta)
    {
        GD.Print("Player physics process!");
    }

    public override void _Input(InputEvent @event)
    {
        GD.Print("Player input!");
    }
}
