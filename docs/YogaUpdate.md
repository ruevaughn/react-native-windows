This document contains an overview of Yoga nuget creation.

## Yoga submodule

Yoga is available in `RNW` as submodule from [here](https://github.com/ReactWindows/yoga)

## Nuget Creation

Yoga sources are located here: `.\Yoga`.

Nuspec file is located here: `.\nuget`.

Steps to create NuGet package:
- Build `Facebook.Yoga.csproj` located here: `.\Yoga\csharp\Windows\Facebook.Yoga`
- Build native located here: `.\Yoga\csharp\Yoga`
- Update version inside `\nuget\Facebook.Yoga.nuspec` if needed
- run `.\nuget.exe .\Facebook.Yoga.nuspec` from `.\nuget` folder
- Upload generated package `.\nuget\Facebook.Yoga.<your_version>.nupkg` to  [MyGet](https://www.myget.org/feed/Packages/bluejeans)
- Inside ReactNative solution upldate Facebook.Yoga nuget for Playground.Net46 and ReactNative.Net46.Tests

