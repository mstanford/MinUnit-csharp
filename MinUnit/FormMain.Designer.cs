// -----------------------------------------------------------------------------
// 
// Copyright (c) 2014 Matthew Stanford
// 
// The person or persons who have associated work with this document (the 
// "Dedicator" or "Certifier") hereby either (a) certifies that, to the best of 
// his knowledge, the work of authorship identified is in the public domain of 
// the country from which the work is published, or (b) hereby dedicates 
// whatever copyright the dedicators holds in the work of authorship identified 
// below (the "Work") to the public domain. A certifier, moreover, dedicates any 
// copyright interest he may have in the associated work, and for these 
// purposes, is described as a "dedicator" below.
//
// A certifier has taken reasonable steps to verify the copyright status of this 
// work. Certifier recognizes that his good faith efforts may not shield him 
// from liability if in fact the work certified is not in the public domain.
//
// Dedicator makes this dedication for the benefit of the public at large and to 
// the detriment of the Dedicator's heirs and successors. Dedicator intends this 
// dedication to be an overt act of relinquishment in perpetuity of all present 
// and future rights under copyright law, whether vested or contingent, in the 
// Work. Dedicator understands that such relinquishment of all rights includes 
// the relinquishment of all rights to enforce (by lawsuit or otherwise) those 
// copyrights in the Work.
//
// Dedicator recognizes that, once placed in the public domain, the Work may be 
// freely reproduced, distributed, transmitted, used, modified, built upon, or 
// otherwise exploited by anyone for any purpose, commercial or non-commercial, 
// and in any way, including by methods that have not yet been invented or 
// conceived.
// 
// -----------------------------------------------------------------------------
namespace MinUnit
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.treeView = new System.Windows.Forms.TreeView();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.splitter = new System.Windows.Forms.Splitter();
			this.richTextBox = new System.Windows.Forms.RichTextBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeView.ImageIndex = 0;
			this.treeView.ImageList = this.imageList;
			this.treeView.Indent = 19;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = 0;
			this.treeView.ShowRootLines = false;
			this.treeView.Size = new System.Drawing.Size(300, 566);
			this.treeView.TabIndex = 0;
			this.treeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDoubleClick);
			this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_BeforeCollapse);
			this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "component.png");
			this.imageList.Images.SetKeyName(1, "bullet_square_blue.png");
			this.imageList.Images.SetKeyName(2, "bullet_square_green.png");
			this.imageList.Images.SetKeyName(3, "bullet_square_red.png");
			this.imageList.Images.SetKeyName(4, "bullet_square_yellow.png");
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(300, 0);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 566);
			this.splitter.TabIndex = 1;
			this.splitter.TabStop = false;
			// 
			// richTextBox
			// 
			this.richTextBox.BackColor = System.Drawing.Color.Black;
			this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox.ForeColor = System.Drawing.Color.Lime;
			this.richTextBox.Location = new System.Drawing.Point(303, 8);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ReadOnly = true;
			this.richTextBox.Size = new System.Drawing.Size(489, 558);
			this.richTextBox.TabIndex = 2;
			this.richTextBox.Text = "";
			// 
			// progressBar
			// 
			this.progressBar.BackColor = System.Drawing.Color.Black;
			this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
			this.progressBar.ForeColor = System.Drawing.Color.Lime;
			this.progressBar.Location = new System.Drawing.Point(303, 0);
			this.progressBar.Margin = new System.Windows.Forms.Padding(0);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(489, 8);
			this.progressBar.Step = 1;
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar.TabIndex = 3;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.richTextBox);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.treeView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FormMain";
			this.Text = "MinUnit";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.RichTextBox richTextBox;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ProgressBar progressBar;
	}
}

