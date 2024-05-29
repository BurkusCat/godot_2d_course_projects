extends RigidBody2D

# Called when the node enters the scene tree for the first time.
func _ready():
	# < > == != >= <=
	var test = 5
	if test > 5:
		print("True")
		print("Second message")
	elif test == 5:
		print("Test is 5")
	else:
		print("False")
	
	var message = "Greet"
	if message == "Hello":
		print("Hi there")
	elif message == "Bye":
		print("Goodbye!")
	elif message == "Thanks":
		print("Invalid message")

func _process(delta):
	pass

func _physics_process(delta):
	apply_force(Vector2(25, 0))
