# Burst
A ShootThemAll low poly Unity Game.
## Versionning
- **Unity Collab** is used for all the project except the `Scripts` folder
	https://developer.cloud.unity3d.com/collab/orgs/rynzlery/projects/burst/assets/
- **GitHub** is used for the `Scripts` folder
## Project architecture
- `Assets`
	- `Blender` : Blender & FBX files
	- `Libs` : Unity libraries from assets store or GitHub
	- `Materials` : Unity materials, to apply on objects
	- `Scripts` : C# Unity scripts
	scene
	PostProcessing_Profile
- `Scripts`
	- `Potion Controller` : Object `potion_life`
	- `Shooting Controller` : Object `GunBarrelEnd`
			- G_damage_Per_Shot = 20
			- G_time_Between_Bullet = 0.15
			- G_range = 10
	- `PostProcessingBehaviour` : Object `Main Camera`
	- `Camera Follow` : Object Main Camera
			- G_target : Object `Character`
			- G_distance : 2
			- G_height : 3
			- G_height_Damping : 2
	- `Enemy Manager` : Object `GameManagerObject`
			- G_player_Health : Script `PlayerController` on `Character` object
			- G_enemy : `EnemyBody` prefab
			- G_big_Enemy : `EnemyBody-Big` prefab
			- G_fast_Enemy : `EnemyBody-Fast` prefab
			- G_time_Between_Spawn : 1
			- G_time_Before_Wave : 10
			- G_spawn_Count : 10
			- G_spawn_Points : Array of `Transform` (set `EnemySpawns` object's children)
	- `Loot Box` : Object `LootBoxObject`
	- `Menu` : Object `Canvas` (Menu scene)
	- `UI Btn Animation` : Each button in `Canvas` object
		
## Licence
Beerware 🍺
