
namespace Updater
{
    partial class Updater
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.lb_name = new System.Windows.Forms.Label();
            this.pb_status = new System.Windows.Forms.ProgressBar();
            this.lb_percent = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(12, 19);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(0, 13);
            this.lb_name.TabIndex = 0;
            // 
            // pb_status
            // 
            this.pb_status.Location = new System.Drawing.Point(12, 35);
            this.pb_status.Name = "pb_status";
            this.pb_status.Size = new System.Drawing.Size(354, 13);
            this.pb_status.TabIndex = 1;
            // 
            // lb_percent
            // 
            this.lb_percent.AutoSize = true;
            this.lb_percent.Location = new System.Drawing.Point(372, 35);
            this.lb_percent.Name = "lb_percent";
            this.lb_percent.Size = new System.Drawing.Size(21, 13);
            this.lb_percent.TabIndex = 2;
            this.lb_percent.Text = "0%";
            // 
            // btn_exit
            // 
            this.btn_exit.Enabled = false;
            this.btn_exit.Location = new System.Drawing.Point(140, 54);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(75, 23);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Cerrar";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.Btn_exit_Click);
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 84);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.lb_percent);
            this.Controls.Add(this.pb_status);
            this.Controls.Add(this.lb_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.ProgressBar pb_status;
        private System.Windows.Forms.Label lb_percent;
        private System.Windows.Forms.Button btn_exit;
    }
}

