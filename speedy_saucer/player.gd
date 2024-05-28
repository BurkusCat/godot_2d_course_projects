extends RigidBody2D

# Called when the node enters the scene tree for the first time.
func _ready():
	test()

func test():
	var my_variable = "Hello"
	print(my_variable)
	print(my_variable)
	print(my_variable)
	my_variable = "Good bye"
	print(my_variable)
	print(my_variable)
