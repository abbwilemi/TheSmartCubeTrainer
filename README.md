# Rubik’s Cube Solver & Visualizer in Unity

## Overview
This project implements a 3D Rubik’s Cube solver and visualizer in Unity, allowing the user to interact with the cube, visualize its state, and solve it automatically. The system features a series of components to simulate and manipulate the cube, including a raycasting mechanism to detect the current state, user input controls to rotate the cube, and an automated solver based on Kociemba’s Algorithm.

## User Interface
![TheSmartCubeTrainer UI](https://github.com/user-attachments/assets/4f4531c6-30a2-4fe5-bbee-45d0242725d2)


### Features
- **CubeState**: Tracks the current state of the Rubik’s Cube.  
- **CubeVisualizer**: Displays the cube and updates the colors based on its current state.  
- **CubeRayCaster**: Uses raycasting to detect and capture the state of the cube.  
- **CubeRotator**: Allows the user to rotate the cube interactively with mouse input or swipe gestures.  
- **CubeFaceSelector**: Lets the user select a face and perform specific rotations.  
- **CubeAutoSolver**: Solves the cube automatically using the Kociemba Algorithm.  
- **CubeAutomater**: Scrambles the cube by generating random moves.  
- **AxisRotation**: Provides rotation functionality for individual axis-based rotations.

## Project Components
The project is structured around 8 main scripts that work together to manage the cube's state, allow interaction, and solve the cube:

1. **AxisRotation**  
   Handles individual axis-based rotations for the cube.

2. **CubeAutomater**  
   Scrambles the cube by generating random rotations.

3. **CubeAutoSolver**  
   Solves the cube automatically using Kociemba’s Algorithm.

4. **CubeFaceSelector**  
   Allows users to manually rotate individual faces of the cube.

5. **CubeRayCaster**  
   Detects the positions of cube pieces and their current alignment by casting rays.

6. **CubeRotator**  
   Handles cube rotations by dragging with the mouse or swiping on touch devices.

7. **CubeState**  
   Holds the state of the cube’s faces (up, down, left, right, front, back).

8. **CubeVisualizer**  
   Displays the cube in Unity and updates its colors based on the state of each face.

## Getting Started

### Requirements
- **Unity**: Version 2020.x or later  
- **Basic knowledge** of Unity and C# programming  

### Installation

1. **Download or Clone the Project**  
   You can either download the project directly or clone it from a repository (replace the URL with your own):

   ```bash
   git clone https://github.com/your-username/your-repository.git

2. **Open the Project in Unity**

- Open Unity and select Open Project.*** 
- Navigate to the folder where you downloaded or cloned the project.

3. **Import Prefabs and Components**

- Make sure all necessary prefabs for the cube’s faces and associated GameObjects are correctly placed in your Unity Editor.  
- Ensure that all scripts are attached to the appropriate GameObjects in your scene.

4. **Setting Up the Ray Origins**

- The ray origins for each face are instantiated and managed through the **CubeRayCaster** script.  
- Make sure the prefabs for the ray origin points are correctly configured to detect and interact with the cube faces.

## Usage

Once the project is set up in Unity, the following features are available:

### Interact with the Cube

- **Rotate Cube**: Hold the right mouse button and drag to rotate the entire cube in 3D space.  
- **Swipe Gestures**: On a touchscreen device or using a touchpad, swipe to rotate the cube in specific directions (left, right, up, down).

### Cube Rotation and Interaction

- **Manual Face Selection**: Use the **CubeFaceSelector** component to interact with specific faces of the cube. Select a face and rotate it.  
- **View the Cube’s State**: The cube is displayed with colors corresponding to each face (e.g., Red, Orange, Yellow, White, Green, Blue).

### Scramble the Cube

- **Scramble Button**: Call the scramble method to generate random rotations. The **CubeAutomater** can generate these random moves automatically.

### Automatic Cube Solving with Kociemba’s Algorithm

The cube can be solved automatically using **Kociemba’s Algorithm**, a well-known two-phase algorithm used to find the shortest solution to a scrambled Rubik’s Cube.

## Understanding Kociemba’s Algorithm

### Kociemba’s Algorithm

Kociemba’s Algorithm is one of the most popular algorithms for solving a Rubik’s Cube efficiently. It produces much shorter solutions (in terms of moves) than many other methods.

### The Two Phases

1. **Phase 1**  
   Reduce the cube’s configuration to a state where the top and bottom layers are correctly aligned (only the middle layer pieces remain scrambled). Moves affect only the middle layer without disturbing the top and bottom layers.  
2. **Phase 2**  
   Solve the cube by affecting only the top and bottom layers. The middle layer is already solved, so the remaining moves finish the cube.

The algorithm uses a search tree to explore possible states and minimize the number of moves required to reach the solved state.

### Implementation Details

- The **CubeAutoSolver** script implements the Kociemba algorithm.  
- It converts the current cube state into a string representation.  
- The algorithm computes an efficient sequence of moves.  
- The cube then performs these rotations step-by-step until the solved state is reached.

### Kociemba’s Algorithm Source

The implementation is sourced from a GitHub repository providing the core Unity integration for Kociemba’s Algorithm, enabling automatic Rubik’s Cube solving.

Repository: https://github.com/Megalomatt/Kociemba/tree/Unity

Wiki: https://www.speedsolving.com/wiki/index.php/Kociemba%27s_Algorithm

### Why Kociemba’s Algorithm?

- **Efficiency**: Requires fewer moves than many other algorithms.  
- **Two-Phase Solution**: Splits the problem into manageable phases.  
- **Widely Used**: Proven, reliable, and common in most cube-solving applications.

## How the Scripts Work Together

- **CubeState**  
  Manages the cube’s state, representing each face with a list of GameObjects. Updates on user interaction or scramble.  
- **CubeVisualizer**  
  Updates face colors based on the current state in **CubeState**.  
- **CubeRayCaster**  
  Casts rays to detect piece positions and alignments for state reading and updating.  
- **CubeRotator**  
  Handles dragging and swipe gestures for rotating the cube or individual faces.  
- **CubeFaceSelector**  
  Allows selecting and rotating individual faces on user input.  
- **CubeAutoSolver**  
  Implements Kociemba’s Algorithm, generating and executing the solution moves.  
- **CubeAutomater**  
  Scrambles the cube by generating random move sequences.  
- **AxisRotation**  
  Performs precise axis-based rotations for specific movements.

## Future Improvements

- **Optimization**: Improve performance for larger cubes (4×4, 5×5).  
- **Support for Other Algorithms**: Implement Fridrich’s Method (CFOP) and others.  

## License

This project is licensed under the MIT License. See the **LICENSE** file for details.

## Conclusion

This project provides a fully interactive Rubik’s Cube solver and visualizer in Unity, supporting both manual and automatic solving via Kociemba’s Algorithm. Users can manipulate the cube via mouse or touch and watch it solve step by step.


**Contact**  
William Emilsson  
Email: william.emilsson@hitachigymnasiet.se


