commit f2a75365f700e3d820c3c97d50a217b5dd9c6978
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 27 11:00:06 2025 +0200

    Update ModernCityMap.unity

commit d2ca2c6fd850c00c5b7bb2743a031e0cb47ad023
Merge: 0a5fc31d 4a0ce114
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 27 10:59:11 2025 +0200

    Merge branch 'main' into MissionText

commit 4a0ce114bd39ab40b9f351b592c135dcd8d02450
Merge: 1aca3b44 a932fd26
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 27 10:50:19 2025 +0200

    Merge pull request #57 from ObelixSoftware/Driving-Overhaul
    
    Driving Overhaul

commit a932fd26d588fe83495a554bc6a9fc08d7cf4b6d
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Wed Aug 27 02:49:28 2025 -0600

    Driving Overhaul
    
    Improve car handling, acceleration, and camera zoom; fix rotation bugs
    
    Gameplay Updates:
    - Reduced player and cop speeds; cops slightly faster unless player boosts
    - Steering now scales with speed: minimal at very low/high speeds, strongest at mid-range and during boost
    - Acceleration follows a curve: slow from standstill, more responsive as speed increases
    - Camera zooms dynamically based on player speed
    
    Bug Fixes:
    - Fixed snapping to North at start (rotationAngle now initialized in Awake)
    - Fixed Ctrl-based steering straighten resetting; rotationAngle now updates to match straightened rotation

commit 0a5fc31d88bbaf204b879c6132ebea04966852dd
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 27 09:20:02 2025 +0200

    Update ModernCityMap.unity

commit 01358704e1906862c5e3955dc50ede332d2a3cac
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 27 09:18:18 2025 +0200

    Update ModernCityMap.unity

commit 1aca3b44200eb304ce96f379143707cca67ff113
Merge: 8326c5ae 934b2057
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 22:12:02 2025 +0200

    Merge branch 'Fixes1'

commit 934b20570647247ab42063cdf67a0335a5954957
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 21:29:43 2025 +0200

    Change game name and version 1.0

commit 6805f0deac3b70a8dfeed31ee2ffc937760df766
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 21:28:55 2025 +0200

    Update ModernCityMap.unity

commit b498f727d42fe852948e5d18721345da5687dbfc
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:17:09 2025 +0200

    Merge pull request #53 from ObelixSoftware/feature/ChangeIconMission2
    
    Updated the Mission 2 Icon

commit 4f85f954ddfa7ee636933885492bf5f36149b542
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 09:08:15 2025 +0200

    Merge pull request #54 from ObelixSoftware/Update/Readme

commit 8326c5aefa43c84dbe9ad27b4d87b28f1d8d692b
Merge: c15fb15d 8ec5ce2e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 09:08:15 2025 +0200

    Merge pull request #54 from ObelixSoftware/Update/Readme

commit 8ec5ce2e8770a6d33d502c4b1896f22297fa1864
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:27:06 2025 +0200

    Updated the readme file

commit c15fb15d96327187a0b4ce056bfc0bbb7558259c
Merge: 80222a46 9e09da1d
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:17:09 2025 +0200

    Merge pull request #53 from ObelixSoftware/feature/ChangeIconMission2
    
    Updated the Mission 2 Icon

commit 9e09da1dc1b02d42df5189c0d7647f5c0df7fa7e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:16:01 2025 +0200

    Updated the Mission 2 Icon

commit 80222a46ffdef37187bdb183be686d6a265e776c
Merge: d6479a29 d417eb38
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:14:45 2025 +0200

    Merge pull request #52 from ObelixSoftware/GameStartText
    
    Update game start text

commit d417eb38ab723b7763ea2e6634f08550d05f4ea2
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 08:13:22 2025 +0200

    Update game start text

commit d6479a295620dc1231f62652d3ea85c14536fa48
Merge: dd6781ae 7479b7ef
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 07:03:02 2025 +0200

    Merge pull request #50 from ObelixSoftware/feature/CarControllerUpdate

commit dd6781aea5091b8e6cc1a5361c354ebfbab9b535
Merge: fb74573e 4cc501d4
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 26 07:02:34 2025 +0200

    Merge pull request #51 from ObelixSoftware/Tutorial_and_arrow

commit 4cc501d48aff574c3184399e9614eb0f850aec36
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Mon Aug 25 21:27:19 2025 +0100

    Arrow works

commit a811f6311762ab3e463cdf8fe0118f8c77b68a9b
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Mon Aug 25 16:42:55 2025 +0100

    mission waypoints complete

