using System;

namespace FileHandlingSystem
{
    partial class frm_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_MainForm));
            this.grd_userData = new System.Windows.Forms.DataGridView();
            this.btnsearch = new System.Windows.Forms.ToolStripMenuItem();
            this.textbox_search = new System.Windows.Forms.TextBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btnclearsearch = new System.Windows.Forms.Button();
            this.lablesearch = new System.Windows.Forms.Label();
            this.btnaboutus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grd_userData)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_userData
            // 
            this.grd_userData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grd_userData.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grd_userData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grd_userData.Location = new System.Drawing.Point(15, 129);
            this.grd_userData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_userData.Name = "grd_userData";
            this.grd_userData.RowHeadersWidth = 51;
            this.grd_userData.RowTemplate.Height = 24;
            this.grd_userData.Size = new System.Drawing.Size(955, 464);
            this.grd_userData.TabIndex = 3;
            this.grd_userData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_userData_CellClick);
            this.grd_userData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_userData_CellContentClick);
            this.grd_userData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_userData_CellDoubleClick);
            this.grd_userData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_userData_CellValueChanged);
            // 
            // btnsearch
            // 
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(80, 29);
            this.btnsearch.Text = "Search";
            // 
            // textbox_search
            // 
            this.textbox_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textbox_search.Location = new System.Drawing.Point(622, 15);
            this.textbox_search.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textbox_search.Name = "textbox_search";
            this.textbox_search.Size = new System.Drawing.Size(332, 26);
            this.textbox_search.TabIndex = 6;
            this.textbox_search.TextChanged += new System.EventHandler(this.textbox_search_TextChanged);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(15, 16);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 36);
            this.btn_new.TabIndex = 7;
            this.btn_new.Text = "New";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(96, 16);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 36);
            this.btn_update.TabIndex = 8;
            this.btn_update.Text = "Update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(187, 16);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 36);
            this.btn_delete.TabIndex = 9;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btnclearsearch
            // 
            this.btnclearsearch.Location = new System.Drawing.Point(268, 16);
            this.btnclearsearch.Name = "btnclearsearch";
            this.btnclearsearch.Size = new System.Drawing.Size(174, 36);
            this.btnclearsearch.TabIndex = 10;
            this.btnclearsearch.Text = "Clear Search\\Filter";
            this.btnclearsearch.UseVisualStyleBackColor = true;
            this.btnclearsearch.Click += new System.EventHandler(this.btnclearsearch_Click);
            // 
            // lablesearch
            // 
            this.lablesearch.AutoSize = true;
            this.lablesearch.Location = new System.Drawing.Point(556, 19);
            this.lablesearch.Name = "lablesearch";
            this.lablesearch.Size = new System.Drawing.Size(60, 20);
            this.lablesearch.TabIndex = 11;
            this.lablesearch.Text = "Search";
            // 
            // btnaboutus
            // 
            this.btnaboutus.Location = new System.Drawing.Point(448, 16);
            this.btnaboutus.Name = "btnaboutus";
            this.btnaboutus.Size = new System.Drawing.Size(91, 36);
            this.btnaboutus.TabIndex = 12;
            this.btnaboutus.Text = "About us";
            this.btnaboutus.UseVisualStyleBackColor = true;
            this.btnaboutus.Click += new System.EventHandler(this.btnaboutus_Click);
            // 
            // frm_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 661);
            this.Controls.Add(this.btnaboutus);
            this.Controls.Add(this.lablesearch);
            this.Controls.Add(this.btnclearsearch);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.textbox_search);
            this.Controls.Add(this.grd_userData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1005, 721);
            this.MinimumSize = new System.Drawing.Size(1005, 721);
            this.Name = "frm_MainForm";
            this.Text = "File handling Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_userData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView grd_userData;
        private System.Windows.Forms.TextBox textbox_search;
        private System.Windows.Forms.ToolStripMenuItem btnsearch;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btnclearsearch;
        private System.Windows.Forms.Label lablesearch;
        private System.Windows.Forms.Button btnaboutus;
    }
}

