using NUnit.Engine;
using System.Xml;

namespace NUnitWrappers;

public class NUnitExplorer
{
    string _dll, _workSpaceDir, _archiveDir;

    public NUnitExplorer(string dll)
    {
        _dll = dll;
        _workSpaceDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"ConsoleApp\bin\Debug\net8.0", "WorkSpace");
        _archiveDir = AppDomain.CurrentDomain.BaseDirectory.Replace(@"ConsoleApp\bin\Debug\net8.0","Archive");
    }

    public void Explore()
    {
        try
        {
            File.Copy(_workSpaceDir + _dll, AppDomain.CurrentDomain.BaseDirectory + _dll, true);

            ITestEngine engine = TestEngineActivator.CreateInstance();
            engine.InternalTraceLevel = InternalTraceLevel.Off;
            TestPackage package = new TestPackage(_dll);
            ITestRunner runner = engine.GetRunner(package);
            XmlNode explore = runner.Explore(TestFilter.Empty);         
                                                                        
            Console.WriteLine(explore.OuterXml.ToString());

            runner.Unload();
            runner.Dispose();
            engine.Dispose();
        }
        catch (Exception ex) { Console.WriteLine("\n" + ex.ToString()); }
    }

    public void Archive()
    {
        try
        {
            File.Copy(_workSpaceDir + _dll, _archiveDir + "explored_" + _dll, true);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + _dll);    // file is Locked
                                                                          
        } catch (Exception ex) { Console.WriteLine("\n" + ex.ToString()); }
    }
}

// https://docs.nunit.org/articles/nunit-engine/Test-Engine-API.html

// https://docs.nunit.org/articles/vs-test-adapter/Adapter-Engine-Compatibility.html

// https://docs.nunit.org/articles/nunit/release-notes/Nunit4.0-MigrationGuide.html