commit 7479b7efe2ba28d3d03cefb614e4453b18855435
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 25 20:50:49 2025 +0200

    Updated the Car controller again

commit fb74573e659b65b55bb3d5cb8857d86ef2bed5e5
Merge: ea4f5ab6 3b463a79
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 25 16:12:51 2025 +0200

    Merge pull request #49 from ObelixSoftware/feature/Exit-Functionality
    
    Feature/exit functionality

commit 3b463a798c0e0d4a7096db75aac3c85ab0dddeba
Merge: 20d9a705 ea4f5ab6
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 25 15:46:18 2025 +0200

    Merge branch 'main' into feature/Exit-Functionality

commit 20d9a705476e36242cfacf29050711dbea6aabcd
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 25 15:46:10 2025 +0200

    ExitFunction
    
    Created score for player when exiting via exit zone
    
    fixed explotion animation cutoff from layer order

commit ea4f5ab611f3b233cb2bdb9ae3768364434f4d8a
Merge: b73425c9 4bdc83df
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 25 06:54:23 2025 +0200

    Merge pull request #48 from ObelixSoftware/Damage-Rework

commit 4bdc83df0de5a1d0db494d1984f6e86729ee4244
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sun Aug 24 20:41:16 2025 -0600

    Damage Update
    
    Changed damage to only calculate based off speed when the player makes contact with a cop car or a pedestrian

commit b73425c94e9df857b17968f413dcd475d3595b5b
Merge: ef6b93d4 fd89f93f
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 16:09:01 2025 +0200

    Merge pull request #47 from ObelixSoftware/feature/Exit

commit fd89f93f27b016b9c7cf0198d7cc4de27be4cd93
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 16:08:00 2025 +0200

    Exit
    
    Created the exit arrow indicating where the exit is
    
    Added and created basic assets to use for the EXIT marker and Arrow
    
    Kept arrow hidden while mission not active

commit ef6b93d48618041e8de4f35c375f7ce6ad923f4f
Merge: 19ceade2 1ffc8a67
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 13:43:18 2025 +0200

    Merge pull request #46 from ObelixSoftware/StartMission
    
    Start mission

commit 1ffc8a671a8a7735797ccc71f0a1e4c5b0e3c558
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 13:34:47 2025 +0200

    Start mission

commit 19ceade2393b9750b36b69eca6c3c1dba6c171f1
Merge: b701a92b 15fa7355
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 11:34:30 2025 +0200

    Merge pull request #45 from ObelixSoftware/fix/CompileErrors

commit 15fa7355840d1d6ec3a12236a1d7c8d3b23fb36e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 11:30:26 2025 +0200

    Disabled the YouWonPanel

commit b701a92bdd6745bbce87328ebf3736b9093d062d
Merge: bdbe404e b0c1b0a3
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 11:16:21 2025 +0200

    Merge pull request #44 from ObelixSoftware/fix/CompileErrors

commit b0c1b0a3fa65b71051d1ba808b627ffc555c4cd9
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 11:13:58 2025 +0200

    Fixed compile errors

commit bdbe404e9c20a85c4fc0055bc25f323b9fe4de6b
Merge: e3b2c7e6 ee9b146e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 10:11:37 2025 +0200

    Merge pull request #43 from ObelixSoftware/feature/Mission2

commit ee9b146e827e3b274a27c71ddf418c11a3fc826a
Merge: 7ac96bde e3b2c7e6
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 10:08:33 2025 +0200

    Merge branch 'main' into feature/Mission2

commit 7ac96bdee2f4dc958a1bc041ae74b8c594ac2618
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 10:04:11 2025 +0200

    Mission 1.2

commit e3b2c7e60de22dd36d2bcc826b8d14627a242445
Merge: b48b6156 18fb7703
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 24 07:04:45 2025 +0200

    Merge pull request #42 from ObelixSoftware/Highscore-board

commit 18fb7703664d9979ad8505257da63ad1d7dfc378
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sat Aug 23 22:36:08 2025 -0600

    Basic Highscore Board
    
    A basic scoreboard that displays when the player dies, and allows free input of a name that gets associated with the score.

commit 23600bae0bd521fdb290a6ec58911829dc830ec0
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Aug 22 23:58:04 2025 +0200

    Mission1.1

commit 4dc7aa68bed9ea426c66908d6f65d6cc03b6615b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Aug 22 23:00:24 2025 +0200

    Cutscenework1.1

commit b48b6156be4aec508902e866d7813ffe15e42fdd
Merge: cab04056 2e075d3f
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 19 11:51:55 2025 +0200

    Merge pull request #41 from ObelixSoftware/feature/Reverted-Main-Menu-Changes
    
    Reverted Changes to Main Menu - Created Update Menu1 as new Menu

commit 2e075d3f22b0ab5aeede807fa8e429cfd77ec611
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 19 11:45:15 2025 +0200

    Reverted Changes to Main Menu - Created Update Menu1 as new Menu

commit cab04056d48a08d2a7f9c549bd058c4fe196bef8
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 19 11:22:05 2025 +0200

    UpdateToMainMenuUI

commit 52fbb53bb228437a543aa23ccb01f78e5e86da11
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 19 07:35:29 2025 +0200

    Update README.md

commit 60b2ce70af1ef6a1f6fceadf54dc995aecb97c16
Merge: 6e0f235d ef318b42
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 18 13:10:44 2025 +0200

    Merge pull request #40 from ObelixSoftware/feature/MoneyIcon-ReadmeUpdate
    
    AssetUpdate

commit ef318b4262e2e1fd81d26972d200fdb60434b7f5
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 18 13:07:14 2025 +0200

    AssetUpdate
    
    Added in MoneyBag Icon (Sprites folder)
    
    Cleaned up README file

commit 6e0f235dec054dfcf6a5a3bd0f5411488ebd596b
Merge: f71a38ce d17077b9
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 18 06:29:05 2025 +0200

    Merge pull request #39 from ObelixSoftware/Main-scene-update

commit d17077b9edb4140d8344acc796472001742c26ce
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sun Aug 17 17:12:06 2025 -0600

    Added Dialogue and Hideaways
    
    Dialogue logic was added to waypoints
    
    Hideways made into a prefab and added to the saferoom buildings
    
    The mission and waypoint scripts somehow ended up in the /assets folder instead of the /assets/scripts folder, also moved those and (hopefully) fixed a bug that was making the waypoint collider not work.

commit f71a38cea965d3891d8df474c560d195b53cf684
Merge: acb0b824 5581e278
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 17 18:19:15 2025 +0200

    Merge pull request #38 from ObelixSoftware/tutorial_mission

commit 5581e278420face2ef539ab937a8547f876eaa1c
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Sun Aug 17 16:40:33 2025 +0100

    Active mission changes on waypoint reached

commit acb0b8249677ccb5dda9d0a98cbb40c6e5666f28
Merge: f4e6c11f 5e5fe1a1
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 17 07:04:40 2025 +0200

    Merge pull request #37 from ObelixSoftware/cop-reworks

commit 5e5fe1a109fbe0bcc006a4da083aca387c0e7c6f
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sat Aug 16 13:27:26 2025 -0600

    Rebalanced Cops and player damage
    
    Changed cops to have 50% mass relative to the player, hopefully allowing the player to push them out of the way more easily.
    
    Cops will also have their speed reduced to 20% when they hit the player.
    
    The player now takes damage relative to their velocity magnitude when they hit something, instead of being a fixed value.

commit 1c74d34c218802a18099644bf1b0936045891d6b
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Sat Aug 16 16:58:30 2025 +0100

    Initial mission structure and window

commit f4e6c11f167dce3712fd00ef815a82f847c264de
Merge: d46cb4b1 8d622d20
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Aug 16 06:03:18 2025 +0200

    Merge pull request #36 from ObelixSoftware/Safe-area

commit 8d622d200e2f19b9ea95f6ec3dc67d6d0d5a609e
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Aug 15 16:04:57 2025 -0600

    Added basic hide away interaction
    
    Uses a new method in GameController.cs which modifies the rate at which pursuit decays from the player

commit d46cb4b1e5e5d643855adc5f4a52ecff08232e9c
Merge: 5dfaa1f2 962b77d1
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Aug 14 18:58:03 2025 +0200

    Merge pull request #35 from ObelixSoftware/feature/MainMenuMusicChange

commit 962b77d178b3e9069788bf3c43b445566768a6c9
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Aug 14 18:51:19 2025 +0200

    Update to the Main Menu Music
    
    - Changed main menu music
    - Fixed Constraints on Minimap etc.
    - Added in the text for game over

commit 5dfaa1f2f68dab44d4df03ecadd1a3089ead9421
Merge: 3d49410f 34fba7f2
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 13 18:49:59 2025 +0200

    Merge pull request #34 from ObelixSoftware/feature/GameOverLoop

commit 34fba7f2488bff79ef6c23cfa010adfd58cd70f2
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 13 18:47:24 2025 +0200

    Game Over Loop

commit 3d49410f09c0aedda75fa029d4035bdae6a6d0f5
Merge: 8e6f3a17 fd9747a0
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 13 07:18:17 2025 +0200

    Merge pull request #33 from ObelixSoftware/fix/QAFixes

commit fd9747a06b1427842c423b5d12c44322668ee7f1
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 12 21:04:12 2025 +0200

    Did a few minor fixes
    
    - Fixed pedestrian die sound when game starts
    - Fixed Traffic lights not working in the bottom of the map
    - No Explosions sound
    - Added more pedestrians and collectables across the map

commit 8e6f3a17869258e4fb4a2d3e270f73918152f361
Merge: def4e245 c451c62b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 11 22:10:57 2025 +0200

    Merge pull request #32 from ObelixSoftware/fix/PedestrianDieSoundWebGLfix

commit c451c62bf1c92fb1a9c92a4b42f948248392b099
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 11 22:02:41 2025 +0200

    Fix to the pedestrian die sound
    
    I Had to fix the pedestrian die sound in the main menu which prevented the sound from playing in WebGL

commit def4e24581a413784106dbadf6283f2107bb9cc1
Merge: 00e58682 812060ff
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 11 20:54:20 2025 +0200

    Merge pull request #31 from ObelixSoftware/feature/CarControlledWithCTRL
    
    CTRL to straighten vehicle

commit 812060ffbd412fa4e50c1cee6e9f6a76232dc7c1
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 11 20:53:04 2025 +0200

    CTRL to straighten vehicle
    
    Added the ability to straighten the car out like our previous PlayerCarController script

commit 00e58682a73020ecca154d38c000fcaa4715e9aa
Merge: fd22747c 01e0b5b5
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 10 21:55:51 2025 +0200

    Merge pull request #30 from ObelixSoftware/Handler

commit fd22747c98f9a683d73b90c6f4258e70b53aae1d
Merge: 45914835 94ccdce0
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 10 11:29:25 2025 +0200

    Merge pull request #29 from ObelixSoftware/fix/CollectiblesHealthBoostBug

commit 94ccdce0331ea5b5cf73ad9efa65a4829dc5a94d
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 10 11:24:47 2025 +0200

    Fix to the health and boost pickups
    
    Quick fix was removing all child objects from the parent.
    
    Issue is now fixed

commit 45914835fed0556c23c28f005b61fa3722550717
Merge: 59f34c0c fadb449f
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Aug 9 12:11:41 2025 +0200

    Merge pull request #28 from ObelixSoftware/origin/RevertPhysicsCarControllerChanges

commit fadb449fc8e82755d35f658ae691e5907abaaf61
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Aug 9 12:09:33 2025 +0200

    RevertedCarControllerScript

commit 01e0b5b59b9807d3998c62d1e1ac8a0eacf9fbc0
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Aug 8 20:38:02 2025 -0600

    Basic Handler Implementation
    
    A basic version of a handler/dialogue box that pops up when there is dialogue to give the player

commit 59f34c0c1f0a557b2a899633de06dfeca4266a73
Merge: 83e8ab42 2f554a37
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Aug 8 13:01:53 2025 +0200

    Merge pull request #27 from ObelixSoftware/feature/ImprovedandUpdatedPhysicsPlayerCarController
    
    Update

commit 2f554a37691a11b96f77296aceb3aef96ad66b29
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Aug 8 12:28:17 2025 +0200

    Update
    
    Fixed collision in road
    
    Update to car controller

commit 83e8ab42481f72cdd6befe91afa475a143d17811
Merge: 9f97cf19 b73aacd4
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Aug 7 08:13:35 2025 +0200

    Merge pull request #26 from ObelixSoftware/feature/ImprovedandUpdatedPhysicsPlayerCarController
    
    Update to Car controller

commit b73aacd4eca1d1bdc6b9b14a70a669feb9a34f94
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 6 19:55:51 2025 +0200

    Update to Car controller

commit 9f97cf194d3186c46b202a3d04d14aa7b916cf72
Merge: 4cd787d3 5e8d7f66
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 6 10:22:43 2025 +0200

    Merge pull request #25 from ObelixSoftware/feature/WebGLKeyFix
    
    Fixed arrow keys not working in WebGL

