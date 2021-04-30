# TowerDefenseGame
A game built in Unity where the player must build towers to defend their base. Tower have different elements and can be combined to become stronger.

## How to Run
1. Go to the [Versions](Versions) folder
2. Download the newest version
3. Unzip it
4. Run TowerDefenseGame.exe (Note: DONT run "UnityCrashHandler64.exe")

## Game Info
### Towers
Tower       | Tier     | Type(s)      | Cost | Damage | Attack Speed | Range | # of Targets | Notes
----------- | -------- | ------------ | ---- | ------ | ------------ | ----- | ------------ | -----
Air         | Basic    | Air          | 60   | 1      | 0.5 sec      | 5     | 1            | Fast Attacks
Earth       | Basic    | Earth        | 100  | 5      | 2.0 sec      | 2     | 2*           | Multishot*
Fire        | Basic    | Fire         | 120  | 2      | 1.0 sec      | 3     | 1            | Attack Modifier, Burn: Deals 1 dps for 2 sec
Water       | Basic    | Water        | 80   | 2      | 1.5 sec      | 4     | 1            | Attack Modifier, Gush: Slows enemies by 25% for 2 sec
Lightning   | Advanced | Air, Fire    | #    | #      | #            | #     | 1            | To be designed
Ice         | Advanced | Earth, Water | #    | #      | #            | #     | 3*           | To be designed
Tornado     | Advanced | Water, Air   | #    | #      | #            | #     | 1            | To be designed
Volcano     | Advanced | Fire, Earth  | #    | #      | #            | #     | 5*           | To be designed
Apocalypse  | Expert   | All          | #    | #      | #            | #     | #            | Not created yet

\*to be implemented

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
