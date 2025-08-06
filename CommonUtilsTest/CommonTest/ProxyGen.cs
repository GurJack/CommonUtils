//using System;
//using System.CodeDom;
//using System.CodeDom.Compiler;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Microsoft.CSharp;

//namespace CommonUtils
//{
//    /// <summary>
//    /// Static class for runtime types generation.
//    /// </summary>
//    public class ProxyGen
//    {
//        /// <summary>
//        /// Generate derived type
//        /// </summary>
//        /// <param name="baseType"></param>
//        /// <param name="createOverrideProperties"></param>
//        /// <returns></returns>
//        public static Type CreateDerivedTypeFor(Type baseType, bool createOverrideProperties = false)
//        {
//            var provider = new CSharpCodeProvider();
//            var cp = new CompilerParameters {GenerateInMemory = true};

//            var cu = new CodeCompileUnit();
//            AddAllAssemblyAsReference(cu);
//            cu.Namespaces.Add(CreateNamespace(baseType, createOverrideProperties));

//            var cr = provider.CompileAssemblyFromDom(cp, cu);

//            return cr.CompiledAssembly.GetTypes()[0];
//        }

//        private static void AddAllAssemblyAsReference(CodeCompileUnit cu)
//        {
//            foreach (var v in AppDomain.CurrentDomain.GetAssemblies())
//            {
//                string location = String.Empty;
//                try
//                {
//                    location = v.Location;
//                }
//                catch (Exception)
//                {
//                }

//                if(String.IsNullOrEmpty(location)) continue;

//                cu.ReferencedAssemblies.Add(v.Location);
//            }
//        }

//        private static string GetNameForDerivedClass(Type t)
//        {
//            return string.Concat(t.Name, "__derived");
//        }

//        private static CodeNamespace CreateNamespace(Type t, bool createOverrideProperties)
//        {
//            var nsp = new CodeNamespace("__derived");
//            var decl = new CodeTypeDeclaration();
//            decl.Name = GetNameForDerivedClass(t);
//            decl.TypeAttributes = TypeAttributes.NotPublic;
//            decl.Attributes = MemberAttributes.Private;
//            decl.BaseTypes.Add(t);

//            foreach (var ci in t.GetConstructors())
//            {
//                if (ci.IsPublic && ci.GetParameters().Length > 0)
//                {
//                    AddDerivedConstructor(decl, ci);
//                }
//            }

//            if (createOverrideProperties)
//            {
//                foreach (PropertyInfo pi in t.GetProperties().Where(p => p.IsVirtual()))
//                {
//                    //// make sense only to override properties having setter and getter
//                    //if (pi.CanWrite && pi.CanRead)
//                    {
//                        decl.Members.Add(CreatePropertyOverride(pi));
//                    }
//                }

//            }

//            nsp.Types.Add(decl);
//            return nsp;
//        }

//        private static void AddDerivedConstructor(CodeTypeDeclaration decl, ConstructorInfo ci)
//        {
//            var cc = new CodeConstructor();
//            cc.Attributes = MemberAttributes.Public;
//            foreach (var pi in ci.GetParameters())
//            {
//                cc.Parameters.Add(new CodeParameterDeclarationExpression() {Name = pi.Name, Type = new CodeTypeReference(pi.ParameterType), Direction = ToDirection(pi)});
//                cc.BaseConstructorArgs.Add(new CodeVariableReferenceExpression(pi.Name));
//            }

//            decl.Members.Add(cc);
//        }

//        private static FieldDirection ToDirection(ParameterInfo pi)
//        {
//            if (pi.IsIn)
//                return FieldDirection.In;
//            if (pi.IsOut)
//                return FieldDirection.Out;
//            if (pi.ParameterType.IsByRef)
//                return FieldDirection.Ref;
//            return FieldDirection.In;
//        }

//        private static CodeTypeMember CreatePropertyOverride(PropertyInfo pi)
//        {
//            CodeMemberProperty mp = new CodeMemberProperty();
//            mp.Name = pi.Name;
//            mp.Attributes = MemberAttributes.Override | MemberAttributes.Public;
//            mp.Type = new CodeTypeReference(pi.PropertyType);
//            mp.HasGet = mp.HasSet = true;

//            mp.GetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression() {PropertyName = pi.Name, TargetObject = new CodeBaseReferenceExpression()}));

//            //var conditionForNotify = new CodeMethodInvokeExpression() { Method = new CodeMethodReferenceExpression(new CodePropertyReferenceExpression() { PropertyName = pi.Name, TargetObject = new CodeBaseReferenceExpression() }, "Equals") };
//            //conditionForNotify.Parameters.Add(new CodePropertySetValueReferenceExpression());
//            //var setCondition = new CodeConditionStatement()
//            //{
//            //    Condition = new CodeBinaryOperatorExpression() { Left = new CodePrimitiveExpression(false), Right = conditionForNotify, Operator = CodeBinaryOperatorType.IdentityEquality }

//            //};
//            //setCondition.TrueStatements.Add(new CodeAssignStatement() { Left = new CodePropertyReferenceExpression() { PropertyName = pi.Name, TargetObject = new CodeBaseReferenceExpression() }, Right = new CodePropertySetValueReferenceExpression() });


//            //var invokeMethod = new CodeMethodInvokeExpression() { Method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), PropertyChangedFunctionName) };
//            //invokeMethod.Parameters.Add(new CodePrimitiveExpression(pi.Name));
//            //setCondition.TrueStatements.Add(invokeMethod);

//            //TODO:  dont work it:
//            mp.SetStatements.Add(new CodeMethodReturnStatement(new CodePropertyReferenceExpression() {PropertyName = pi.Name, TargetObject = new CodeBaseReferenceExpression()}));
//            return mp;
//        }
//    }

//    internal static class PropertyInfoExtension
//    {
//        private static readonly Dictionary<PropertyInfo, bool> _propertiesDic = new Dictionary<PropertyInfo, bool>();

//        public static bool IsVirtual(this PropertyInfo pi)
//        {
//            bool isVirtual;
//            if (!_propertiesDic.TryGetValue(pi, out isVirtual))
//            {
//                isVirtual = (pi.CanRead == false || pi.GetGetMethod().IsVirtual)
//                            &&
//                            (pi.CanWrite == false || pi.GetSetMethod().IsVirtual);

//                _propertiesDic[pi] = isVirtual;
//            }

//            return isVirtual;
//        }
//    }
//}