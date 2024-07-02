using NUnit.Engine;
using System.Xml;

namespace NUnitWrappers;

public class NUnitExplorer
{
    string _dll, _workSpaceDir, _archiveDir;

    public NUnitExplorer(string dll)
    {
        _dll = dll;
        _workSpaceDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"ConsoleApp\bin\Debug\net8.0", @"WorkSpace");
        _archiveDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"ConsoleApp\bin\Debug\net8.0","Archive");
    }

    public void Explore()
    {
        try
        {
            File.Copy(_workSpaceDir + _dll, AppDomain.CurrentDomain.BaseDirectory + _dll, true);

            ITestEngine engine = TestEngineActivator.CreateInstance();
            TestPackage package = new TestPackage(_dll);
            ITestRunner runner = engine.GetRunner(package);
            XmlNode explore = runner.Explore(TestFilter.Empty);         // Dependency on Test.dll is mandatory otherwise the file is not found. (and see note below).
                                                                        // 1st Issue => That means we can't load unknown nUnit dlls ?  
            Console.WriteLine(explore.OuterXml.ToString());

            runner.Unload();
            runner.Dispose();
            engine.Dispose();
        }
        catch (Exception ex) { Console.WriteLine("\n" + ex.Message); }
    }

    public void Archive()
    {
        try
        {
            File.Copy(_workSpaceDir + _dll, _archiveDir + "explored_" + _dll, true);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + _dll);    // 2nd Issue => file is Locked
                                                                          // Note: without dependency on Test.dll, runner.explore failed (file not found) but does not lock the file.
        } catch (Exception ex) { Console.WriteLine("\n" + ex.Message); }
    }
}