commit 5e8d7f667e5d27b124420bb4c39eb16cec561ee2
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Aug 6 10:17:46 2025 +0200

    Fixed arrow keys not working in WebGL

commit 4cd787d30cb83a11d294639da04cdf299ba09d5e
Merge: 751cc426 6f664274
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 18:38:31 2025 +0200

    Merge pull request #24 from ObelixSoftware/feature/ArrowsAsMovementKeys-and-UpdateMenuInstructions
    
    Feature/arrows as movement keys and update menu instructions

commit 751cc426880ced3cbbfa7b86e0630de462fce1d1
Merge: 9cfe1b4b 03675c43
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 18:38:09 2025 +0200

    Merge pull request #23 from ObelixSoftware/feature/MakePedestriansWander
    
    Update

commit 6f66427498ccda972c6b733e68e7985be6fd1109
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 18:23:20 2025 +0200

    DrivingMachanicAndMainMenuUpdate
    
    Changed the controls on how to operate the car
    
    Updated main menu to reflect

commit 03675c4307809070ad149aa5ec095806da0ea9e3
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 13:18:35 2025 +0200

    Update
    
    Created the feature of wandering pedestrians
    
    Removed debuglogs not needed anymore
    
    adjusted the wandering pedestrians, made a few improvements
    
    Cleaned up code a bit

commit 9cfe1b4ba54d2a289acab03efe7c58ce1f788334
Merge: 3273268f 1d32f36e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 12:32:23 2025 +0200

    Merge pull request #22 from ObelixSoftware/feature/TransferAllFeaturesToNewMap
    
    Feature/transfer all features to new map

commit 1d32f36ecc381633766fcb4fa024864877e8774b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Aug 5 10:38:49 2025 +0200

    Update ModernCityMap.unity

commit a4fb522db126056f5dc24be3f46e37e63ad32e2e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 4 22:07:39 2025 +0200

    UpdateModernCityMapTuning

commit a4fa50bfee9f0c4da61f49cbc885f84df64a2876
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Aug 4 22:00:43 2025 +0200

    FineTune Pedestrian light

commit 957e6ea60bb1f5e6e2480dbd6f8773c312b6b094
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 3 21:42:36 2025 +0200

    Pedestrian walker finetune

commit 1a7927289dba36ee27c7c0b8fa28ebb647f88f7a
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Aug 3 21:23:01 2025 +0200

    Moved all features to the new map
    
    Moved all the features to the new map
    
    Finetuned the pedestrians (Random Wanderers and Traffic light section Walkers) and the traffic light system

commit 3273268f4c1976d7379c0ff394bfccc66e2a3728
Merge: bb3ca916 f1dde17b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Jul 31 07:58:17 2025 +0200

    Merge pull request #21 from ObelixSoftware/pathfinding_for_cops

commit f1dde17b65a0643606273f3df75d033801969ff5
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Wed Jul 30 23:17:44 2025 +0100

    adjusted settings

commit e00eb6a9ad722be7cd5a052ad15ccfd877ff48e1
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Wed Jul 30 23:14:49 2025 +0100

    Fixed it

commit 355d8529d58fe8b024e58bec309a1f6836545f60
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Wed Jul 30 22:43:50 2025 +0100

    Added reversing if the cop car gets stuck

commit 4ca3978d973b7f75686d8188030826a6751cc3ab
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Wed Jul 30 21:54:00 2025 +0100

    Added cop car sensors

commit bb3ca91636f5199351aa670a7e3884a1be620c57
Merge: 350b2ca7 f3d0c97e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 16:33:11 2025 +0200

    Merge pull request #20 from ObelixSoftware/feature/DescriptionOfResourceBars

commit f3d0c97e11cbba63e2c918692774d9eeb5e3014b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 15:53:52 2025 +0200

    Added text to all the resource bars

commit 350b2ca7787bb36264d200a6ce6c0665346c06cb
Merge: c52097c8 8edd2066
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 14:29:14 2025 +0200

    Merge pull request #19 from ObelixSoftware/feature/MainCameraMiniMapViewAdjustment

commit 8edd20667c85d07d8ab03f2fef4e39bb7d32853e
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 13:51:20 2025 +0200

    Updated on Main Camera and Mini Map
    
    Updated distance of main camera and minimap

commit c52097c8363b8479da400134a8b0040bc970bd9f
Merge: 5b020381 c340c50f
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 12:13:45 2025 +0200

    Merge pull request #18 from ObelixSoftware/feature/ModernCityMap

