# CNC Controller (C# + Arduino Firmware)

> ⚠️ **[Warning]** This project is no longer in active development.  
> Originally developed as a **Bachelor’s thesis**.

A custom CNC control system consisting of a C# Windows Forms application (built on .NET Framework) and Arduino-based firmware (`CNC_V10`). The software allows manual and automated control of a CNC machine using a basic G-code interface.

---

## 🖥️ PC Application (C# Windows Forms)

### ✨ Features

- Manual CNC control (jogging)  
- Homing ,centering,Z calibration routines  
- Enable/disable stepper motors  
- Send G-code lines to the machine  
- Compatible with firmware version **V10 or higher**  
- Custom command protocol specific to the included firmware  

### ⚠️ Limitations

- No validation of G-code input  
- No offset handling (e.g., G54, G92 not supported)  
- Limited machine feedback (e.g., no live status, error reporting)

---

## 🔧 Arduino Firmware (`CNC_V10`)

### ✨ Features

- Basic G-code handling:
  - Supported commands: `G0`, `G1`, `G2`, `G3`, `G17–G19`  
- Stepper motor control using the **AccelStepper** library  

### ⚠️ Limitations

- No support for helical movements  
- Single datum only (no work offsets)  
- Inefficient handling of arc commands (`G2/G3`)  
- `R`-parameter arcs unsupported  
- No precise RPM control (`%V` ≠ real motor speed)  

---

## 📝 Notes

This project was developed for educational purposes as part of a bachelor's thesis. It demonstrates foundational CNC control concepts but lacks the robustness and safety checks required for production environments.

---
