# TowerDefenseGame
A game built in Unity where the player must build towers to defend their base. Tower have different elements and can be combined to become stronger.

## How to Run
1. Go to the [Versions](Versions) folder
2. Download the newest version
3. Unzip it
4. Run the .exe (Note: do not run "UnityCrashHandler64.exe", run the other .exe)

## Game Info
### Towers
Tower       | Tier     | Type(s)      | Damage | Attack Speed | Range | Cost | Notes
----------- | -------- | ------------ | ------ | ------------ | ----- | ---- | -----
Air         | Basic    | Air          | 1      | 0.5 sec      | 5     | 60   | 
Earth       | Basic    | Earth        | 5      | 2.0 sec      | 2     | 100  | Hits all enemies within range
Fire        | Basic    | Fire         | 2      | 1.0 sec      | 3     | 120  | Deals 1 dps for 2 sec
Water       | Basic    | Water        | 2      | 1.5 sec      | 4     | 80   | Slows enemies by 25% for 2 sec
Lightning   | Advanced | Air, Fire    | #      | #            | #     | #    | To be designed
Ice         | Advanced | Water, Air   | #      | #            | #     | #    | To be designed
Quicksand   | Advanced | Earth, Water | #      | #            | #     | #    | To be designed
Volcano     | Advanced | Fire, Earth  | #      | #            | #     | #    | To be designed
Apocalypse  | Expert   | All          | #      | #            | #     | #    | Not created yet

### Enemies
Enemy  | Color  | Health | Damage | Worth | Move Speed
-------| ------ | ------ | ------ | ----- | ----------
Normal | Red    | 5      | 5      | 20    | 10
Heavy  | Blue   | 10     | 10     | 10    | 5
Light  | Yellow | 2      | 2      | 40    | 20

### Waves
Wave | Enemy Count | Enemy Type
---- | ----------- | ------
1    | 3           | Normal
2    | 2           | Heavy
3    | 4           | Light
4    | 4           | Normal
5    | 6           | Light
6    | 4           | Heavy