commit c340c50f02d202dc00d00536cf3c6acbd5ffe7fd
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 29 11:58:13 2025 +0200

    Update - ModernCityMap
    
    Latest Update of the design of Modern City Map

commit 5b020381e5d63b4a67a0e1259299ce6c2fdf0bb6
Merge: 20e09099 69d9e597
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 28 21:04:35 2025 +0200

    Merge pull request #17 from ObelixSoftware/feature/ModernCityMap

commit 69d9e5972357aa0a690b01dd073da0e95c3c5844
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 28 20:25:57 2025 +0200

    Update - ModernCity Assets added

commit a2d1736297780d3e88ad4dbc92585cdb7bd71cd8
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 28 19:12:22 2025 +0200

    Update - Modern City Map
    
    Another update for the city map

commit 20e09099319d28616eccca5dec78d09e224469b6
Merge: 5fbf51bb 3c52aa84
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jul 27 20:07:38 2025 +0200

    Merge pull request #16 from ObelixSoftware/feature/ModernCityMap

commit 3c52aa8413d89359937e596e0d4acfd1abf9766f
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jul 27 20:03:43 2025 +0200

    Update - ModernCityMap
    
    - Updating current ModernCityMap so far. About 35% done.
    
    - Moved the "Bars" To top left in the ModernCityMap scene (Main Map where all features will go)

commit 5fbf51bb34a917ff98cf10c4412ee14b0930ec70
Merge: bd51ec55 f08dd517
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Jul 25 16:00:14 2025 +0200

    Merge pull request #15 from ObelixSoftware/feature/ModernCityMap

commit f08dd5173bc2f9068e1bd66a9b543930345c3858
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Jul 25 14:04:09 2025 +0200

    Development of the new ModernCityMap
    
    - Added new assets for the design of the new map

commit bd51ec556c535fab668664eb1b0cfee16fe3d728
Merge: ba68dc21 ca785373
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Jul 24 07:06:19 2025 +0200

    Merge pull request #14 from ObelixSoftware/feature/healthboostspeedboost

commit ca785373b7984d2da3c32e04d8fe5396cc1a0658
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Jul 24 01:53:55 2025 +0200

    Added working health and boost pickup system with visual UI updates
    
    - Implemented a boost system that increases car speed when holding Shift
    - Boost bar now decreases during boost usage and replenishes when picking up BoostItems
    - Health bar system updated to restore health when picking up HealthItems
    - Fixed bug where pickups weren’t disappearing after collection
    - UI sliders now reflect boost and health values correctly
    - Removed unnecessary debug logs cluttering the console
    
    Feels good to finally have boost + health working smoothly!

commit ba68dc21c514565e6b763b90e11886dd5b4164da
Merge: 75e9c631 c9a63596
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Jul 23 12:32:24 2025 +0200

    Merge pull request #13 from ObelixSoftware/feature/backgroundmusictoggle

commit c9a635960c7372f12377166214708ab4e16293a7
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Jul 23 11:23:28 2025 +0200

    Background music / Main menu music update

commit 5696c5f0019bfb63bb0d3483b0e7f6ccd41469f9
Merge: f9066257 75e9c631
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Jul 23 11:18:33 2025 +0200

    Merge branch 'main' into feature/backgroundmusictoggle

commit 75e9c6310d422b7f21ef1e5c877595132e0510d5
Merge: dee003eb 2a7b595d
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Jul 23 08:22:18 2025 +0200

    Merge pull request #12 from ObelixSoftware/feature/pedestriandiesound

commit 2a7b595d40631f802ef81a7d46e7b26e730a63ca
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 22 20:07:28 2025 +0200

    Re-added pedestrian hit sound (was lost in previous merge)
    
    This PR brings back the pedestrian hit sound and logic that went missing due to merge order issues.
    
    Pedestrian sound plays correctly when hit
    
    Death + respawn logic restored
    
    Fixed audio setup and collider disabling

commit dee003ebdf75b3db7063e5da67d120928eb5f002
Merge: 5b838c96 5ffeccb9
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 22 14:41:11 2025 +0200

    Merge pull request #9 from ObelixSoftware/origin/pedestrianspawnindicator

commit f9066257b5e8dc43724d4c2ada00e998f66e8f88
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 22 14:37:00 2025 +0200

    Add background and chase music with volume control in main menu
    
    -Added background music and police chase music that loops properly
    
    -Added a volume slider in the main menu to control all the game’s music volume
    
    -Made chase music start and keep playing while the police are chasing
    
    -Fixed the music switching smoothly between background and chase tracks
    
    -Updated SoundManager to handle multiple music sources and volume settings
    
    -Made sure the volume slider remembers your settings between sessions

