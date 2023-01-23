using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nmermaid.DocAsTests;

public class ExpressionToCodeSample : ExpressionVisitor
{
    public static string Convert(Expression node, bool replaceEnumVariableToValue = true)
    {
        ExpressionToCodeSample esb = new(replaceEnumVariableToValue);
        esb.Visit(node);
        return esb._sb.ToString();
    }

    private readonly StringBuilder _sb = new();
    private int _indentation = 1;
    private readonly bool _replaceEnumVariableToValue;

    private ExpressionToCodeSample(bool replaceEnumVariableToValue)
    {
        this._replaceEnumVariableToValue = replaceEnumVariableToValue;
    }

    private void VisitExpressions<T>(char open, IList<T> expressions, char close, string seperator = ", ") where T : Expression
    {
        _sb.Append(open);
        if (expressions != null)
        {
            bool isFirst = true;
            foreach (T e in expressions)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    _sb.Append(seperator);
                }
                Visit(e);
            }
        }
        _sb.Append(close);
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
        if (node.Value == null)
        {
            _sb.Append("null");
            return node;
        }

        string? sValue = node.Value.ToString();
        if (node.Value is string)
        {
            _sb.Append("\"");
            _sb.Append(sValue?.Replace("\"", "\\\""));
            _sb.Append("\"");
        }
        else if (node.Type.IsEnum)
        {
            _sb.Append(node.Type.Name + "." + sValue);
        }
        else
        {
            _sb.Append(sValue);
        }

        return node;
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
        int start = 0;
        Expression? ob = node.Object;
        if (Attribute.GetCustomAttribute(node.Method, typeof(ExtensionAttribute)) != null)
        {
            start = 1;
            ob = node.Arguments[0];
        }

        if (ob != null)
        {
            Visit(ob);
            Indent();
            _sb.Append(".");
        }

        if (node.Method.IsStatic)
        {
            _sb.Append(node.Type.Name);
            _sb.Append(".");
        }

        _sb.Append(node.Method.Name);
        _indentation++;
        _sb.Append("(");
        for (int i = start, n = node.Arguments.Count; i < n; i++)
        {
            if (i > start)
                _sb.Append(", ");
            Visit(node.Arguments[i]);
        }
        _sb.Append(")");
        _indentation--;
        return node;
    }

    private void Indent()
    {
        _sb.Append("\n");
        _sb.Append(new string(' ', _indentation * 4));
    }

    protected override Expression VisitNewArray(NewArrayExpression node)
    {
        switch (node.NodeType)
        {
            case ExpressionType.NewArrayBounds:
                // new MyType[](expr1, expr2)
                _sb.Append("new " + node.Type.ToString());
                VisitExpressions('(', node.Expressions, ')');
                break;
            case ExpressionType.NewArrayInit:
                // new [] {expr1, expr2}
                _sb.Append("new [] ");
                VisitExpressions('{', node.Expressions, '}');
                break;
        }
        return node;
    }

    protected override Expression VisitNew(NewExpression node)
    {
        _sb.Append("new " + node.Type.Name);
        _sb.Append("(");
        var members = node.Members;
        for (int i = 0; i < node.Arguments.Count; i++)
        {
            if (i > 0)
            {
                _sb.Append(", ");
            }
            if (members != null)
            {
                string name = members[i].Name;
                _sb.Append(name);
                _sb.Append(" = ");
            }
            Visit(node.Arguments[i]);
        }
        _sb.Append(")");
        return node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitBlock(BlockExpression node)
    {
        throw new NotImplementedException();
    }

    protected override CatchBlock VisitCatchBlock(CatchBlock node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitConditional(ConditionalExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitDebugInfo(DebugInfoExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitDefault(DefaultExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitDynamic(DynamicExpression node)
    {
        throw new NotImplementedException();
    }

    protected override ElementInit VisitElementInit(ElementInit node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitExtension(Expression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitGoto(GotoExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitIndex(IndexExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitInvocation(InvocationExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitLabel(LabelExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitListInit(ListInitExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitLoop(LoopExpression node)
    {
        throw new NotImplementedException();
    }

    private object GetValue(MemberExpression member)
    {
        var objectMember = Expression.Convert(member, typeof(object));

        var getterLambda = Expression.Lambda<Func<object>>(objectMember);

        var getter = getterLambda.Compile();

        return getter();
    }

    protected override Expression VisitMember(MemberExpression node)
    {
        if (node.Expression != null)
        {
            if (_replaceEnumVariableToValue && node.Type.IsEnum)
            {
                _sb.Append(node.Type.Name + "." + GetValue(node));
            }
            else
            {
                if (node.Expression.NodeType != ExpressionType.Constant || !node.Expression.Type.IsNestedPrivate)
                {
                    Visit(node.Expression);
                    _sb.Append(".");
                }
                _sb.Append(node.Member.Name);
            }
        }
        else
        {
            _sb.Append(node.Member.DeclaringType!.Name + "." + node.Member.Name);
        }

        return node;
    }

    protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
    {
        throw new NotImplementedException();
    }

    protected override MemberBinding VisitMemberBinding(MemberBinding node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitMemberInit(MemberInitExpression node)
    {
        throw new NotImplementedException();
    }

    protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
    {
        throw new NotImplementedException();
    }

    protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        _sb.Append(node.Name);
        return node;
    }

    protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitSwitch(SwitchExpression node)
    {
        throw new NotImplementedException();
    }

    protected override SwitchCase VisitSwitchCase(SwitchCase node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitTry(TryExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitTypeBinary(TypeBinaryExpression node)
    {
        throw new NotImplementedException();
    }

    protected override Expression VisitUnary(UnaryExpression node)
    {
        if (node.NodeType == ExpressionType.Convert)
        {
            return Visit(node.Operand);
        }
        throw new NotImplementedException();
    }
}