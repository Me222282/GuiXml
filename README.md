# GuiXml

This is a simple program that converts xml files used for loading with the Zene GUI library, into C# code to be compiled ahead of time.

### Usage
The project with the xml files must have been compiled before GuiXml is run.
This is due to it relying on dotnet assemblies to do error checking.
The Zene Structs, Graphics, Windowing and GUI libraries must also be referenced by the project assembly.
GuiXml searches for the first csproj file along or above the folder with the first passed xml file.
It then relies on the standard dotnet 8.0 folder structure and location for bin to find the assmeblies.

The arguments to the program are as follows:
```
guixml <xml_files>.. [--abs] [--] [<event_types>..]
```

**\<xml_files\>** specify all the xml files that are to be converted.
It is presumed that they are all apart of the same assembly.
If not, you can run GuiXml separately for each one.

The \-\- must be included for **\<event_types\>** to be used

**\<event_types\>** specify all types where event methods are to be searched for.
If a type is non-static, a reference to an instance will be required in the generated function regardless of whether it is needed or not.  
*(This is just my lazyness. You can easily remove any unused arguments from the generated file.)*  
*(Required using statements may also be missing from the file, and some may be unused.)*

**\[\-\-abs\]** is used if you want all types to referenced with their namespace, excluding ones defined in the project's root namespace.
This removes all using statements and resolves the issue of missing using statements.

The generated function does not clear the contents of the root element passed, nor does it start a group action.
If you want either of these to occur, either add it to the function manually, or call them before the function.  
Group actions is recommended for complex GUIs as it removes unnecessary layout calculations.