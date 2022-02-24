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

    public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
    {
        // 无用
        foreach (var variable in node.Variables)
        {
            if (variable.Initializer != null)
            {
                var value = variable.Initializer!.Value!;
                
                switch(value.Kind())
                {
                    case SyntaxKind.ObjectCreationExpression:
                    {
                        var objCreation = value as ObjectCreationExpressionSyntax;
                        var typeSyntax = objCreation!.Type;
                        var typeString = typeSyntax.ToString();
                        if (typeString is "Sql" or "PetaPoco.Sql")
                        {
                            // println or counts
                            foreach (var arg in objCreation!.ArgumentList!.Arguments)
                            {
                                var exprType = arg.Expression.Kind();
                                switch (exprType.ToString())
                                {
                                    case "StringLiteralExpression":
                                        Console.WriteLine(arg);
                                        break;
                                    default:
                                        Console.WriteLine("Argument Expression Kind: " + exprType);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine(typeString);
                        }
                        break;
                    }
                    case SyntaxKind.InvocationExpression:
                        var invocation = value as InvocationExpressionSyntax;
                        Console.WriteLine(invocation);
                        break;
                    default:
                       Console.WriteLine("Value Kind: " + value.Kind());
                       break;
                }
            }
        }
    }
}