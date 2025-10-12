# Martian Robots (TDD + Extensible)

## Why this stands out
- Built with **TDD**: each feature starts with failing tests.
- Extensible via **Command**, **Strategy**, **Factory**, and **Parser** abstractions.
- Includes **XML docs**, error handling, logging, property-style randomized tests, and a pluggable **Visualizer** (ASCII) which can be replaced by a web UI renderer.

## Run
dotnet build
dotnet test
dotnet run --project MartianRobots -- sample_input.txt