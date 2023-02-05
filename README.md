# OneGlassBackend
In Archituctire I chose I focused on seperation of concerns for follwing reasons:
Separation of concerns is a software design principle that states that every module or component in a software system should have a single, well-defined responsibility. In the context of a service model controller in a C# .NET backend, this means that the controller should be responsible only for handling HTTP requests and returning HTTP responses, while all other logic should be separated into other components or modules.

By adhering to this principle, the codebase is kept clean and modular, making it easier to maintain, test, and extend. Additionally, it promotes reusability by allowing the same components to be used in multiple parts of the system, reducing code duplication.

In a C# .NET backend, you can implement separation of concerns by using a layered architecture, where the controller communicates with service and repository layers to perform the required operations and retrieve data from the database. This way, the controller remains focused on its main responsibility of handling HTTP requests and returning HTTP responses, while other logic is abstracted away into other layers of the system.
