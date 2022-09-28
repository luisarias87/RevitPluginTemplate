namespace RevitPluginTemplate
{
    partial class Form1
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Create = new System.Windows.Forms.Button();
            this.sheet_titleBlock = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.viewsComboBox = new System.Windows.Forms.ComboBox();
            this.duplicateAsDependentRB = new System.Windows.Forms.RadioButton();
            this.duplicateWithDetailingRB = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.duplicateRB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.disciplineListBox = new System.Windows.Forms.CheckedListBox();
            this.areaName = new System.Windows.Forms.TextBox();
            this.Alldisciplines = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(453, 429);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(100, 28);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(561, 429);
            this.btn_Create.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(100, 28);
            this.btn_Create.TabIndex = 2;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // sheet_titleBlock
            // 
            this.sheet_titleBlock.FormattingEnabled = true;
            this.sheet_titleBlock.Location = new System.Drawing.Point(32, 135);
            this.sheet_titleBlock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sheet_titleBlock.Name = "sheet_titleBlock";
            this.sheet_titleBlock.Size = new System.Drawing.Size(368, 24);
            this.sheet_titleBlock.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Title Block";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Plan Views";
            // 
            // viewsComboBox
            // 
            this.viewsComboBox.FormattingEnabled = true;
            this.viewsComboBox.Location = new System.Drawing.Point(32, 49);
            this.viewsComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.viewsComboBox.Name = "viewsComboBox";
            this.viewsComboBox.Size = new System.Drawing.Size(368, 24);
            this.viewsComboBox.TabIndex = 3;
            // 
            // duplicateAsDependentRB
            // 
            this.duplicateAsDependentRB.AutoSize = true;
            this.duplicateAsDependentRB.Location = new System.Drawing.Point(453, 122);
            this.duplicateAsDependentRB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.duplicateAsDependentRB.Name = "duplicateAsDependentRB";
            this.duplicateAsDependentRB.Size = new System.Drawing.Size(174, 20);
            this.duplicateAsDependentRB.TabIndex = 4;
            this.duplicateAsDependentRB.TabStop = true;
            this.duplicateAsDependentRB.Text = "Duplicate As Dependent";
            this.duplicateAsDependentRB.UseVisualStyleBackColor = true;
            // 
            // duplicateWithDetailingRB
            // 
            this.duplicateWithDetailingRB.AutoSize = true;
            this.duplicateWithDetailingRB.Location = new System.Drawing.Point(453, 159);
            this.duplicateWithDetailingRB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.duplicateWithDetailingRB.Name = "duplicateWithDetailingRB";
            this.duplicateWithDetailingRB.Size = new System.Drawing.Size(170, 20);
            this.duplicateWithDetailingRB.TabIndex = 4;
            this.duplicateWithDetailingRB.TabStop = true;
            this.duplicateWithDetailingRB.Text = "Duplicate With Detailing";
            this.duplicateWithDetailingRB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(449, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Duplicate Options :";
            // 
            // duplicateRB
            // 
            this.duplicateRB.AutoSize = true;
            this.duplicateRB.Location = new System.Drawing.Point(453, 82);
            this.duplicateRB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.duplicateRB.Name = "duplicateRB";
            this.duplicateRB.Size = new System.Drawing.Size(117, 20);
            this.duplicateRB.TabIndex = 4;
            this.duplicateRB.TabStop = true;
            this.duplicateRB.Text = "Duplicate View";
            this.duplicateRB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 273);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Area Name";
            // 
            // disciplineListBox
            // 
            this.disciplineListBox.FormattingEnabled = true;
            this.disciplineListBox.Location = new System.Drawing.Point(452, 229);
            this.disciplineListBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disciplineListBox.Name = "disciplineListBox";
            this.disciplineListBox.Size = new System.Drawing.Size(204, 157);
            this.disciplineListBox.TabIndex = 6;
            // 
            // areaName
            // 
            this.areaName.Location = new System.Drawing.Point(37, 304);
            this.areaName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.areaName.Name = "areaName";
            this.areaName.Size = new System.Drawing.Size(368, 22);
            this.areaName.TabIndex = 1;
            // 
            // Alldisciplines
            // 
            this.Alldisciplines.AutoSize = true;
            this.Alldisciplines.Location = new System.Drawing.Point(452, 393);
            this.Alldisciplines.Name = "Alldisciplines";
            this.Alldisciplines.Size = new System.Drawing.Size(165, 20);
            this.Alldisciplines.TabIndex = 7;
            this.Alldisciplines.Text = "Check all / Check none";
            this.Alldisciplines.UseVisualStyleBackColor = true;
            this.Alldisciplines.CheckedChanged += new System.EventHandler(this.Alldisciplines_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(450, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(187, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Select one or more disciplines";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 470);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Alldisciplines);
            this.Controls.Add(this.disciplineListBox);
            this.Controls.Add(this.duplicateRB);
            this.Controls.Add(this.duplicateAsDependentRB);
            this.Controls.Add(this.duplicateWithDetailingRB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.viewsComboBox);
            this.Controls.Add(this.sheet_titleBlock);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.areaName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.ComboBox sheet_titleBlock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox viewsComboBox;
        private System.Windows.Forms.RadioButton duplicateAsDependentRB;
        private System.Windows.Forms.RadioButton duplicateWithDetailingRB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton duplicateRB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox disciplineListBox;
        private System.Windows.Forms.TextBox areaName;
        private System.Windows.Forms.CheckBox Alldisciplines;
        private System.Windows.Forms.Label label5;
    }
}