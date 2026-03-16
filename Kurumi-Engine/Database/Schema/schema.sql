BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "stats" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"short_name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "skills" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "elements" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "enemy_elements" (
	"enemy_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("enemy_id","element_id"),
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	FOREIGN KEY("enemy_id") REFERENCES "enemies"("id")
);
CREATE TABLE IF NOT EXISTS "statuses" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"sprite_id"	INTEGER NOT NULL,
	"priority"	INTEGER NOT NULL,
	"accuracy_modifier"	INTEGER NOT NULL,
	"evasion_modifier"	INTEGER NOT NULL,
	"turn_length"	INTEGER NOT NULL,
	"cure_at_battle_end"	INTEGER NOT NULL,
	"can_act"	INTEGER NOT NULL,
	"turn_effect_sprite_id"	INTEGER NOT NULL,
	"turn_effect"	TEXT,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "statuses_stats" (
	"status_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("status_id","stat_id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	FOREIGN KEY("stat_id") REFERENCES "stats"("id")
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
CREATE TABLE IF NOT EXISTS "abilities" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"effect"	TEXT NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"cost"	INTEGER NOT NULL,
	"mp_cost"	INTEGER NOT NULL,
	"battle_sprite_id"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT),
	FOREIGN KEY("element_id") REFERENCES "elements"("id")
);
CREATE TABLE IF NOT EXISTS "enemy_statuses" (
	"enemy_id"	INTEGER NOT NULL,
	"status_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("enemy_id","status_id"),
	FOREIGN KEY("enemy_id") REFERENCES "enemies"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id")
);
CREATE TABLE IF NOT EXISTS "equipment_skills" (
	"equipment_id"	INTEGER NOT NULL,
	"skill_id"	INTEGER NOT NULL,
	PRIMARY KEY("equipment_id","skill_id"),
	FOREIGN KEY("skill_id") REFERENCES "skills"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id")
);
CREATE TABLE IF NOT EXISTS "equipment_abilities" (
	"equipment_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	PRIMARY KEY("equipment_id","ability_id"),
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id")
);
CREATE TABLE IF NOT EXISTS "equipment_elements" (
	"equipment_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("equipment_id","element_id"),
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id")
);
CREATE TABLE IF NOT EXISTS "statuses_elements" (
	"status_id"	INTEGER NOT NULL,
	"element_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("status_id","element_id"),
	FOREIGN KEY("element_id") REFERENCES "elements"("id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id")
);
CREATE TABLE IF NOT EXISTS "statuses_abilities" (
	"status_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	PRIMARY KEY("status_id","ability_id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id")
);
CREATE TABLE IF NOT EXISTS "statuses_skills" (
	"status_id"	INTEGER NOT NULL,
	"skill_id"	INTEGER NOT NULL,
	"sealed"	INTEGER NOT NULL,
	PRIMARY KEY("status_id","skill_id"),
	FOREIGN KEY("status_id") REFERENCES "statuses"("id"),
	FOREIGN KEY("skill_id") REFERENCES "skills"("id")
);
CREATE TABLE IF NOT EXISTS "tile_objects" (
	"id"	INTEGER NOT NULL UNIQUE,
	"art_id"	INTEGER NOT NULL,
	"animated"	INTEGER NOT NULL,
	"passable"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "equipment" (
	"id"	INTEGER NOT NULL UNIQUE,
	"item_id"	INTEGER NOT NULL UNIQUE,
	"equipment_type"	INTEGER NOT NULL,
	"equipment_slot"	INTEGER NOT NULL,
	"attack"	INTEGER NOT NULL,
	"defence"	INTEGER NOT NULL,
	"magic_attack"	INTEGER NOT NULL,
	"magic_defence"	INTEGER NOT NULL,
	"accuracy_modifier"	INTEGER NOT NULL,
	"evasion_modifier"	INTEGER NOT NULL,
	"turn_effect"	TEXT,
	"turn_effect_sprite_id"	INTEGER NOT NULL,
	PRIMARY KEY("id","item_id"),
	FOREIGN KEY("equipment_slot") REFERENCES "equipment_slots"("id"),
	FOREIGN KEY("equipment_type") REFERENCES "equipment_types"("id"),
	FOREIGN KEY("item_id") REFERENCES "items"("id")
);
CREATE TABLE IF NOT EXISTS "enemies" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"max_hp"	INTEGER NOT NULL,
	"battle_sprite_id"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "items" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL UNIQUE,
	"description"	TEXT NOT NULL,
	"effect"	TEXT,
	"usable_in_battle"	INTEGER NOT NULL,
	"usable_in_menu"	INTEGER NOT NULL,
	"targets_party"	INTEGER NOT NULL,
	"targets_enemies"	INTEGER NOT NULL,
	"targets_all"	INTEGER NOT NULL,
	"consume_on_use"	INTEGER NOT NULL,
	"sprite_id"	INTEGER NOT NULL,
	"price"	INTEGER NOT NULL,
	"weight"	INTEGER NOT NULL,
	PRIMARY KEY("id","name")
);
CREATE TABLE IF NOT EXISTS "equipment_stats" (
	"equipment_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("equipment_id","stat_id"),
	FOREIGN KEY("equipment_id") REFERENCES "equipment"("id"),
	FOREIGN KEY("stat_id") REFERENCES "stats"("id")
);
CREATE TABLE IF NOT EXISTS "enemy_stats" (
	"enemy_id"	INTEGER NOT NULL,
	"stat_id"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	PRIMARY KEY("stat_id","enemy_id"),
	FOREIGN KEY("stat_id") REFERENCES "stats"("id"),
	FOREIGN KEY("enemy_id") REFERENCES "enemies"("id")
);
CREATE TABLE IF NOT EXISTS "playable_characters" (
	"id"	INTEGER NOT NULL UNIQUE,
	"name"	TEXT NOT NULL,
	"description"	TEXT NOT NULL,
	"battle_sprite_id"	INTEGER NOT NULL,
	"field_sprite_id"	INTEGER NOT NULL,
	"menu_sprite_id"	INTEGER NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "item_pools" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "item_pool_items" (
	"item_pool_id"	INTEGER NOT NULL,
	"item_id"	INTEGER NOT NULL,
	PRIMARY KEY("item_id","item_pool_id"),
	FOREIGN KEY("item_pool_id") REFERENCES "item_pools"("id"),
	FOREIGN KEY("item_id") REFERENCES "items"("id")
);
CREATE TABLE IF NOT EXISTS "enemy_abilities" (
	"enemy_id"	INTEGER NOT NULL,
	"ability_id"	INTEGER NOT NULL,
	PRIMARY KEY("ability_id","enemy_id"),
	FOREIGN KEY("enemy_id") REFERENCES "enemies"("id"),
	FOREIGN KEY("ability_id") REFERENCES "abilities"("id")
);
CREATE TABLE IF NOT EXISTS "actors" (
	"art_id"	INTEGER NOT NULL UNIQUE,
	"width"	INTEGER NOT NULL,
	"height"	INTEGER NOT NULL,
	PRIMARY KEY("art_id" AUTOINCREMENT)
);
COMMIT;
