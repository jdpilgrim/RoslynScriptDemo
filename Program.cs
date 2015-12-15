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
  ///   Targeted .net 4.6.1
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
      Console.ReadKey();
    }

    public async void TestMethod()
    {
      var globals = new Globals();
      var task = CSharpScript.RunAsync("var two = 2;", ScriptOptions.Default, globals, globals.GetType());
      var task2 = task.Result.ContinueWithAsync("var three = 3;");
      var finalState = await task2.Result.ContinueWithAsync("Final = two * three;");

      Console.WriteLine("Variables");
      foreach (var variable in finalState.Variables.Select(variable => variable.Name + ":" + variable.Value))
      {
        Console.WriteLine(variable);
      }
      Console.WriteLine("Global.Final: " + globals.Final);
    }
  }

  public class Globals
  {
    public int Final;
  }
}
