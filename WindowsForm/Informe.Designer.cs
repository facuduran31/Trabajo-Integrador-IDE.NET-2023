namespace WindowsForm
{
    partial class Informe
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
            panel3 = new Panel();
            lblTitulo = new Label();
            panel4 = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            btnEmitir = new Button();
            dtpHasta = new DateTimePicker();
            label3 = new Label();
            dtpDesde = new DateTimePicker();
            label2 = new Label();
            dgvInforme = new DataGridView();
            btnPdf = new Button();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInforme).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightSeaGreen;
            panel3.Controls.Add(lblTitulo);
            panel3.Controls.Add(panel4);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1094, 100);
            panel3.TabIndex = 4;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Fill;
            lblTitulo.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1094, 100);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Informe de recaudación";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel4.BackColor = Color.White;
            panel4.Location = new Point(0, 72);
            panel4.Name = "panel4";
            panel4.Size = new Size(1753, 11);
            panel4.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(42, 123);
            label1.Name = "label1";
            label1.Size = new Size(462, 109);
            label1.TabIndex = 5;
            label1.Text = "Ingrese el rango de fechas sobre el cual desea emitir el informe:";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel1.Controls.Add(btnEmitir);
            panel1.Controls.Add(dtpHasta);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(dtpDesde);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(510, 123);
            panel1.Name = "panel1";
            panel1.Size = new Size(555, 109);
            panel1.TabIndex = 7;
            // 
            // btnEmitir
            // 
            btnEmitir.BackColor = Color.LightSeaGreen;
            btnEmitir.Location = new Point(421, 15);
            btnEmitir.Name = "btnEmitir";
            btnEmitir.Size = new Size(114, 61);
            btnEmitir.TabIndex = 4;
            btnEmitir.Text = "Emitir";
            btnEmitir.UseVisualStyleBackColor = false;
            btnEmitir.Click += btnEmitir_Click;
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(75, 61);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(313, 27);
            dtpHasta.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 67);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 2;
            label3.Text = "Hasta";
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(75, 11);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(313, 27);
            dtpDesde.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 15);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 0;
            label2.Text = "Desde";
            // 
            // dgvInforme
            // 
            dgvInforme.AllowUserToAddRows = false;
            dgvInforme.AllowUserToDeleteRows = false;
            dgvInforme.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvInforme.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInforme.Location = new Point(42, 265);
            dgvInforme.Name = "dgvInforme";
            dgvInforme.ReadOnly = true;
            dgvInforme.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvInforme.RowTemplate.Height = 29;
            dgvInforme.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInforme.Size = new Size(1024, 367);
            dgvInforme.TabIndex = 8;
            // 
            // btnPdf
            // 
            btnPdf.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPdf.BackColor = Color.LightSeaGreen;
            btnPdf.Location = new Point(925, 647);
            btnPdf.Name = "btnPdf";
            btnPdf.Size = new Size(140, 43);
            btnPdf.TabIndex = 9;
            btnPdf.Text = "Descargar PDF";
            btnPdf.UseVisualStyleBackColor = false;
            btnPdf.Click += btnPdf_Click;
            // 
            // Informe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 702);
            Controls.Add(btnPdf);
            Controls.Add(dgvInforme);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(panel3);
            Name = "Informe";
            Text = "Informe";
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInforme).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Panel panel4;
        private Label lblTitulo;
        private Label label1;
        private Panel panel1;
        private DateTimePicker dtpHasta;
        private Label label3;
        private DateTimePicker dtpDesde;
        private Label label2;
        private Button btnEmitir;
        private DataGridView dgvInforme;
        private Button btnPdf;
    }
}