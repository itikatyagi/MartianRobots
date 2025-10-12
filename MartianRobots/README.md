---

# Martian Robots

*C# .NET • Clean Architecture • Extensible Design*

A **cleanly architected**, **extensible** C# implementation of the *Martian Robots* simulation.
This project demonstrates **SOLID principles** and design patterns such as **Command**, **Strategy**, **Factory**, **Dependency Injection**, and **Single Responsibility**.

---

## Problem Overview

Mars is modeled as a rectangular grid. Robots are deployed at specific coordinates and follow a sequence of commands:

- `L` -> Turn left
- `R` -> Turn right
- `F` -> Move forward one grid unit

If a robot moves off the grid, it becomes **LOST** and leaves a "scent" at its last valid position and orientation. This prevents future robots from repeating the same fatal move.

---

## Key Design Patterns & Architecture

| Pattern                   | Purpose                                                                                                         |
| ------------------------- | --------------------------------------------------------------------------------------------------------------- |
| **Command**               | Encapsulates each robot instruction (`L`, `R`, `F`) in an `ICommand` class                                      |
| **Strategy**              | Implements direction logic (`N`, `E`, `S`, `W`) via interchangeable `IDirection` strategies                     |
| **Factory**               | Centralizes creation logic in `CommandFactory` and `DirectionFactory`                                           |
| **Dependency Injection**  | Enables flexible composition in `SimulationRunner` by injecting `IInputParser`, `TextWriter`, and `IVisualizer` |
| **Single Responsibility** | Ensures each class has one clear purpose (SRP)                                                                  |

This separation allows you to **replace or extend components** independently (e.g., swap the ASCII visualizer for a web UI).

---

## How to Run

### CLI

```bash
dotnet build
dotnet test
dotnet run --project MartianRobots -- sample_input.txt
```

If no input file is provided, input can be entered **via console**.

> **Important:** After typing the input, press **Ctrl+Z** (Windows) + **Enter** to signal end-of-input.

### Visual Studio

1. Set `MartianRobots` as **Startup Project**
2. Press **Ctrl+F5** to run
3. Provide input file path in **Debug -> Application Arguments**, or type input in console with **Ctrl+Z + Enter** to finish.

---

## Example Input / Output

### Input

```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

### Output

```
1 1 E
3 3 N LOST
2 3 S
```

---

## Extensibility Guide

| Feature            | How to Extend                                                        |
| ------------------ | -------------------------------------------------------------------- |
| **New Command**    | Implement `ICommand` and register it in `CommandFactory`.            |
| **New Direction**  | Implement `IDirection` and register it in `DirectionFactory`.        |
| **New Parser**     | Implement `IInputParser` (e.g., JSON, XML, or interactive CLI).      |
| **New Visualizer** | Implement `IVisualizer` to render robot states in GUI or Web UI.     |
| **Logging**        | Inject a custom `ILogger` to capture robot movements or diagnostics. |

### Example - Adding a Backward Command

```csharp
public class MoveBackwardCommand : ICommand
{
    public void Execute(Robot robot, Grid grid)
    {
        var (dx, dy) = robot.Direction.MoveVector();
        robot.TryMoveTo(robot.X - dx, robot.Y - dy, grid);
    }
}

// Register in CommandFactory
case 'B': return new MoveBackwardCommand();
```

---

## Performance & Edge Cases

- Each robot executes commands in **O(N)** time, N = number of commands.
- `HashSet` used for **scent tracking** -> O(1) lookups.
- Validates **input format** and command characters.
- Handles **edge cases**: moving off grid, repeated scents, invalid directions, empty or malformed input.
- Logging can be injected without changing core logic.

---

## TDD Commit Flow

1. `chore: initialize solution and test projects`
2. `test: add grid validation tests`
3. `feat: implement Grid`
4. `test: add robot movement & LOST tests`
5. `feat: implement Robot & Directions`
6. `test: add command tests`
7. `feat: implement Command pattern`
8. `feat: add Parser and SimulationRunner`
9. `feat: add ASCII Visualizer & logging`
10. `docs: add README with design rationale and run instructions`

> Each commit builds incrementally on the previous, following **TDD** principles

---

