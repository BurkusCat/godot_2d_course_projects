extends Node2D

signal enemy_spawned(enemy_instance)
signal path_enemy_spawned(path_enemy_instance)

var enemy_scene = preload("res://scenes/enemy.tscn")
var path_enemy_scene = preload("res://scenes/path_enemy.tscn")
@onready var enemy_container = $EnemyContainer
@onready var spawn_positions = $SpawnPositions

@onready var enemy_timer = $EnemyTimer
@onready var path_enemy_timer = $PathEnemyTimer

const min_normal_enemy_timer = 1
const min_path_enemy_timer = 5

# number of enemies to spawn before increasing difficulty
const difficult_increase_threshold = 5

# number of enemies until next difficulty
var next_difficulty_count = difficult_increase_threshold

func _on_timer_timeout():
	spawn_enemy()
	
	next_difficulty_count -= 1
	if next_difficulty_count <= 0:
		enemy_timer.wait_time = clamp(enemy_timer.wait_time - 0.25, min_normal_enemy_timer, 3)
		path_enemy_timer.wait_time = clamp(path_enemy_timer.wait_time - 0.75, min_path_enemy_timer, 10)
		next_difficulty_count = difficult_increase_threshold
		print(enemy_timer.wait_time)
		print(path_enemy_timer.wait_time)

func spawn_enemy():
	var spawn_positions_array = spawn_positions.get_children()
	var random_spawn_position = spawn_positions_array.pick_random()
	
	var enemy_instance = enemy_scene.instantiate()
	enemy_instance.global_position = random_spawn_position.global_position
	emit_signal("enemy_spawned", enemy_instance)

func _on_path_enemy_timer_timeout():
	spawn_path_enemy()

func spawn_path_enemy():
	var path_enemy_instance = path_enemy_scene.instantiate()
	emit_signal("path_enemy_spawned", path_enemy_instance)
