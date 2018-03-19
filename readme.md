## Environment required
- .Net Core 2.0

## Instruction to run the program
- In the root folder, run 'dotnet restore'
- then enter the console app folder by 'cd LovelyCats.Directory'
- then run 'dotnet run'

## Discussion
### The declaration of the .Net console app
In the requirement, processing data and listing data would be the primary objective. Console application would be sufficient to meet the requirement. The design principle in this project is to minimize the development cost for future requirement change, and some patterns are used here.
- Inversion of Control(IoC)
- Command and Query Responsibility Separation (CQRS)

### The declaration of the test
The application relies on an external data source, and the unit test would not be sufficient to test the integration of external data. At the same time, writing units for every single functional unit would be time-consuming and still have blind spots. Therefore, only integration test on weakest point of the system is contained in the testing project.