using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace RoslynScriptingDemo
{
  /// <summary>
  /// </summary>
  /// <remarks>
  ///   Targeted .net 4.6.2
  ///   Added nuget soruce https://www.myget.org/F/roslyn-nightly/
  ///   Added nuget added Microsoft.CodeAnalysis.CSharp.Scripting.1.2.0-beta-20151118-02
  ///   Looked at http://www.jayway.com/2015/05/09/using-roslyn-to-build-a-simple-c-interactive-script-engine/#comment-359503
  /// </remarks>
  internal class Program
  {
    private static void Main(string[] args)
    {
      var t1 = new Task(new Program().TestMethod);
      t1.RunSynchronously();
      //t1.Wait();
      Console.WriteLine("done");

      Globals globals = new Globals();
      var task = CSharpScript.RunAsync("var one = 1;", ScriptOptions.Default, globals, globals.GetType());
      var task2 = task.Result.ContinueWithAsync("var two = 2;");
      var last = task2.Result.ContinueWithAsync("Final = one * two;");
      
      last.Wait();
      Console.WriteLine("Variables");
      foreach (var variable in last.Result.Variables.Select(variable => variable.Name + ":" + variable.Value))
      {
        Console.WriteLine(variable);
      }
      Console.WriteLine("Global.Final: " + globals.Final);
      Console.ReadKey();
    }

    public async void TestMethod()
    {
      var state = await CSharpScript.RunAsync("int X = 100;").Result.ContinueWithAsync("int y = 200;");
      Console.WriteLine(state.ReturnValue);
    }
  }

  public class Globals
  {
    public int Final;
  }
 
  }