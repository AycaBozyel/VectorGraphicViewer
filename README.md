# Vector Graphic Viewer

Vector Graphic Viewer is a simple application that reads vector graphic data in JSON format and renders it in a scalable manner using a WPF interface.

## Overview
The application reads a JSON file containing vector shapes and displays them on the screen. The rendering should be scalable so that the entire image fits within the window while maintaining proportions.

## Shapes.json File

### 1. File Location
The `shapes.json` file should be located in the same directory as the executable file. If a different directory is used, the relevant settings within the application should be updated accordingly.

### 2. File Format
`shapes.json` is a JSON-formatted file that defines vector graphics. Each shape must include specific attributes.

#### Example JSON Content:
```json
[
    {
        "type": "line",
        "a": "-1,5; 3,4",
        "b": "2,2; 5,7",
        "color": "127; 255; 255; 255"
    },
    {
        "type": "circle",
        "center": "0; 0",
        "radius": 15.0,
        "filled": false,
        "color": "127; 255; 0; 0"
    },
    {
        "type": "triangle",
        "a": "-15; -20",
        "b": "15; -20,3",
        "c": "0; 21",
        "filled": true,
        "color": "127; 255; 0; 255"
    },
    {
        "type": "line",
        "A": "0;0",
        "B": "100;100",
        "Color": "255;0;0;255"
    },
    {
        "type": "rectangle",
        "A": "50;50",
        "B": "20;15",
        "color": "234;53;12;79",
        "filled": true
    }
]
```

### 3. Supported Shapes
Currently, the following shapes are supported:
- **Line**: `type: "line"`, `a`, `b`, `color`
- **Circle**: `type: "circle"`, `center`, `radius`, `filled`, `color`
- **Triangle**: `type: "triangle"`, `a`, `b`, `c`, `filled`, `color`
- **Rectangle**: `type: "rectangle"`, `A`, `B`, `color`, `filled`

### 4. Color Format
Colors must be defined in `A; R; G; B` (Alpha, Red, Green, Blue) format.

### 5. Scaling and Rendering
- The coordinate system follows the Cartesian plane, where the Y-axis points upward.
- Units are virtual; if the image exceeds the screen size, it should be proportionally scaled down to fit within the window.
- If the zoom level is set to 100%, one unit equals one pixel.
- If the `filled` flag is `true`, the shape should be rendered with both a border and fill. Otherwise, only the border should be drawn.
- An arbitrary border width can be used.

### 6. Loading the File
When the application starts, it reads the `shapes.json` file and renders the specified shapes. If the file is missing or incorrectly formatted, an error message may be displayed.

### 7. Using the JSON File in Code
The following code snippet demonstrates how to load the `shapes.json` file:
```csharp
private readonly string fileName = "shapes.json";
var shapes = shapeLoader.LoadShapes(fileName);
```
This code reads the JSON file using the `shapeLoader` component and loads the shapes accordingly.

### 8. Extensibility
Special effort has been made to ensure that the solution is extensible. In particular:
- New types of primitives (e.g., rectangles) can be added in the future without significant changes to the core architecture.
- The system is designed to support additional data formats if needed.

---

This README explains the structure and usage of the `shapes.json` file. For more details, refer to the relevant sections of the code.

