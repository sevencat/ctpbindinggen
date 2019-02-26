using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtpBindingGen
{
    public interface ITypeWrapper
    {
        string GetRawTypeName();
        string GetSystemTypeName(bool addComment = false);
        string GetMarshalTag();
        string GetCppTypeName();
        string GetProtoTypeName();
    }

    public struct DelegateDefine
    {
        public string tag;
        public string name;
        public string fullName;
    }

    public static class TypeWrapperFactory
    {
        // function tag -> delegate define
        public static Dictionary<string, DelegateDefine> DelegateGenerator = new Dictionary<string, DelegateDefine>();
        public static string CurrentPrefix = "";
        public static ITypeWrapper CreateTypeWrapper(CppSharp.AST.Type rawType)
        {
            if (rawType is CppSharp.AST.FunctionType)
            {
                return new FunctionTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.BuiltinType)
            {
                return new BuildinTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.DecayedType)
            {
                return new DecayedTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.TagType)
            {
                return new TagedTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.TypedefType)
            {
                return new TypedefTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.PointerType)
            {
                return new PointerTypeWrapper(rawType);
            }
            else if (rawType is CppSharp.AST.ArrayType)
            {
                return new ArrayTypeWrapper(rawType);
            }
            else
            {
                return new UnknowTypeWrapper(rawType);
            }
        }
    }

    internal class UnknowTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        public UnknowTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
        }

        public string GetRawTypeName()
        {
            return "FIXME Unknow";
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            return "FIXME Unknow";
        }

        public string GetMarshalTag()
        {
            return "FIXME Unknow";
        }

        public string GetCppTypeName()
        {
            throw new NotImplementedException();
        }

        public string GetProtoTypeName()
        {
            throw new NotImplementedException();
        }
    }

    internal class FunctionTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        private CppSharp.AST.FunctionType type;
        public FunctionTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.FunctionType;
        }

        public string GetRawTypeName()
        {
            string functionTag = "";
            functionTag += TypeWrapperFactory.CreateTypeWrapper(type.ReturnType.Type).GetSystemTypeName();
            functionTag += "(";
            for (int i = 0; i < type.Parameters.Count; i++)
            {
                var param = type.Parameters[i];
                functionTag += TypeWrapperFactory.CreateTypeWrapper(param.Type).GetSystemTypeName();
                if (i != (type.Parameters.Count - 1))
                    functionTag += ",";
            }
            functionTag += ")";
            return functionTag;
        }

        public string GetCppTypeName()
        {
            return "";
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            string tag = GetRawTypeName();
            if (TypeWrapperFactory.DelegateGenerator.ContainsKey(tag))
            {
                return TypeWrapperFactory.DelegateGenerator[tag].name;
            }
            else
            {
                DelegateDefine deleg = new DelegateDefine();
                deleg.tag = tag;
                deleg.name = TypeWrapperFactory.CurrentPrefix + "_func_" + TypeWrapperFactory.DelegateGenerator.Count.ToString();
                //public delegate int GetBufferCallback(IntPtr pAVCodecContext, IntPtr pAVFrame);
                deleg.fullName = "public delegate ";
                deleg.fullName += TypeWrapperFactory.CreateTypeWrapper(type.ReturnType.Type).GetSystemTypeName(true);
                deleg.fullName += " " + deleg.name + "(\r\n";
                for (int i = 0; i < type.Parameters.Count; i++)
                {
                    var param = type.Parameters[i];
                    deleg.fullName += "\t" + TypeWrapperFactory.CreateTypeWrapper(param.Type).GetMarshalTag();
                    deleg.fullName += TypeWrapperFactory.CreateTypeWrapper(param.Type).GetSystemTypeName(true);
                    if (!string.IsNullOrEmpty(param.Name))
                        deleg.fullName += " " + param.Name;
                    else
                        deleg.fullName += " __arg" + i.ToString();
                    if (i != (type.Parameters.Count - 1))
                        deleg.fullName += ", \r\n";
                }
                deleg.fullName += ");";
                TypeWrapperFactory.DelegateGenerator.Add(tag, deleg);
                return deleg.name;
            }
        }

        public string GetMarshalTag()
        {
            return "[MarshalAs(UnmanagedType.FunctionPtr)]";
        }

        public string GetProtoTypeName()
        {
            throw new NotImplementedException();
        }
    }

    internal class BuildinTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        CppSharp.AST.BuiltinType type;
        public BuildinTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.BuiltinType;
        }

        public string GetRawTypeName()
        {
            return type.Type.ToString();
        }

        public string GetProtoTypeName()
        {
            string ret = "";
            switch (type.Type)
            {
                case CppSharp.AST.PrimitiveType.Bool:
                    ret = "bool";
                    break;
                //case CppSharp.AST.PrimitiveType.Char:
                case CppSharp.AST.PrimitiveType.Char:
                    ret = "char";
                    break;
                case CppSharp.AST.PrimitiveType.Double:
                    ret = "double";
                    break;
                case CppSharp.AST.PrimitiveType.Float:
                    ret = "float";
                    break;
                case CppSharp.AST.PrimitiveType.Short:
                    ret = "sint32";
                    break;
                case CppSharp.AST.PrimitiveType.Int:
                    ret = "sint32";
                    break;
                case CppSharp.AST.PrimitiveType.Long:
                    ret = "sint64";
                    break;
                case CppSharp.AST.PrimitiveType.IntPtr:
                    throw new NotImplementedException();
                    break;
                case CppSharp.AST.PrimitiveType.Null:
                    throw new NotImplementedException();
                    break;
                case CppSharp.AST.PrimitiveType.UChar:
                    ret = "uint8_t";
                    break;
                case CppSharp.AST.PrimitiveType.UShort:
                    ret = "uint32";
                    break;
                case CppSharp.AST.PrimitiveType.UInt:
                    ret = "uint32";
                    break;
                case CppSharp.AST.PrimitiveType.ULong:
                    ret = "uint64";
                    break; ;
                case CppSharp.AST.PrimitiveType.Void:
                    throw new NotImplementedException();
                case CppSharp.AST.PrimitiveType.WideChar:
                    throw new NotImplementedException();
                default:
                    ret = "";
                    break;
            }
            return ret;
        }

        public string GetCppTypeName()
        {
            string ret = "";
            switch (type.Type)
            {
                case CppSharp.AST.PrimitiveType.Bool:
                    ret = "bool";
                    break;
                //case CppSharp.AST.PrimitiveType.Char:
                case CppSharp.AST.PrimitiveType.Char:
                    ret = "char";
                    break;
                case CppSharp.AST.PrimitiveType.Double:
                    ret = "double";
                    break;
                case CppSharp.AST.PrimitiveType.Float:
                    ret = "float";
                    break;
                case CppSharp.AST.PrimitiveType.Short:
                    ret = "int16_t";
                    break;
                case CppSharp.AST.PrimitiveType.Int:
                    ret = "int32_t";
                    break;
                case CppSharp.AST.PrimitiveType.Long:
                    ret = "int64_t";
                    break;
                case CppSharp.AST.PrimitiveType.IntPtr:
                    ret = "int *";
                    break;
                case CppSharp.AST.PrimitiveType.Null:
                    ret = "NULL";
                    break;
                case CppSharp.AST.PrimitiveType.UChar:
                    ret = "uint8_t";
                    break;
                case CppSharp.AST.PrimitiveType.UShort:
                    ret = "uint16_t";
                    break;
                case CppSharp.AST.PrimitiveType.UInt:
                    ret = "uint32_t";
                    break;
                case CppSharp.AST.PrimitiveType.ULong:
                    ret = "uint64_t";
                    break; ;
                case CppSharp.AST.PrimitiveType.Void:
                    ret = "void";
                    break;
                case CppSharp.AST.PrimitiveType.WideChar:
                    ret = "wchar";
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            string ret = "";
            switch (type.Type)
            {
                case CppSharp.AST.PrimitiveType.Bool:
                    ret = "Bool";
                    break;
                //case CppSharp.AST.PrimitiveType.Char:
                case CppSharp.AST.PrimitiveType.Char:
                    ret = "byte";
                    break;
                case CppSharp.AST.PrimitiveType.Double:
                    ret = "Double";
                    break;
                case CppSharp.AST.PrimitiveType.Float:
                    ret = "float";
                    break;
                case CppSharp.AST.PrimitiveType.Short:
                    ret = "Int16";
                    break;
                case CppSharp.AST.PrimitiveType.Int:
                    ret = "Int32";
                    break;
                case CppSharp.AST.PrimitiveType.Long:
                    ret = "Int64";
                    break;
                case CppSharp.AST.PrimitiveType.IntPtr:
                    ret = "IntPtr";
                    break;
                case CppSharp.AST.PrimitiveType.Null:
                    ret = "null";
                    break;
                case CppSharp.AST.PrimitiveType.UChar:
                    ret = "SByte";
                    break;
                case CppSharp.AST.PrimitiveType.UShort:
                    ret = "UInt16";
                    break;
                case CppSharp.AST.PrimitiveType.UInt:
                    ret = "UInt32";
                    break;
                case CppSharp.AST.PrimitiveType.ULong:
                    ret = "UInt64";
                    break; ;
                case CppSharp.AST.PrimitiveType.Void:
                    ret = "void";
                    break;
                case CppSharp.AST.PrimitiveType.WideChar:
                    ret = "UInt16";
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        }

        public string GetMarshalTag()
        {
            string ret = "";
            return "";
            switch (type.Type)
            {
                case CppSharp.AST.PrimitiveType.Bool:
                    ret = "[MarshalAs(UnmanagedType.I1)]";
                    break;
                //case CppSharp.AST.PrimitiveType.Char:
                case CppSharp.AST.PrimitiveType.Char:
                    ret = "[MarshalAs(UnmanagedType.I1)]";
                    break;
                case CppSharp.AST.PrimitiveType.Double:
                    ret = "[MarshalAs(UnmanagedType.R8)]";
                    break;
                case CppSharp.AST.PrimitiveType.Float:
                    ret = "[MarshalAs(UnmanagedType.R4)]";
                    break;
                case CppSharp.AST.PrimitiveType.Short:
                    ret = "[MarshalAs(UnmanagedType.I2)]";
                    break;
                case CppSharp.AST.PrimitiveType.Int:
                    ret = "[MarshalAs(UnmanagedType.I4)]";
                    break;
                case CppSharp.AST.PrimitiveType.Long:
                    ret = "[MarshalAs(UnmanagedType.I8)]";
                    break;
                case CppSharp.AST.PrimitiveType.IntPtr:
                    ret = "";
                    break;
                case CppSharp.AST.PrimitiveType.Null:
                    ret = "";
                    break;
                case CppSharp.AST.PrimitiveType.UChar:
                    ret = "[MarshalAs(UnmanagedType.I1)]";
                    break;
                case CppSharp.AST.PrimitiveType.UShort:
                    ret = "[MarshalAs(UnmanagedType.I2)]";
                    break;
                case CppSharp.AST.PrimitiveType.UInt:
                    ret = "[MarshalAs(UnmanagedType.I4)]";
                    break;
                case CppSharp.AST.PrimitiveType.ULong:
                    ret = "[MarshalAs(UnmanagedType.I8)]";
                    break; ;
                case CppSharp.AST.PrimitiveType.Void:
                    ret = "";
                    break;
                case CppSharp.AST.PrimitiveType.WideChar:
                    ret = "[MarshalAs(UnmanagedType.I2)]";
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        }
    }

    internal class DecayedTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        CppSharp.AST.DecayedType type;
        public DecayedTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.DecayedType;
        }

        public string GetCppTypeName()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Decayed.Type).GetCppTypeName();
        }

        public string GetRawTypeName()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Decayed.Type).GetRawTypeName();
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Decayed.Type).GetSystemTypeName();
        }

        public string GetMarshalTag()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Decayed.Type).GetMarshalTag();
        }

        string ITypeWrapper.GetRawTypeName()
        {
            throw new NotImplementedException();
        }

        string ITypeWrapper.GetSystemTypeName(bool addComment)
        {
            throw new NotImplementedException();
        }

        string ITypeWrapper.GetMarshalTag()
        {
            throw new NotImplementedException();
        }

        string ITypeWrapper.GetCppTypeName()
        {
            throw new NotImplementedException();
        }

        string ITypeWrapper.GetProtoTypeName()
        {
            throw new NotImplementedException();
        }
    }

    internal class TagedTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        CppSharp.AST.TagType type;
        public TagedTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.TagType;
        }

        public string GetRawTypeName()
        {
            return type.Declaration.Name;
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            return GetRawTypeName();
        }

        public string GetCppTypeName()
        {
            return "";
        }

        public string GetMarshalTag()
        {
            return "";
        }

        string ITypeWrapper.GetProtoTypeName()
        {
            throw new NotImplementedException();
        }
    }

    internal class TypedefTypeWrapper : ITypeWrapper
    {
        public  CppSharp.AST.Type rawType;
        public CppSharp.AST.TypedefType type;
        public TypedefTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.TypedefType;
        }

        public string GetRawTypeName()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Declaration.Type).GetRawTypeName();
        }

        public string GetCppTypeName()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Declaration.Type).GetCppTypeName();
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Declaration.Type).GetSystemTypeName();
        }

        public string GetMarshalTag()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Declaration.Type).GetMarshalTag();
        }

        string ITypeWrapper.GetProtoTypeName()
        {
            return TypeWrapperFactory.CreateTypeWrapper(type.Declaration.Type).GetProtoTypeName();
        }
    }

    internal class PointerTypeWrapper : ITypeWrapper
    {
        private CppSharp.AST.Type rawType;
        CppSharp.AST.PointerType type;
        public PointerTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.PointerType;
        }

        string ITypeWrapper.GetProtoTypeName()
        {
            throw new NotImplementedException();
        }

        public string GetRawTypeName()
        {
            return "IntPtr";
        }

        public string GetCppTypeName()
        {
            if (type.Pointee is CppSharp.AST.FunctionType)
            {
                return TypeWrapperFactory.CreateTypeWrapper(type.Pointee).GetSystemTypeName();
            }
            else if (type.Pointee is CppSharp.AST.BuiltinType)
            {
                CppSharp.AST.BuiltinType buildin = type.Pointee as CppSharp.AST.BuiltinType;
                if (buildin.Type == CppSharp.AST.PrimitiveType.UChar ||
                    buildin.Type == CppSharp.AST.PrimitiveType.Char)
                    return "string";
            }
            string pointeeTypeName = TypeWrapperFactory.CreateTypeWrapper(type.Pointee).GetSystemTypeName();
            return pointeeTypeName + " *";
        }

        public string GetSystemTypeName(bool addComment = false)
        {
            if (type.Pointee is CppSharp.AST.FunctionType)
            {
                return TypeWrapperFactory.CreateTypeWrapper(type.Pointee).GetSystemTypeName();
            }
            else if (type.Pointee is CppSharp.AST.BuiltinType)
            {
                CppSharp.AST.BuiltinType buildin = type.Pointee as CppSharp.AST.BuiltinType;
                if (buildin.Type == CppSharp.AST.PrimitiveType.UChar ||
                    buildin.Type == CppSharp.AST.PrimitiveType.Char)
                    return "string";
            }
            string pointeeTypeName = TypeWrapperFactory.CreateTypeWrapper(type.Pointee).GetSystemTypeName();
            return pointeeTypeName;
            string ret = "IntPtr";
            if (addComment)
                ret += "/* " + pointeeTypeName + "*  */";
            return ret;
        }

        public string GetMarshalTag()
        {
            if (type.Pointee is CppSharp.AST.FunctionType)
            {
                return TypeWrapperFactory.CreateTypeWrapper(type.Pointee).GetMarshalTag();
            }

            else if (type.Pointee is CppSharp.AST.BuiltinType)
            {
                CppSharp.AST.BuiltinType buildin = type.Pointee as CppSharp.AST.BuiltinType;
                if (buildin.Type == CppSharp.AST.PrimitiveType.Char ||
                    buildin.Type == CppSharp.AST.PrimitiveType.UChar)
                    return "[MarshalAs(UnmanagedType.LPStr)]";
            }

            return "";
        }
    }

    internal class ArrayTypeWrapper : ITypeWrapper
    {
        public CppSharp.AST.Type rawType;
        public CppSharp.AST.ArrayType type;
        bool isChar = false;
        public ArrayTypeWrapper(CppSharp.AST.Type rawType)
        {
            this.rawType = rawType;
            type = rawType as CppSharp.AST.ArrayType;
            if (type.Type is CppSharp.AST.BuiltinType)
            {
                var realtp = type.Type as CppSharp.AST.BuiltinType;
                if (realtp.Type == CppSharp.AST.PrimitiveType.Char)
                {
                    isChar = true;
                }
            }
        }

        public string GetCppTypeName()
        {
            if (isChar) return "const char *";
            string typeName = TypeWrapperFactory.CreateTypeWrapper(type.Type).GetCppTypeName();
            return typeName + "[]";
        }

        public string GetRawTypeName()
        {
            string typeName = TypeWrapperFactory.CreateTypeWrapper(type.Type).GetRawTypeName();
            return typeName;
        }



        public string GetSystemTypeName(bool addComment = false)
        {
            if (isChar) return "string";
            return GetRawTypeName() + "[]";
        }

        public string GetMarshalTag()
        {
            if (isChar) return "[MarshalAs(UnmanagedType.ByValTStr, SizeConst=" + type.Size.ToString() + ")]";
            return "[MarshalAs(UnmanagedType.ByValArray, SizeConst=" + type.Size.ToString() + ")]";
        }

        string ITypeWrapper.GetProtoTypeName()
        {
            if (isChar) return "string";
            throw new NotImplementedException();
        }
    }

    

    public class TypeHeloer
    {
        public static string GetRawTypeName(CppSharp.AST.Type type)
        {
            return TypeWrapperFactory.CreateTypeWrapper(type).GetRawTypeName();
        }

        public static string GetSystemTypeName(CppSharp.AST.Type type, bool addComment = false)
        {
            return TypeWrapperFactory.CreateTypeWrapper(type).GetSystemTypeName(addComment);
        }

        public static string GetMarshalTag(CppSharp.AST.Type type)
        {
            return TypeWrapperFactory.CreateTypeWrapper(type).GetMarshalTag();
        }
    }
}
