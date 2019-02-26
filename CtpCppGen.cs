using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CppSharp;
using CppSharp.AST;
using CppSharp.Parser;

namespace CtpBindingGen
{
    public class CtpCppGen
    {
        public CtpCppGen()
        {
            DirHome= AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            DirTpl = DirHome + "\\tpl";
        }
           
        public CppSharp.AST.ASTContext astContext;

        ArrayTypeWrapper GetRealArrType(ITypeWrapper tw)
        {
            if (tw.GetType() == typeof(ArrayTypeWrapper))
            {
                return (ArrayTypeWrapper)tw;
            }
            if (tw.GetType() == typeof(TypedefTypeWrapper))
            {
                TypedefTypeWrapper reapttw = (TypedefTypeWrapper)tw;
                var realtp = TypeWrapperFactory.CreateTypeWrapper(reapttw.type.Declaration.Type);
                if (realtp.GetType() == typeof(ArrayTypeWrapper))
                    return (ArrayTypeWrapper)realtp;
            }
            return null;
        }

        public void GenProtoCppGen(StringBuilder sb)
        {
            var structdef = astContext.TranslationUnits[0];
            {
                foreach (var clsitem in structdef.Classes)
                {
                    GenZhuShiCStyle(sb, clsitem.Comment.BriefText, "");
                    string clsname = clsitem.Name;

                    sb.AppendLine("//反序列化");
                    sb.AppendFormat("bool parse_from_buf({0} &d,const void *pdata,int datalen)\r\n", clsname);
                    sb.AppendLine("{");
                    sb.AppendFormat("   memset(&d,sizeof(d),0);\r\n");
                    sb.AppendFormat("   yz::ctp::{0} proto_data;\r\n",clsname);
                    sb.AppendFormat("   if(!proto_data.ParsePartialFromArray(pdata,datalen))\r\n");
                    sb.AppendFormat("       return false;\r\n");

                    foreach (var fld in clsitem.Fields)
                    {
                        var tw = TypeWrapperFactory.CreateTypeWrapper(fld.QualifiedType.Type);
                        ArrayTypeWrapper atw = GetRealArrType(tw);
                        GenZhuShiCStyle(sb, fld.Comment.BriefText, "    ");
                        if (atw!=null)
                        {
                            //strncpy
                            sb.AppendFormat("   strncpy(d.{0},proto_data.{0},{1}-1);\r\n",fld.Name,atw.type.ElementSize);
                        }
                        else
                        {
                            sb.AppendFormat("   d.{0}=proto_data.{0};\r\n",fld.Name);
                        }
                    }
                    sb.AppendLine("    return true;");
                    sb.AppendLine("}\r\n");

                    sb.AppendLine("//序列化");
                    sb.AppendFormat("bool serialize_from_string({0} &d,std::string *output)\r\n", clsname);
                    sb.AppendLine("{");
                    sb.AppendFormat("   yz::ctp::{0} proto_data;\r\n", clsname);
                    foreach (var fld in clsitem.Fields)
                    {
                        var tw = TypeWrapperFactory.CreateTypeWrapper(fld.QualifiedType.Type);
                        ArrayTypeWrapper atw = GetRealArrType(tw);
                        GenZhuShiCStyle(sb, fld.Comment.BriefText, "    ");
                        if (atw != null)
                        {
                            //strncpy
                            sb.AppendFormat("   proto_data.{0}=std::string(d.{0},strnlen(d.{0},{1}));\r\n", fld.Name, atw.type.ElementSize);
                        }
                        else
                        {
                            sb.AppendFormat("   proto_data.{0}=d.{0};\r\n", fld.Name);
                        }
                    }
                    sb.AppendFormat("   if(!proto_data.SerializePartialToString(output))\r\n");
                    sb.AppendFormat("       return false;\r\n");


                    sb.AppendLine("    return true;");
                    sb.AppendLine("}\r\n");
                }
            }
        }

        public void GenStruct(StringBuilder sb,StringBuilder sb_proto)
        {
            sb_proto.AppendLine("syntax = \"proto3\"");
            sb_proto.AppendLine("package yz.ctp;\r\n");

            var structdef = astContext.TranslationUnits[0];
            {
                foreach (var clsitem in structdef.Classes)
                {
                    GenZhuShi(sb, clsitem.Comment.BriefText, "");
                    GenZhuShiCStyle(sb_proto, clsitem.Comment.BriefText, "");

                    sb.AppendLine("[StructLayout(LayoutKind.Sequential)]");
                    string clsname = clsitem.Name;

                    sb.AppendFormat("public class {0}\r\n", clsname);
                    sb.AppendLine("{");

                    sb_proto.AppendFormat("message class {0}\r\n", clsname);
                    sb_proto.AppendLine("{");

                    int protonum = 1;
                    foreach (var fld in clsitem.Fields)
                    {
                        var tw = TypeWrapperFactory.CreateTypeWrapper(fld.QualifiedType.Type);
                        GenZhuShi(sb, fld.Comment.BriefText, "    ");
                        string marshaltag = tw.GetMarshalTag();
                        if(marshaltag.Length>0)
                            sb.AppendFormat("    {0}\r\n", tw.GetMarshalTag());
                        sb.AppendFormat("    public {0} {1};\r\n", tw.GetSystemTypeName(), fld.Name);

                        //string name = 1;
                        GenZhuShiCStyle(sb_proto, fld.Comment.BriefText, "    ");
                        sb_proto.AppendFormat("    {0} {1}={2};\r\n", tw.GetProtoTypeName(), fld.Name,protonum);
                        protonum++;
                    }
                    sb.AppendLine("}");
                    sb_proto.AppendLine("}\r\n");
                }
            }
        }

