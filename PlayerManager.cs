using Godot;
using System;

public partial class PlayerManager : Node3D
{

	[Export]
	private PackedScene playerScene;

	[Export]
	private InputManager inputManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void addPlayer(int device)
	{
		player p = (player)playerScene.Instantiate();
		AddChild(p);
		p.setControls(inputManager.createDeviceControls(device));
		p.Position = Vector3.Zero + Vector3.Up * 8;

	}

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
		
		if(@event.IsActionPressed("start"))
		{
			if(@event is InputEventJoypadButton) addPlayer(@event.Device);
			if(@event is InputEventKey) addPlayer(-1);
		}
    }
}
