# Lab Break

You are an experiment in a research laboratory. You are to collect all evidence and escape the laboratory. You have to solve puzzles in order to obtain some items.

Lastly, be careful as there are hazards that can kill you!

## Controls

WASD - Move
E - Interact
Shift - Sprint
Space - Jump
F - Fire ( Upon obtaining gun )

## Requirements

There isn't really a requirement to run my application, but these are what I created this game with.

- Platform: Windows 11
- Hardware:
    - CPU: AMD Ryzen 7 7840HS
    - RAM: 32 GB ( Honestly you don't need much RAM to run the application )
    - GPU: NVIDIA GeForce RTX 4050
- Recommended Resolution: 2560 x 1440

## Limitations / Bugs

- Interacting Too Quickly
    - Some UI elements are displayed for a few seconds. Interacting too quickly can cause multiple UIs to overlap.

- Door Trigger Zones
    - I used OnTriggerExit to make doors close automatically when the player leaves the trigger zone. However, when a door opens, its trigger zone shifts. This can cause the door to close on the player if they're no longer inside the updated zone.

- Doors Pushing the Player
    - Since the doors have colliders, they might push the player when closing automatically, especially if the player is still near. This can result in the player being pushed or even flung.

## References / Credits

I used ChatGPT and GitHub CoPilot to make some of my codes work.

- Teleporting player to spawn after death
- Total time taken stats for player to exit the facility
- Doors always opening away from the player
- Script to make collectibles float and rotate

- SFX
    - Door Opening and Closing: https://pixabay.com/sound-effects/main-door-opening-closing-38280/
    - Collectibles: https://pixabay.com/sound-effects/collect-points-190037/
    - Power Box: https://pixabay.com/sound-effects/rachet-click-47834/
    - Victory: https://pixabay.com/sound-effects/goodresult-82807/
    - Gun Shot: https://uppbeat.io/sfx/desert-eagle-gunshot/5027/19574
    - Pressure Plates: https://uppbeat.io/sfx/button-push-chunky-plastic-button-1/359/4889
    - Background Music: https://www.youtube.com/watch?v=SY-49rIbOSc&list=PLobY7vO0pgVIOZNKTVRhkPzrfCjDJ0CNl&index=71&ab_channel=HeatleyBros-RoyaltyFreeVideoGameMusic

- Images
    - Power Box's Electrical Supply: https://www.istockphoto.com/vector/electrical-hazard-sign-high-voltage-danger-symbol-vector-illustration-gm993960510-269196159