        public void GenMacro(StringBuilder sb)
        {
            var typedefs = astContext.TranslationUnits[1];
            {
                //先生成枚举
                foreach (var e in typedefs.Enums)
                {
                    TranslatEnum(sb, e, "    ");
                }
                //public enum EnumBrokerFunctionCodeType : byte
                sb.AppendLine("public enum ConstValue : byte");
                sb.AppendLine("{");
                foreach (var e in typedefs.PreprocessedEntities)
                {
                    CppSharp.AST.MacroDefinition d = e as CppSharp.AST.MacroDefinition;
                    if (d != null)
                    {
                        sb.AppendFormat("   {0}==(byte){1},\r\n", d.Name, d.Expression);
                    }
                }
                sb.AppendLine("}");
            }
        }

        public Tuple<string,string> GenTradeCode()
        {
            var tra = astContext.TranslationUnits[2];
            var cls = tra.FindClass("CThostFtdcTraderApi");
            StringBuilder sb_cpp = new StringBuilder();
            StringBuilder sb_csharp = new StringBuilder();
            foreach(var method in cls.Methods)
            {
                string name = method.Name;
                if (!name.StartsWith("Req"))
                    continue;
                var paramout = method.ReturnType;
                var paramin = method.Parameters;

                {
                    string str_out = TypeWrapperFactory.CreateTypeWrapper(paramout.Type).GetSystemTypeName();
                    List<string> str_inlist = new List<string>();
                    str_inlist.Add("IntPtr pApi");
                    foreach (var p in paramin)
                    {
                        string tempbuf = string.Format("{0} {1}", TypeWrapperFactory.CreateTypeWrapper(p.Type).GetSystemTypeName(), p.Name);
                        str_inlist.Add(tempbuf);
                    }
                    string str_in = String.Join(",", str_inlist);
                    string curline = string.Format("{0} {1}({2})", str_out, name, str_in);

                    //[DllImport("cctp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
                    //public static extern int TdReqQryInstrument(IntPtr pApi, ThostFtdcQryInstrumentField pQryInstrument, int nRequestID);
                    sb_csharp.AppendLine("[DllImport(\"cctp\", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]");
                    sb_csharp.AppendFormat("public static extern {0} Td{1}({2});\r\n\r\n", str_out, name, str_in);
                }

                {
                    string str_out = TypeWrapperFactory.CreateTypeWrapper(paramout.Type).GetCppTypeName();
                    List<string> str_inlist = new List<string>();
                    List<string> str_innamelist = new List<string>();
                    str_inlist.Add("void* pApi");
                    foreach (var p in paramin)
                    {
                        string tempbuf = string.Format("{0} {1}", TypeWrapperFactory.CreateTypeWrapper(p.Type).GetCppTypeName(), p.Name);
                        str_inlist.Add(tempbuf);
                        str_innamelist.Add(p.Name);
                    }
                    string str_in = String.Join(",", str_inlist);
                    string curline = string.Format("{0} {1}({2})", str_out, name, str_in);
                    string str_inname= String.Join(",", str_innamelist);
                    //[DllImport("cctp", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
                    //public static extern int TdReqQryInstrument(IntPtr pApi, ThostFtdcQryInstrumentField pQryInstrument, int nRequestID);
                    sb_cpp.AppendFormat("FTDC2C_API {0} MYDECL Td{1}({2})\r\n",str_out,name, str_in);
                    sb_cpp.AppendLine("{");
                    sb_cpp.AppendFormat("   return (static_cast<Trader*>(pApi))->RawApi->{0}({1});\r\n", name, str_inname);
                    sb_cpp.AppendLine("}\r\n");
                }


                //FTDC2C_API int MYDECL TdReqQryTrade(void* pApi, CThostFtdcQryTradeField*pQryTrade, int nRequestID) {
                //   return (static_cast<Trader*>(pApi))->RawApi->ReqQryTrade(pQryTrade, nRequestID);
                //}
                //sb_cpp.AppendFormat();
            }
            var x = sb_csharp.ToString();
            var y = sb_cpp.ToString();
            return Tuple.Create(x,y);
        }

