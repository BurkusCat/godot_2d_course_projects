using Godot;
using System;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        int totalEnemies = GetChildCount();

        GameEvents.RaiseNewEnemyCount(totalEnemies);

        // when an enemy dies
        ChildExitingTree += HandleChildExitingTree;
    }

    private void HandleChildExitingTree(Node node)
    {
        // need to subtract 1 as the child hasn't quite been removed from the tree yet
        int totalEnemies = GetChildCount() - 1;
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        if (totalEnemies == 0)
        {
            // all enemies have been defeated
            GameEvents.RaiseVictory();
        }
    }

}
