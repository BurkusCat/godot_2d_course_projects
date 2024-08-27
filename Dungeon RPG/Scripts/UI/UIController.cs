using Godot;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren()
            .Where(element => element is UIContainer)
            .Cast<UIContainer>()
            .ToDictionary(element => element.container);

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;

        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleEndGame()
    {
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleStartPressed()
    {
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }
}
