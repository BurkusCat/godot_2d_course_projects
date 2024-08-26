using Godot;

public partial class StatLabel : Label
{
    [Export] public StatResource statResource { get; private set; }

    public override void _Ready()
    {
        statResource.OnUpdate += HandleOnUpdate;

        // show the stat on game startup
        Text = statResource.StatValue.ToString();
    }

    private void HandleOnUpdate()
    {
        Text = statResource.StatValue.ToString();
    }
}
