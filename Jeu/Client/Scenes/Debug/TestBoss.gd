extends Node3D

@onready var player = $Player

func _physics_process(_delta):
	get_tree().call_group("Bosses","update_target_location",player.global_transform.origin)
