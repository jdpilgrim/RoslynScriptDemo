using System;
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
      CSharpScriptEngine.Execute(
//This could be code submitted from the editor
        @"
    public class ScriptedClass
    {
      public string HelloWorld { get; set; }
      public ScriptedClass()
      {
        HelloWorld = ""Hello Roslyn!"";
      }
    }");
//And this from the REPL
      Console.WriteLine(CSharpScriptEngine.Execute("new ScriptedClass().HelloWorld"));
      Console.ReadKey();
    }
  }

  #region Nested type: CSharpScriptEngine

    public class CSharpScriptEngine
    {
      private static Script _previousInput;

      private static readonly object _globals = new {};

      public static AggregateException Exception { get; private set; }

      public static object Execute(string code)
      {
        Script script;
        if (_previousInput == null)
        {
          script = CSharpScript.Create(code, null, _globals.GetType());
        }
        else
        {
          script = _previousInput.ContinueWith(code);
        }

        var task = script.RunAsync(_globals);
        task.Wait();

        Exception = task.Exception;

        if (Exception == null)
        {
          var endState = task.Result;
          _previousInput = endState.Script;
          return endState.ReturnValue;
        }
        return null;
      }
    }

    #endregion
  }