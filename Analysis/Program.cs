using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

String programText ="Console.WriteLine(\"Hello, World!\");";

SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
var root = (CompilationUnitSyntax)tree.GetRoot();

foreach (var member in root.Members)
{
    Console.WriteLine(member.Modifiers);
    foreach (var attribute in member.AttributeLists)
    {
           
    }
}
