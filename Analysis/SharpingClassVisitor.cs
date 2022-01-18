using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Analysis;

public class SharpingClassVisitor: CSharpSyntaxWalker
{
    private SClass currentClass = new SClass();
    public Domain domain { get; set; }

    public SharpingClassVisitor(Domain domain)
    {
        this.domain = domain;
    }
    
    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        currentClass.name = node.Identifier.Text;
        domain.classes.Add(currentClass);
    }
}