# Maui Developer Sample

A collection of .NET MAUI sample pages and controls demonstrating sensor access, device capabilities, and UI components for cross-platform development.

## Features

- **Sensor Management**: Complete bindable services for device sensors (Accelerometer, Gyroscope, Compass, Barometer, Magnetometer, OrientationSensor)
- **Custom Sensor Views**: Rich visualizations including bidirectional indicators, rotation indicators, and real-time value displays
- **Haptic Feedback**: Advanced haptic feedback system with platform-specific implementations
- **Vibration Control**: Device vibration capabilities with customizable patterns
- **Custom Graphics**: Arc drawing and other custom UI components
- **MVVM-Friendly**: All capabilities exposed via bindable properties and commands

## Project Structure

### Main Application
- `Maui-Developer-Sample/` — Main MAUI application
  - `Pages/Sensors/` — Sensor demo pages (Accelerometer, Gyroscope, Compass, Barometer, Magnetometer, OrientationSensor)
  - `Pages/AppCapability/` — Device capability demos (Vibration, Haptic Feedback)
  - `Pages/UI/` — Custom UI component demos (Arc Drawing)
  - `Helpers/` — Utility classes and bindable object helpers

### Sensor Management Library
- `Framinosona.SensorsManager/` — Dedicated library for sensor management and bindable services

### Haptic Feedback Library  
- `Framinosona.HapticFeedback/` — Cross-platform haptic feedback implementation with platform-specific services

## Demo Pages

### Sensor Demos
- **Accelerometer**: Real-time acceleration data with visual indicators
- **Gyroscope**: Angular velocity measurements with rotation visualization
- **Compass**: Magnetic compass with directional indicators
- **Barometer**: Atmospheric pressure readings
- **Magnetometer**: Magnetic field strength measurements
- **Orientation Sensor**: Device orientation with 3D visualization

### Device Capability Demos
- **Vibration**: Various vibration patterns and intensities
- **Haptic Feedback**: Platform-specific haptic responses

### UI Component Demos
- **Arc Drawing**: Custom graphics view for drawing arcs and curves

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

Explore the comprehensive sensor and device capability demos:

### Sensor Pages
Navigate through individual sensor pages to see:
- Real-time sensor data visualization
- Custom indicator controls showing sensor values
- Bindable services that can be integrated into your own projects

### Device Capability Pages  
Test platform-specific features:
- Vibration patterns with different intensities and durations
- Haptic feedback responses for enhanced user interaction

### UI Component Demos
Discover custom graphics and UI elements:
- Interactive arc drawing components
- Reusable visual indicators and controls

### Integration
- Use the sensor services (`Framinosona.SensorsManager`) in your own MAUI projects
- Integrate haptic feedback (`Framinosona.HapticFeedback`) for enhanced user experience
- Leverage the custom view components for rich data visualization
   
## License

MIT License