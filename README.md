# RoslynScriptDemo
Example of using Roslyn scripting 

Uses nuget package: 
package id="Microsoft.CodeAnalysis.CSharp.Scripting" version="1.2.0-beta-20151118-02" targetFramework="net461"

and all its friends.

I did this
* Targeted .net 4.6.1
* Added nuget soruce https://www.myget.org/F/roslyn-nightly/
* Added nuget package Microsoft.CodeAnalysis.CSharp.Scripting.1.2.0-beta-20151118-02
* Looked at http://www.jayway.com/2015/05/09/using-roslyn-to-build-a-simple-c-interactive-script-engine/#comment-359503
* then discovered it threw an exception. Made it happy by creating a Globals class with a property.
* More exceptions, messed around with api till it worked.
