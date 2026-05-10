BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "stats" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"short_name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "elements" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "equipment_types" (
	"id"	INTEGER NOT NULL UNIQUE,
	"type_name"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "equipment_slots" (
	"id"	INTEGER NOT NULL UNIQUE,
	"slot_name"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "equipment_abilities" (
	"equipment_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id"),
	PRIMARY KEY("equipment_id","ability_id")
);
CREATE TABLE IF NOT EXISTS "equipment_elements" (
	"equipment_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	PRIMARY KEY("equipment_id","element_id")
);
CREATE TABLE IF NOT EXISTS "equipment_stats" (
	"equipment_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("stat_id") REFERENCES "stats"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	PRIMARY KEY("equipment_id","stat_id")
);
CREATE TABLE IF NOT EXISTS "item_pools" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "item_pool_items" (
	"item_pool_id"	INTEGER NOT NULL,
	"item_id"	INTEGER NOT NULL,
	FOREIGN KEY("item_id") REFERENCES "items"("id"),
	FOREIGN KEY("item_pool_id") REFERENCES "item_pools"("id"),
	PRIMARY KEY("item_id","item_pool_id")
);
CREATE TABLE IF NOT EXISTS "actor_paths" (
	"actor_id"	INTEGER NOT NULL,
	"path_index"	INTEGER NOT NULL,
	"direction"	INTEGER NOT NULL,
	FOREIGN KEY("actor_id") REFERENCES "actors"("id"),
	PRIMARY KEY("path_index","actor_id")
);
CREATE TABLE IF NOT EXISTS "ability_sets" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "actor_sprites" (
	"id"	INTEGER NOT NULL UNIQUE,
	"sprite_name"	INTEGER NOT NULL,
	"width"	INTEGER NOT NULL,
	"height"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "actors" (
	"id"	INTEGER NOT NULL UNIQUE,
	"behaviour"	INTEGER NOT NULL,
	"sprite_id"	INTEGER NOT NULL,
	"movement_speed"	INTEGER NOT NULL,
	"tracking_range"	NUMERIC NOT NULL,
	"below_party"	INTEGER NOT NULL,
	"passable"	INTEGER NOT NULL,
	"on_touch"	INTEGER NOT NULL,
	"auto"	INTEGER NOT NULL,
	"on_action"	INTEGER NOT NULL,
	"on_find"	INTEGER NOT NULL,
	"script_name"	TEXT,
	FOREIGN KEY("sprite_id") REFERENCES "actor_sprites"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "enemy_formations" (
	"id"	INTEGER NOT NULL UNIQUE,
	"map_name"	TEXT NOT NULL,
	"return_x"	INTEGER NOT NULL,
	"return_y"	INTEGER NOT NULL,
	"search_timer"	INTEGER NOT NULL,
	"item_pool_id"	INTEGER NOT NULL,
	"on_found_actor_info_id"	INTEGER NOT NULL,
	"default_actor_info_id"	INTEGER NOT NULL,
	"on_lose_script"	TEXT,
	"on_win_script"	TEXT,
	FOREIGN KEY("item_pool_id") REFERENCES "item_pools"("id"),
	FOREIGN KEY("default_actor_info_id") REFERENCES "actors"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "enemy_formation_enemies" (
	"id"	INTEGER NOT NULL,
	"enemy_formation_id"	INTEGER NOT NULL,
	"enemy_id"	INTEGER NOT NULL,
	"x_location"	INTEGER NOT NULL,
	"y_location"	INTEGER NOT NULL,
	"main_part"	INTEGER NOT NULL,
	"on_kill_script"	TEXT,
	FOREIGN KEY("main_part") REFERENCES "enemy_formation_enemies"("id"),
	FOREIGN KEY("enemy_id") REFERENCES "entities"("id"),
	FOREIGN KEY("enemy_formation_id") REFERENCES "enemy_formations"("id"),
	PRIMARY KEY("id")
);
CREATE TABLE IF NOT EXISTS "equipment" (
	"id"	INTEGER NOT NULL UNIQUE,
	"item_id"	INTEGER NOT NULL UNIQUE,
	"equipment_type"	INTEGER NOT NULL,
	"equipment_slot"	INTEGER NOT NULL,
	"accuracy_modifier"	INTEGER NOT NULL,
	"evasion_modifier"	INTEGER NOT NULL,
	"turn_effect_script"	TEXT,
	FOREIGN KEY("item_id") REFERENCES "items"("id"),
	FOREIGN KEY("equipment_slot") REFERENCES "equipment_slots"("id"),
	FOREIGN KEY("equipment_type") REFERENCES "equipment_types"("id"),
	PRIMARY KEY("id","item_id")
);
CREATE TABLE IF NOT EXISTS "items" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL UNIQUE,
	"description"	TEXT NOT NULL,
	"script"	TEXT,
	"usable_in_battle"	INTEGER NOT NULL,
	"usable_in_menu"	INTEGER NOT NULL,
	"targets_party"	INTEGER NOT NULL,
	"targets_enemies"	INTEGER NOT NULL,
	"targets_all"	INTEGER NOT NULL,
	"consume_on_use"	INTEGER NOT NULL,
	"sprite_name"	TEXT NOT NULL,
	"price"	INTEGER NOT NULL,
	"weight"	INTEGER NOT NULL,
	PRIMARY KEY("id","name")
);
CREATE TABLE IF NOT EXISTS "abilities" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"script_name"	TEXT NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"cost"	INTEGER NOT NULL,
	"uses_mp"	INTEGER NOT NULL,
	"battle_sprite_animation_name"	TEXT NOT NULL,
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "equipment_statuses" (
	"equipment_id"	INTEGER NOT NULL,
	"status_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	PRIMARY KEY("equipment_id","status_id")
);
CREATE TABLE IF NOT EXISTS "equipment_ability_sets" (
	"equipment_id"	INTEGER NOT NULL,
	"ability_set_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	FOREIGN KEY("ability_set_id") REFERENCES "ability_sets"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	PRIMARY KEY("equipment_id","ability_set_id")
);
CREATE TABLE IF NOT EXISTS "characters" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"battle_sprite"	TEXT NOT NULL,
	"field_sprite"	TEXT NOT NULL,
	"menu_sprite"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "entities" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"max_hp"	INTEGER NOT NULL,
	"battle_sprite_name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "entity_abilities" (
	"entity_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id"),
	FOREIGN KEY("entity_id") REFERENCES "entities"("id"),
	PRIMARY KEY("ability_id","entity_id")
);
CREATE TABLE IF NOT EXISTS "entity_elements" (
	"entity_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	FOREIGN KEY("entity_id") REFERENCES "entities"("id"),
	PRIMARY KEY("entity_id","element_id")
);
CREATE TABLE IF NOT EXISTS "entity_stats" (
	"entity_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("stat_id") REFERENCES "stats"("id"),
	FOREIGN KEY("entity_id") REFERENCES "entities"("id"),
	PRIMARY KEY("stat_id","entity_id")
);
CREATE TABLE IF NOT EXISTS "entity_statuses" (
	"entity_id"	INTEGER NOT NULL,
	"status_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("entity_id") REFERENCES "entities"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	PRIMARY KEY("entity_id","status_id")
);
CREATE TABLE IF NOT EXISTS "tiles" (
	"id"	INTEGER NOT NULL UNIQUE,
	"art_id"	INTEGER NOT NULL,
	"animated"	INTEGER NOT NULL,
	"passable"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "statuses" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"sprite_name"	TEXT NOT NULL,
	"priority"	INTEGER NOT NULL,
	"accuracy_modifier"	INTEGER NOT NULL,
	"evasion_modifier"	INTEGER NOT NULL,
	"turn_length"	INTEGER NOT NULL,
	"cure_at_battle_end"	INTEGER NOT NULL,
	"can_act"	INTEGER NOT NULL,
	"turn_effect_script"	TEXT,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "status_abilities" (
	"status_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	PRIMARY KEY("status_id","ability_id")
);
CREATE TABLE IF NOT EXISTS "status_ability_sets" (
	"status_id"	INTEGER NOT NULL,
	"ability_set_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	FOREIGN KEY("ability_set_id") REFERENCES "ability_sets"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	PRIMARY KEY("status_id","ability_set_id")
);
CREATE TABLE IF NOT EXISTS "status_elements" (
	"status_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	PRIMARY KEY("status_id","element_id")
);
CREATE TABLE IF NOT EXISTS "status_stats" (
	"status_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	FOREIGN KEY("stat_id") REFERENCES "stats"("id"),
	PRIMARY KEY("status_id","stat_id")
);
CREATE TABLE IF NOT EXISTS "enemy_formation_enemy_battle_scripts" (
	"id"	INTEGER NOT NULL,
	"enemy_formation_enemy_id"	INTEGER NOT NULL,
	"script_name"	TEXT NOT NULL,
	"target"	INTEGER NOT NULL,
	"start_turn"	INTEGER NOT NULL,
	"frequency"	INTEGER NOT NULL,
	FOREIGN KEY("enemy_formation_enemy_id") REFERENCES "enemy_formation_enemies"("id"),
	PRIMARY KEY("id")
);
COMMIT;
