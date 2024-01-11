namespace Diplomski_rad___s21_20.Views
{
    partial class Korpa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Korpa));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnIsprazniKorpu = new System.Windows.Forms.Button();
            this.btnZavrsiKupovinu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(60, 81);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1006, 434);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnIsprazniKorpu
            // 
            this.btnIsprazniKorpu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIsprazniKorpu.Image = ((System.Drawing.Image)(resources.GetObject("btnIsprazniKorpu.Image")));
            this.btnIsprazniKorpu.Location = new System.Drawing.Point(1166, 102);
            this.btnIsprazniKorpu.Name = "btnIsprazniKorpu";
            this.btnIsprazniKorpu.Size = new System.Drawing.Size(128, 91);
            this.btnIsprazniKorpu.TabIndex = 1;
            this.btnIsprazniKorpu.Text = "Isprazni Korpu";
            this.btnIsprazniKorpu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnIsprazniKorpu.UseVisualStyleBackColor = true;
            // 
            // btnZavrsiKupovinu
            // 
            this.btnZavrsiKupovinu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZavrsiKupovinu.Image = ((System.Drawing.Image)(resources.GetObject("btnZavrsiKupovinu.Image")));
            this.btnZavrsiKupovinu.Location = new System.Drawing.Point(1166, 239);
            this.btnZavrsiKupovinu.Name = "btnZavrsiKupovinu";
            this.btnZavrsiKupovinu.Size = new System.Drawing.Size(128, 91);
            this.btnZavrsiKupovinu.TabIndex = 2;
            this.btnZavrsiKupovinu.Text = "Zavrsi Kupovinu";
            this.btnZavrsiKupovinu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnZavrsiKupovinu.UseVisualStyleBackColor = true;
            // 
            // Korpa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1399, 553);
            this.Controls.Add(this.btnZavrsiKupovinu);
            this.Controls.Add(this.btnIsprazniKorpu);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Korpa";
            this.Text = "Korpa";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnIsprazniKorpu;
        private System.Windows.Forms.Button btnZavrsiKupovinu;
    }
}