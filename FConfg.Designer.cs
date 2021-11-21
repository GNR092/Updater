
namespace Updater
{
    partial class FConfg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.lb_info = new System.Windows.Forms.Label();
            this.txt_directory = new System.Windows.Forms.TextBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_info
            // 
            this.lb_info.AutoSize = true;
            this.lb_info.Location = new System.Drawing.Point(12, 26);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(114, 13);
            this.lb_info.TabIndex = 0;
            this.lb_info.Text = "Directorio de Minecraft";
            // 
            // txt_directory
            // 
            this.txt_directory.Location = new System.Drawing.Point(15, 42);
            this.txt_directory.Name = "txt_directory";
            this.txt_directory.Size = new System.Drawing.Size(284, 20);
            this.txt_directory.TabIndex = 1;
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(305, 40);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(52, 23);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "Buscar";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.Btn_open_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(133, 95);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(57, 25);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "Guardar";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.Btn_save_Click);
            // 
            // FConfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 134);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.txt_directory);
            this.Controls.Add(this.lb_info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FConfg";
            this.Text = "Confg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_info;
        private System.Windows.Forms.TextBox txt_directory;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.Button btn_save;
    }
}