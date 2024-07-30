namespace WinForms
{
	partial class MainForm
	{
		private System.ComponentModel.IContainer components;
		
		protected override void Dispose(bool disposing)
		{
			if (disposing)
				components?.Dispose();
			base.Dispose(disposing);
		}
		
		private void InitializeComponent()
		{
			this.components      = new System.ComponentModel.Container();
			this.AutoScaleMode   = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize      = new System.Drawing.Size(250, 0);
			this.Text            = "NoHeadache";
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Icon            = new System.Drawing.Icon("Resources/NoHeadache.ico");
		}
	}
}