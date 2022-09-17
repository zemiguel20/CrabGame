# AIGT Project

## Project description

Project for the "AI for Game Technology" course about Reinforcement Learning.

## Game Design

Move a crab inside a sandbox in order to dodge moving seagulls.
The player should survive the most time he can without touching any seagull.

The environment is 2D.
Player movement is horizontal, vertical or diagonal,
using either the arrow keys or WASD keys from a keyboard.
Seagulls spawn in a random spot in the border of the
sandbox and move in a single direction.
This direction is randomly chosen inside a radius of X degrees
based on the direction of this spot to the player's current position.