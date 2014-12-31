// ------------------------------------------------------------------------
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>
// 
// ------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MinUnit
{
	public partial class FormMain : Form
	{
		private readonly string _filename;

		public FormMain()
		{
			InitializeComponent();
		}

		public FormMain(string[] args)
		{
			InitializeComponent();

			if (args.Length > 0)
			{
				_filename = new System.IO.FileInfo(args[0]).FullName;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			switch (e.KeyCode)
			{
				case Keys.F5:
					e.Handled = true;
					RunTests(treeView.SelectedNode);
					break;
			}
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			if (_filename != null)
			{
				System.Console.SetOut(new RichTextBoxWriter(richTextBox));

				TreeNode assemblyTreeNode = treeView.Nodes.Add(_filename);
				assemblyTreeNode.ImageIndex = 0;
				assemblyTreeNode.SelectedImageIndex = 0;
				Dictionary<string, TreeNode> namespaceTreeNodes = new Dictionary<string, TreeNode>();
				namespaceTreeNodes.Add(".", assemblyTreeNode);

				//System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(_filename);
				System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(_filename);
				Type[] types = assembly.GetExportedTypes();
				System.Array.Sort(types, new TypeFullNameComparer());
				for (int i = 0; i < types.Length; i++)
				{
					object[] attributes = types[i].GetCustomAttributes(typeof(NUnit.Framework.TestFixtureAttribute), false);
					if (attributes.Length > 0)
					{
						string[] segments = types[i].Namespace.Split('.');
						string accumulatedNamespace = "";
						for (int j = 0; j < segments.Length; j++)
						{
							string accumulatedNamespace2 = accumulatedNamespace + "." + segments[j];
							if (!namespaceTreeNodes.ContainsKey(accumulatedNamespace2))
							{
								TreeNode namespaceTreeNode = ((accumulatedNamespace.Length == 0) ? namespaceTreeNodes["."] : namespaceTreeNodes[accumulatedNamespace]).Nodes.Add(segments[j]);
								namespaceTreeNode.ImageIndex = 1;
								namespaceTreeNode.SelectedImageIndex = 1;
								namespaceTreeNodes.Add(accumulatedNamespace2, namespaceTreeNode);
							}
							accumulatedNamespace = accumulatedNamespace2;
						}

						TreeNode typeTreeNode = namespaceTreeNodes["." + types[i].Namespace].Nodes.Add(types[i].Name);
						typeTreeNode.ImageIndex = 1;
						typeTreeNode.SelectedImageIndex = 1;

						System.Reflection.MethodInfo[] methods = types[i].GetMethods();
						for (int k = 0; k < methods.Length; k++)
						{
							object[] attributes2 = methods[k].GetCustomAttributes(typeof(NUnit.Framework.TestAttribute), false);
							if (attributes2.Length > 0)
							{
								NUnit.Framework.ExpectedExceptionAttribute expectedExceptionAttribute = null;
								object[] attributes3 = methods[k].GetCustomAttributes(typeof(NUnit.Framework.ExpectedExceptionAttribute), false);
								if (attributes3.Length > 0)
									expectedExceptionAttribute = (NUnit.Framework.ExpectedExceptionAttribute)attributes3[0];

								TreeNode testCaseTreeNode = typeTreeNode.Nodes.Add(methods[k].Name);
								testCaseTreeNode.ImageIndex = 4;
								testCaseTreeNode.SelectedImageIndex = 4;
								testCaseTreeNode.Tag = new TestCase(methods[k], expectedExceptionAttribute);
							}
						}
					}
				}

				foreach (TreeNode treeNode in namespaceTreeNodes.Values)
					treeNode.Expand();

				treeView.SelectedNode = assemblyTreeNode;
			}
		}

		private bool _doubleClick = false;

		private void treeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			if (_doubleClick)
				e.Cancel = true;
		}

		private void treeView_MouseDown(object sender, MouseEventArgs e)
		{
			_doubleClick = e.Clicks > 1;
		}

		private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			RunTests(treeView.SelectedNode);
		}

		private void RunTests(TreeNode treeNode)
		{
			treeView.Enabled = false;

			_doubleClick = false;

			richTextBox.Clear();
			Cursor = Cursors.WaitCursor;

			progressBar.ForeColor = System.Drawing.Color.Lime;
			progressBar.Maximum = CountTests(treeNode) * progressBar.Step;
			progressBar.Value = 0;

			RunTests2(treeNode);

			treeView.Enabled = true;
			Cursor = Cursors.Default;
		}

		private int CountTests(TreeNode treeNode)
		{
			if (treeNode.Nodes.Count == 0)
			{
				if (treeNode.Tag == null)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
			else
			{
				int count = 0;
				foreach (TreeNode treeNode2 in treeNode.Nodes)
					count += CountTests(treeNode2);
				return count;
			}
		}

		private bool RunTests2(TreeNode treeNode)
		{
			if (treeNode.Nodes.Count == 0)
			{
				if (treeNode.Tag == null)
				{
					return true;
				}
				else
				{
					TestCase testCase = (TestCase)treeNode.Tag;

					bool success = testCase.Run(richTextBox);
					int imageIndex = success ? 2 : 3;
					treeNode.ImageIndex = imageIndex;
					treeNode.SelectedImageIndex = imageIndex;
					progressBar.PerformStep();
					if (!success)
						progressBar.ForeColor = System.Drawing.Color.Red;

					Application.DoEvents();

					return success;
				}
			}
			else
			{
				bool expanded = false;
				if (!treeNode.IsExpanded)
				{
					treeNode.Expand();
					Application.DoEvents();
					expanded = true;
				}

				bool success = true;
				foreach (TreeNode treeNode2 in treeNode.Nodes)
					if (!RunTests2(treeNode2))
						success = false;

				if (success && expanded)
				{
					treeNode.Collapse();
					Application.DoEvents();
				}

				return success;
			}
		}

		private class TestCase
		{
			private readonly System.Reflection.MethodInfo _method;
			private readonly NUnit.Framework.ExpectedExceptionAttribute _expectedExceptionAttribute;

			public TestCase(System.Reflection.MethodInfo method, NUnit.Framework.ExpectedExceptionAttribute expectedExceptionAttribute)
			{
				_method = method;
				_expectedExceptionAttribute = expectedExceptionAttribute;
			}

			public bool Run(RichTextBox richTextBox)
			{
				try
				{
					_method.Invoke(Activator.CreateInstance(_method.ReflectedType), new object[0]);
					return true;
				}
				catch (System.Exception exception)
				{
					if (_expectedExceptionAttribute != null)
					{
						return true;
					}
					else
					{
						if (exception.InnerException is NUnit.Framework.AssertionException)
						{
							richTextBox.AppendText(">" + _method.ReflectedType.FullName + "." + _method.Name + "()\n");
							richTextBox.AppendText(exception.InnerException.Message + "\n\n\n");
						}
						else
						{
							richTextBox.AppendText(exception.InnerException.Message + "\n\n" + exception.InnerException.StackTrace + "\n\n\n");
						}
						return false;
					}
				}
			}
		}

		private class RichTextBoxWriter : System.IO.TextWriter
		{
			private readonly RichTextBox _richTextBox;

			public RichTextBoxWriter(RichTextBox richTextBox)
			{
				_richTextBox = richTextBox;
			}

			public override Encoding Encoding
			{
				get { throw new Exception("The method or operation is not implemented."); }
			}

			public override void Write(string value)
			{
				_richTextBox.AppendText(value);
			}

			public override void WriteLine()
			{
				_richTextBox.AppendText("\n");
			}

			public override void WriteLine(string value)
			{
				_richTextBox.AppendText(value + "\n");
			}
		}

		private class TypeFullNameComparer : IComparer<System.Type>
		{
			public int Compare(Type x, Type y)
			{
				return x.FullName.CompareTo(y.FullName);
			}
		}

	}
}