        public string str_struct { get; set; }
        public string str_proto { get; set; }
        public string str_proto_cpp { get; set; }
        public Tuple<string,string,string,string,string> GenCode()
        {
            
            var str_trd=GenTradeCode();

            StringBuilder sbstruct = new StringBuilder();
            StringBuilder sb_proto = new StringBuilder();
            
            GenStruct(sbstruct,sb_proto);
            str_struct = sbstruct.ToString();
            str_proto = sb_proto.ToString();
            
            {
                StringBuilder sb_protocpp = new StringBuilder();
                GenProtoCppGen(sb_protocpp);
                str_proto_cpp = sb_protocpp.ToString();
            }
            StringBuilder sbmacro = new StringBuilder();
            GenMacro(sbmacro);
            return Tuple.Create(str_trd.Item1, str_trd.Item2,sbstruct.ToString(),sbmacro.ToString(),"");
        }

        private static void TranslatEnum(StringBuilder writer, CppSharp.AST.Enumeration enums, string prefix)
        {
            //foreach (var enums in unit.Enums)
            //{
                writer.AppendLine("public enum " + enums.Name);
                writer.AppendLine("{");
                foreach (var item in enums.Items)
                {
                    writer.AppendLine("\t" + item.Name + " = " + item.Value.ToString() + ",");
                }
                writer.AppendLine("}");
                writer.AppendLine("");
            //}
        }

        void GenZhuShi(StringBuilder sb,string zj,string prefix)
        {
            if (zj == null)
                return;
            if (zj.Trim().Length == 0)
                return;
            sb.AppendFormat("{0}/// <summary>\r\n",prefix);
            var lines = zj.Split("\r\n".ToCharArray());
            foreach(var line in lines)
            {
                sb.AppendFormat("{0}/// {1}\r\n", prefix,line);
            }
            sb.AppendFormat("{0}/// </summary>\r\n", prefix);
        }

        void GenZhuShiCStyle(StringBuilder sb, string zj, string prefix)
        {
            if (zj == null)
                return;
            if (zj.Trim().Length == 0)
                return;
            var lines = zj.Split("\r\n".ToCharArray());
            foreach (var line in lines)
            {
                sb.AppendFormat("{0}// {1}\r\n", prefix, line);
            }
        }

        public CppSharp.AST.Type GetRealType(CppSharp.AST.Type t)
        {
            CppSharp.AST.TypedefType typedeftp = t as CppSharp.AST.TypedefType;
            if (typedeftp != null)
            {
                return typedeftp.Declaration.QualifiedType.Type;
            }
            return t;
        }

       

        public bool ParseSourceFile(string d)
        {
            // Lets setup the options for parsing the file.
            var parserOptions = new ParserOptions
            {
                LanguageVersion = LanguageVersion.CPP11,
                // Verbose here will make sure the parser outputs some extra debugging
                // information regarding include directories, which can be helpful when
                // tracking down parsing issues.
                Verbose = true
            };
            parserOptions.IncludeDirs.Add(@"G:\libs\exchg\Ctp\api\20180109_tradeapi64_windows");
            // This will setup the necessary system include paths and arguments for parsing.
            // It will probe into the registry (on Windows) and filesystem to find the paths
            // of the system toolchains and necessary include directories.
            parserOptions.Setup();

            // We create the Clang parser and parse the source code.
            var parser = new CppSharp.ClangParser();
            var filelst = new List<string>();
            filelst.Add(d+"\\ThostFtdcUserApiStruct.h");
            filelst.Add(d + "\\ThostFtdcUserApiDataType.h");
            filelst.Add(d + "\\ThostFtdcTraderApi.h");
            filelst.Add(d + "\\ThostFtdcMdApi.h");
            using (var parserResult = parser.ParseSourceFiles(filelst, parserOptions))
            {

                // If there was some kind of error parsing, then lets print some diagnostics.
                if (parserResult.Kind != ParserResultKind.Success)
                {
                    if (parserResult.Kind == ParserResultKind.FileNotFound)
                        Console.Error.WriteLine($"file was not found.");
                    for (uint i = 0; i < parserResult.DiagnosticsCount; i++)
                    {
                        var diag = parserResult.GetDiagnostics(i);

                        Console.WriteLine("{0}({1},{2}): {3}: {4}",
                            diag.FileName, diag.LineNumber, diag.ColumnNumber,
                            diag.Level.ToString().ToLower(), diag.Message);
                    }
                    return false;
                }
                astContext = CppSharp.ClangParser.ConvertASTContext(parserOptions.ASTContext);
                return true;
            }
        }

        public string DirHome { get; set; }
        public string DirTpl { get; set; }
    }
}
