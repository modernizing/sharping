using Analysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

String programText ="class Hello { Console.WriteLine(\"Hello, World!\"); } ";

SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
var root = (CompilationUnitSyntax)tree.GetRoot();

var structCollector = new SharpingClassVisitor(new Domain());
structCollector.Visit(root);
Console.Write(structCollector.domain);
