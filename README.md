# RasterSharp

RasterSharp is a .NET Standard library that enables developers to write cross-platform image manipulation and drawing code which does not depend on any particular graphics library. Using in-memory bitmaps as the only I/O, interoperability is easily achieved with all common graphics libraries.

> ⚠️ This project is pre-alpha and is not intended for public use

## Principles
* Interoperable with popular graphics libraries:
  * System.Drawing.Common
  * SkiaSharp
  * ImageSharp
* Favor simplicity and interoperability over performance
* Targets .NET Standard 2.0 (supporting .NET Framework and .NET Core)
* No dependencies (minimizing security risk)
* Small code base (easy to review)
* MIT licensed (no usage restrictions)

## Features
* Included functionality for applying colormaps to single-channel images
* Basic drawing operations (lines, rectangles, etc.)
* Enhanced support for scientific images
  * Optional floating point pixel intensity
  * 5-dimensional image types (X, Y, C, Z, T)
  * Multi-dimensional image operations (projection, difference, etc.)

## Not Features
* Rendering text using system fonts
* Anti-aliased drawing
* Hardware-accelerated graphics processing