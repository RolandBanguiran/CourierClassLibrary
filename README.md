# CourierClassLibrary
A sample class library to demonstrate Decorator Pattern and Unit Testing.

## Assumptions
- The library will be consumed by either .Net Framework or .Net Core application
- Parcel type dimensions and cost can be retrieved and updated from some kind of persistence framework in the future
- Client app will be the one to implement the necessary repositories
- Dimensions (cm) and weights (kg) will be rounded to nearest integer for simplicity
- Different parcel type can have different extra weight charge per kg

## The next steps...
- Implement Medium and Mixed Parcel Mania discount calculator (It should be similar implementation as Small Parcel Mania calculator)
- Support floating point values for dimensions (cm) and weights (kg)
- Implement an IServiceCollection extension for .Net Core Dependency Injection
- Due to limited time, repository branching and pull requests are not observed. Should be done in realworld scenario.
- Add code comments / documentation
- More unit test coverage. Should include all critical classes and methods.
