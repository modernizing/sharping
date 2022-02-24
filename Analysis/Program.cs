using Analysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

SharpingClassVisitor analysis(string code)
{
    SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
    var root = (CompilationUnitSyntax) tree.GetRoot();

    var sharpingClassVisitor = new SharpingClassVisitor(new Domain());
    sharpingClassVisitor.Visit(root);
    return sharpingClassVisitor;
}

String programText = "var inmemorylist = db.Fetch<article>(\"SELECT * FROM Articles\")";

var structCollector = analysis(programText);
Console.Write(structCollector.domain);