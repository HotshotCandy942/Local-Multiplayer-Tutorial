using Godot;
using System;
using System.Linq.Expressions;


public partial class player : RigidBody3D
{
	[Export]
	private float moveSpeed = 0.02f;

	private Vector2 movementInput;

	private Controls controls;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void setControls(Controls c)
	{
		controls = c;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		movementInput = new Vector2(Input.GetAxis(controls.getAction("~moveLeft"), controls.getAction("~moveRight")), Input.GetAxis(controls.getAction("~moveForward"), controls.getAction("~moveBackward")));
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

		ApplyCentralForce(new Vector3(movementInput.X * moveSpeed, 0, movementInput.Y * moveSpeed));
    }
}