commit 5ffeccb9d6ce8538be12fea96b9dc1b89f7010ad
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 22 12:38:09 2025 +0200

    PedestrianSpawner
    
    Created different "colored" character assets and made them spawn randomly.

commit 5b838c963ce4b9fe2ba4ffbe169a60c8dce01538
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Jul 18 16:50:35 2025 -0600

    Update LoganScene.unity

commit 90449cd06f3a02926ea7dd226eef54d977829d00
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Jul 18 16:50:01 2025 -0600

    Scene and Layer changes
    
    LoganScene from Driving Rework I wasn't properly saved before being merged back onto main.
    
    This change includes a new layer for dead pedestrians to avoid interaction while they're down

commit 7cf319649afd0301e9fe6cbb32156712b445e326
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Jul 18 16:46:56 2025 -0600

    Adjusted Driving Physics
    
    Changed car, wall, and pedestrian physics materials to allow the car to bounce a bit and slide along walls.
    
    It's a step in the right direction but not perfect

commit 692dc6625d5dc4b82978650a1626adf7370ff36a
Merge: 1cbb564e 4c7c7a65
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Jul 18 14:17:38 2025 -0600

    Merge branch 'main' of https://github.com/ObelixSoftware/StreetRushEscape

commit 4c7c7a656177cdf146a7a9d8e0d6c7d400d6792f
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Wed Jul 16 16:02:53 2025 +0100

    Tweaked menu

commit 1cbb564e3069ee75b0baf14c8ab8ad8c2124f037
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Tue Jul 15 15:15:16 2025 -0600

    Updated UI
    
    Changed UI to have a more obvious health bar, a pursuit bar, and a time remaining bar

commit 10d3d266e1f9d67b545640025e7c6866d511c44a
Author: Inkythunder <98986057+Inkythunder@users.noreply.github.com>
Date:   Tue Jul 15 14:24:49 2025 +0100

    Added main menu with controls section

commit cd04686eec3ee376f5c43c1ff1168f8a35121541
Merge: 12fe7376 b6786a1c
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 15 14:31:39 2025 +0200

    Merge pull request #7 from ObelixSoftware/origin/AnimManager

commit b6786a1c3409b158e09bb3ae70171f74a6480989
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 15 13:48:01 2025 +0200

    AnimationManager
    
    Implemented the AnimationManager that contains the current explosion and the drifting effects - the explotion was moved from the car to the animationmanager

commit 12fe737674dc690657ba24f1050651ac226e1360
Merge: bee7570e 43930bc3
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sun Jul 13 17:31:25 2025 -0600

    Merge branch 'Police-car-driving'

commit 43930bc34ef70f7710d6e01d183b0710e2e48479
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sun Jul 13 17:30:15 2025 -0600

    Added spawners
    
    Added spawners with a system to recycle cop cars that were disabled for being out of range

commit bb8b9ba51e81b2d2921014d94d4e44b363524a59
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sun Jul 13 12:01:34 2025 -0600

    Added basic cop chasing code
    
    Cops drive turn to face the player's vector and always accelerate, no obstacle avoidance yet.

commit bee7570e9540de8b0c508f59a613dcd7d549acfa
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jul 13 09:59:51 2025 +0200

    Update changelog

commit 5634bd3158b63399855152605f76005708430b0e
Merge: 25483522 12898816
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Jul 12 15:39:14 2025 +0200

    Merge pull request #6 from ObelixSoftware/origin/SoundManager

commit 128988168138a323b5f0940a2e7dc4b0488376f1
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Jul 12 14:51:40 2025 +0200

    AddedSoundManager

commit 25483522e7e425b91fa4fea1a3c566b434f2b903
Merge: 8ae216cd ede32322
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sat Jul 12 11:07:35 2025 +0200

    Merge pull request #5 from ObelixSoftware/origin/car-crash-animation-extended

commit ede3232285f2f6929f9444130213fce13b799c60
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri Jul 11 10:06:03 2025 +0200

    ExtendedCrashAnimForCar
    
    Fixed some of the prefab links - extended the animation for the explotion

commit 8ae216cdd6e8c859e828dac2adadb915db2a0163
Merge: 1ae3c848 d0f8c3fa
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Jul 10 16:18:39 2025 +0200

    Merge pull request #4 from ObelixSoftware/origin/car-crash-animation

