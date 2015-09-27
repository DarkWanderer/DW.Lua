﻿using System;
using System.IO;
using System.Windows.Forms;
using DW.Lua.Editor.Properties;
using DW.Lua.Parsers;
using DW.Lua.Syntax;

namespace DW.Lua.Editor
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var script = codeBox.Text;
            try
            {
                var reader = new StringReader(script);
                var tokenEnumerator = Tokenizer.Parse(reader);
                var block = SyntaxParser.Parse(script);
                UpdateSyntaxTreeView(block);
                parserStatusLabel.Text = Resources.label_tokens + string.Join(", ",tokenEnumerator);
            }
            catch (Exception ex)
            {
                parserStatusLabel.Text = ex.Message;
            }
        }

        private void UpdateSyntaxTreeView(Unit unit)
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            InsertSyntaxTreeViewNode(unit,null);
            treeView1.ExpandAll();
            treeView1.EndUpdate();
        }

        private void InsertSyntaxTreeViewNode(Unit unit, TreeNode node)
        {
            var newNode = node?.Nodes.Add(unit.GetType().Name) ?? treeView1.Nodes.Add(unit.GetType().Name);

            foreach (var child in unit.Children)
                InsertSyntaxTreeViewNode(child,newNode);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
