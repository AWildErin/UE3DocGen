# UE3DocGen
A documentation generator for Unreal Engine 3.

## Why?
Unreal Engine 3 currently has some documentation, but as projects rise making use of leaked licensee copies of Unreal Engine 3, I wanted to have a system to document the code.  

The idea is to deliver something similar to UE4's documentation, a mix between written articles and auto-generated C++ API documentation. UE3DocGen is the core component to this process, generating and converting the documentation pages into HTML.

At the moment, the repository containing all the documentation doesn't exist, we are developing this tool first. Once it is, it will be linked here.

## Contributing
We allow any and all contributions! Please bear in mind this repository is only the core for the documentation generation, and all the actual documentation pages are stored in a separate repository.

## Building
### Requirements
- .NET 7
- An IDE capable of building .NET projects (like VS2022 or similar)

## Testing
As we have C++ documentation generation, we use Doxygen to grab all the code, as such you must have Doxygen on your computer *only* if you intend to work with the C++ generation. This is an optional component to UE3DocGen and not vital for running development builds.

### Doxygen
Before running tests, you must install doxygen into `TestResources/Doxygen`. UE3DocGen will run doxygen as part of it's generation process.