commit d0f8c3fa8d7bd8ce7fd19667dd63995b4691a285
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu Jul 10 15:46:36 2025 +0200

    Explotion/sound
    
    Importing of the visuals and sound - cleanup of code

commit 1ae3c84877dcdf6a9ef2548ca551ade4ce583557
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jul 8 10:15:30 2025 +0200

    PoliceCarAsset

commit 98f5dabcdec77405338cd582020fc4154790dfbd
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 7 08:00:21 2025 +0200

    Update CHANGELOG.md

commit fe9eeaf0b5170a19b7a3fec00f10f56238c4db69
Merge: 6aa41af8 5c7973aa
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 7 07:57:58 2025 +0200

    Merge pull request #1 from ObelixSoftware/Game-Controller
    
    Added Game Controller

commit 5c7973aa8ad0a4bbb7502e3d47f16ba9b9f9d1dd
Merge: 61d88650 6aa41af8
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon Jul 7 07:57:19 2025 +0200

    Merge branch 'main' into Game-Controller

commit 6aa41af86103ec2c9a9ecd7683a21d1944cd38f5
Merge: 24bb3bd4 37589dc7
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jul 6 20:47:43 2025 +0200

    Merge pull request #2 from ObelixSoftware/driving-rework

commit 37589dc7b79f915c485ade69a352d5e71a27bdfd
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sat Jul 5 19:32:42 2025 -0600

    Update SampleScene.unity

commit 44adccab1eba968617c7e7ff138e0604fb695092
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Sat Jul 5 19:22:46 2025 -0600

    Physics based car controls
    
    Changed the car to be more reliant on vectors and control a bit smoother. Still needs collision rework for pedestrians.

commit 24bb3bd4e135234e6969f426cdf27156dd3ad089
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Fri Jul 4 14:01:12 2025 -0600

    Added Game Controller
    
    Added just Game Controller code to the main, without hooks to pedestrian collision yet

commit 04c5f759d1939f0b6707f74b7cf4ca69c0779e96
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jun 29 19:33:19 2025 +0200

    Update CHANGELOG.md

commit 52574a792cebb7734896ae4104149f6d34f8638b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jun 29 19:31:55 2025 +0200

    Update .gitignore

commit 214c61c05b1f8587cbf0510f82af22fce818a0f4
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jun 29 19:31:52 2025 +0200

    Add border around the map

commit 6515e0918a08190e06f44cc671b61b05d0e33632
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Sun Jun 29 08:49:14 2025 +0200

    Add MiniMap

commit 2fcfc6480a4d76e6450f42e03c859d12dcbd302b
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jun 17 08:52:14 2025 +0200

    Add change log

commit 85e99c1d333a2e1231282364ff37128c63f6e7cb
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue Jun 17 08:50:10 2025 +0200

    Each traffic lights random change between red, yellow, green

commit 61d88650e36ab1141c5e5334833e2f9372733131
Author: Ol-Vinny <42969794+Ol-Vinny@users.noreply.github.com>
Date:   Wed Jun 11 20:28:10 2025 -0600

    Added Game Controller
    
    Added the game controller object and adjusted UI to display the new variables.
    
    Changed the pedestrians to a prefab

commit a1a4479e53d9cc603117ad071f1a0d267a417ca9
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Wed Jun 4 08:56:46 2025 +0200

    Add traffic lights (robots)

commit c14ffd0cf6a3657f684a4f289afcfbf95e35bc07
Merge: 3ec6aa59 172dce8a
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon May 19 16:29:39 2025 +0200

    Merge branch 'main' of https://github.com/ObelixSoftware/StreetRushEscape

commit 3ec6aa59db8ddc6a6ee135d28ac1b33e206accbc
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Mon May 19 16:29:36 2025 +0200

    Boost

commit 172dce8a90d09824093065f9a079daab0f8fce7a
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu May 15 19:01:33 2025 +0200

    Update README.md

commit 8fe56d19e5e2246d25a72e3da76527a09a7a93f2
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Thu May 15 08:39:04 2025 +0200

    Car don't get stuck

commit 961584157ad9131f5b4c111fcab24a89c24c7c34
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Tue May 13 07:53:48 2025 +0200

    Init

commit 769f456a7d9346e2bf7bb8a70f3dad86c4034560
Author: Lennie De Villiers <lennie.work@gmail.com>
Date:   Fri May 9 19:50:13 2025 +0200

    Initial commit
