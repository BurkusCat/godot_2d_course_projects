extends CharacterBody2D

@export var gravity = 400
@export var speed = 125
@export var jump_force = 200

@onready var animated_sprite = $AnimatedSprite2D

func _physics_process(delta):
	if is_on_floor() == false:
		velocity.y += gravity * delta
		
		# cap max fall speed
		if velocity.y > 500:
			velocity.y = 500
		
	if Input.is_action_just_pressed("jump") && is_on_floor():
		velocity.y = -jump_force
	
	var direction = Input.get_axis("move_left", "move_right")
	velocity.x = direction * speed
	
	move_and_slide()

# Boolean Operators
# and &&
# or ||
# not !

func _ready():
	var a = true
	var b = false
	var result = !a || b
	print(result)
