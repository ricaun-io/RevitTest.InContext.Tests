# RevitTest.InContext.Tests

[![Revit 2021](https://img.shields.io/badge/Revit-2021+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

This project contains tests to benchamark some implementations to check if Revit is in Context using the [ricaun.RevitTest](https://ricaun.com/RevitTest) Framework.

## Tests

* `InAddInContext`: Check if Revit is in Context using the `UIApplication` object to get the `ActiveAddInId`, is null when outside Revit API context.
* `InApiMode`: Check if Revit is in Context using internal method inside the assembly `APIUIAPI` using the method `isRevitInAPIMode` to check if is in API mode.
* `InContext`: Check if Revit is in Context using the `UIApplication` object to register a `Idling` event, exception happen if is outside Revit API context.

## Revit API Forum

* [How to know if Revit API is in Context.](https://forums.autodesk.com/t5/revit-api-forum/how-to-know-if-revit-api-is-in-context/td-p/12574320)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!