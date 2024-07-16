using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

public partial class InputManager : Node3D
{


	private List<String> DefActions = new List<String>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		createDefCopy();
		createDeviceActions(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void createDefCopy()
	{
		foreach(StringName s in InputMap.GetActions()) 
			if(((string)s)[0] == '~')	
				{
					DefActions.Add(s);
					foreach(InputEvent inputEvent in InputMap.ActionGetEvents("s"))
					{
						inputEvent.Device = -9;
					}
				}
	}

	public Controls createDeviceControls(int device)
	{
		createDeviceActions(device);
		return new Controls(device);
	}

	private void createDeviceActions(int device)
	{
		foreach(String s in DefActions) 
		{
			InputMap.AddAction(device + s);

			foreach(InputEvent inputEvent in  InputMap.ActionGetEvents(s))
			{
				if((device < 0 && !(inputEvent is InputEventJoypadButton || inputEvent is InputEventJoypadMotion)) || (device >= 0 && (inputEvent is InputEventJoypadButton || inputEvent is InputEventJoypadMotion)))
				{
					InputEvent newEvent = (InputEvent)inputEvent.Duplicate(true);
					newEvent.Device = device;
					InputMap.ActionAddEvent(device + s, newEvent);
				}
			}
		}
	}
}

public class Controls
{
	private int device;

	public Controls(int dv)
	{
		device = dv;
	}

	public string getAction(string action)
	{
		return device + action;
	}
}
