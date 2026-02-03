# tetrix
**Tetrix is a cross-platform Tetris clone written in C# for the console.**
It is built on a custom game engine and currently under active development.

---

## Requirements

- Visual Studio
- DOTNET 8.0 SDK

> **Note:**  
> On Windows, pre-built executable files are already included. Rebuilding is optional.

---

## Installation

### Clone the repository
```bash
git clone https://github.com/P1wka/tetrix.git
cd tetrix
```

## Running the game
🪟 Windows

1. Open ```tetrix.sln``` file with Visual Studio,
2. Build the project
3. Run tetrix.exe from:
```bash
tetrix/tetrix/bin/Debug/net8.0/
```
🐧 Linux

1. Open tetrix.sln with Visual Studio
2. Publish the project:
```bash
dotnet publish --runtime linux-x64
```
3. Navigate to:
```bash
tetrix/tetrix/bin/Release/net8.0/linux-x64/
```
4. Run from terminal

---

## Commands

| Command                          | Description           |
| -------------------------------- | --------------------- |
| `run`, `play`, `start`, `tetrix` | Start the game        |
| `help`, `-h`, `--help`           | Show all commands     |
| `developer`, `-d`, `--developer` | Developer information |
| `version`, `-v`, `--version`     | Show game version     |

---

## Notes
This project is under active development.
Current version is ```v0.0.1```.

---

## Gameplay

🐧 Linux: 

<img width="1079" height="600" alt="screenshot_linux" src="https://github.com/user-attachments/assets/2e61db34-f8ac-4e90-a28e-98f935d8ad63" />

🪟 Windows: 

<img width="937" height="620" alt="Ekran görüntüsü 2026-02-03 205526" src="https://github.com/user-attachments/assets/ee00ce0f-291c-49f4-b2c4-288169bca6fc" />


