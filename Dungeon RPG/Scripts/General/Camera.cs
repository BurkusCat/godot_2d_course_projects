using Godot;
using System;

public partial class Camera : Camera3D
{
    [Export] private Node target;
    [Export] private Vector3 positionFromTarget;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleOnStartGame;
        GameEvents.OnEndGame += HandleOnEndGame;
    }

    private void HandleOnStartGame()
    {
        Reparent(target);

        Position = positionFromTarget;
    }

    private void HandleOnEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }
}
