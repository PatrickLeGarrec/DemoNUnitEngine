
using NUnitWrappers;


NUnitExplorer nUnitExplorer = new("Test.dll");

nUnitExplorer.Explore();

nUnitExplorer.Archive();




//   How this proof of concept works  : 
/*
 * 
 *   The idea is to have a workspace where the fresh tests to be explored (and executed later) are forged in Tests.dll.
 *   the Test App execute a test and copy its own dll to the workspace. by doing this, it s easier to check the libs compatibility.
 *   
 *   The consoleApp take the test from the workspace, import it into the NUnit workingDir (where the dependencies are), and does the nUnit engine stuff as usual.
 *   
 *   After the exploration has been processed, the Test.dll is archived in the archive folder, and of course deleted from the NUnit workingDir. 
 *   
 *    
 *   I got many issues with dependencies versions and the winning combos are : 
 *   
 *   For the Test :
 *   Net.test.sdk  17.10.0
 *   NUnit 3.14.0
 *   Nunit3TestAdapter 4.3.0
 *   
 *   For the ConsoleApp (nUnit engine stuff) :
 *   Net.test.sdk 17.10.0
 *   NUnit 3.14.0
 *   NUnit Engine 3.14.0
 *   
 *   maybe i could be successull with earlier versions but most of the others combos have failed. (File not found during engine.explore)
 * 
 *   with those libs the Test.dll is correcly read. (without the mess of the 3.17 engine version / hypothetic dependency on the Test.dll itself like as described, forget that.)
 *   
 *   But the file is still locked when deleting.
 *   
 * 
 *   Patrick.
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */