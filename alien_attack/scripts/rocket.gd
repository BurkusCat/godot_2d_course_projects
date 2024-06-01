extends Area2D

@export var speed = 120

func _physics_process(delta):
	global_position.x += speed * delta
	print(speed * delta)
