using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public class EnemyScene : KinematicBody2D
{
	public int runSpeed = 75;
	public Vector2 velocity;
	public Vector2 playerVelocity;

	public Label Text;
	public LineEdit Typer;
	public Area2D crystal;
	public KinematicBody2D Player;
	public CollisionShape2D enemyCollider;
	public Polygon2D PlayerSprite;
	public KinematicBody2D Enemy;
	
	public Polygon2D EnemySprite;
	
	
		
	Random word_from_list = new Random();

	
	public override void _Ready()
	{
		Text = GetNode<Label>("Text");
		Typer = GetNode<LineEdit>("/root/Node2D/Typer");
		Player = GetNode<KinematicBody2D>("/root/Node2D/Player");
		EnemySprite = GetNode<Polygon2D>("Polygon2D");
		PlayerSprite = GetNode<Polygon2D>("/root/Node2D/Player/Polygon2D");
		crystal = GetNode<Area2D>("/root/Node2D/crystal");
		Enemy = GetNode<KinematicBody2D>(".");

		
		
		Text.Text = Global.easy_words[word_from_list.Next(0, Global.easy_words.Length)];
		

	}


	public override void _Process(float delta)
	{	
		if (Global.is_dead == false)
		{
			velocity = (crystal.Position - Position).Normalized() * runSpeed;
			velocity = MoveAndSlide(velocity);

		}
		
		else if (Global.is_dead == true)
		{
			velocity = MoveAndSlide(new Vector2(0,0));
		}
		
		LookAt(crystal.Position);
		if (Position.x > 635)
		{
			Enemy.Scale = new Vector2(-1, 1);
			Text.RectScale = new Vector2(1, -1);
		}
		else if (Position.x < 635)
		{
			EnemySprite.RotationDegrees = 90;
			
		}
		
		if (Typer.Text == Text.Text)
		{
			Global.pos = Position;
			Enemy_Kill();
		}
		
	}
	
	public void Enemy_Kill()
	{
		Typer.Text = "";
		Global.is_dead = true;
		Global.WordsTyped += 1;
		QueueFree();

	}
	
}



