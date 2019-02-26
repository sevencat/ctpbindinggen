using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CtpBindingGen
{
    public partial class Formmain : Form
    {
        public Formmain()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            CtpCppGen gen = new CtpCppGen();
            string d = this.textBoxDir.Text;
            GenErrorCs(d);
            gen.ParseSourceFile(d);
            var ret=gen.GenCode();
            richTextBoxCSharpTrdFunc.Text = ret.Item1;
            richTextBoxCppFunc.Text = ret.Item2;
            richTextBoxMacro.Text = ret.Item4;

            richTextBoxCls.Text = gen.str_struct;
            richTextBoxProto.Text = gen.str_proto;
            richTextBoxCppProtoGen.Text = gen.str_proto_cpp;
            
        }

        private void GenErrorCs(string d)
        {
            string xmlfn = d + "\\error.xml";
            XmlDocument doc=new XmlDocument();
            doc.Load(xmlfn);
            var nodes = doc.SelectNodes("//error");
            StringBuilder sb=new StringBuilder();
            foreach (var node in nodes)
            {
                XmlElement enode = (XmlElement) node;
                string strid = enode.GetAttribute("id");
                int id = int.Parse(enode.GetAttribute("value"));
                string prompt = enode.GetAttribute("prompt");
                sb.AppendFormat(("//{0}\r\n"),prompt);
                sb.AppendFormat("{0}={1},\r\n",strid,id);
            }
            string xxx = sb.ToString();
            return;
        }
    }
}
