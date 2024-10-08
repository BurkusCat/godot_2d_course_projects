using Godot;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        currentState.Notification(5001);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = states
            .FirstOrDefault(state => state is T);

        if (newState == null)
        {
            return;
        }

        if (currentState is T)
        {
            // don't switch to a state we are already in
            return;
        }

        if (!newState.CanTransition())
        {
            // don't switch to a state we are already in
            return;
        }

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
