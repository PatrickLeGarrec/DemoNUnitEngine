
using NUnitWrappers;


NUnitExplorer nUnitExplorer = new("Test.dll");

nUnitExplorer.Explore();

nUnitExplorer.Archive();
