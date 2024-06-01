extends CharacterBody2D

const speed = 18000

func _physics_process(delta):
	velocity = Vector2(0, 0)
	
	var move_speed = speed * delta
	
	if Input.is_action_pressed("move_right"):
		velocity.x = move_speed
	if Input.is_action_pressed("move_left"):
		velocity.x = -move_speed
	if Input.is_action_pressed("move_down"):
		velocity.y = move_speed
	if Input.is_action_pressed("move_up"):
		velocity.y = -move_speed
	
	move_and_slide()
	
	var screen_size = get_viewport_rect().size
	global_position = global_position.clamp(Vector2(0,0), screen_size)
	
	print(position)
