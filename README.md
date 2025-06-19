# Maui Developer Sample

A collection of .NET MAUI sample pages and controls demonstrating sensor access, device capabilities, and UI components for cross-platform development.

## Features

- **Sensor Bindables**: Easily bind to device sensors (Accelerometer, Gyroscope, Compass, etc.)
- **Custom Views**: Visualize sensor data with indicators and dynamic image/label controls
- **Vibration & Haptics**: Trigger device vibration and haptic feedback
- **MVVM-Friendly**: All capabilities exposed via bindable properties and commands

## Project Structure

- `Bindables/` — Bindable wrappers for sensors and device features
- `Pages/Sensors/` — Sample pages and custom views for sensor data
- `Pages/Vibrations/` — Vibration and haptic feedback demos
- `Converters/` — Value converters for UI binding

## Getting Started

### Prerequisites

- [.NET 8+](https://dotnet.microsoft.com/)
- [MAUI workload installed](https://learn.microsoft.com/dotnet/maui/get-started/installation)
- Visual Studio 2022+ or JetBrains Rider 2025.1

### Build & Run

1. Clone the repository:
   ```sh
   git clone https://github.com/framinosona/Maui-Developer-Sample.git
   cd Maui-Developer-Sample
    ```
   
2. Open the solution in your IDE.
   
3. Restore NuGet packages and build the project.
   
4. Deploy to your preferred platform (Android, iOS, macOS, Windows).
   
## Usage

- Browse the sample pages to see live sensor data and UI components.
- Use the bindable classes and custom views in your own MAUI projects.
   
## License

MIT License