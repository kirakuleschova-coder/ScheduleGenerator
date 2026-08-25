namespace ScheduleGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbSpecialty = new System.Windows.Forms.ComboBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.cmbTeacher = new System.Windows.Forms.ComboBox();
            this.numVariants = new System.Windows.Forms.NumericUpDown();
            this.btnLoadDisciplines = new System.Windows.Forms.Button();
            this.btnLoadTeachers = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSaveWord = new System.Windows.Forms.Button();
            this.btnOpenTeacherEditor = new System.Windows.Forms.Button();
            this.btnOpenGroupEditor = new System.Windows.Forms.Button();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numVariants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSpecialty
            // 
            this.cmbSpecialty.FormattingEnabled = true;
            this.cmbSpecialty.Location = new System.Drawing.Point(12, 12);
            this.cmbSpecialty.Name = "cmbSpecialty";
            this.cmbSpecialty.Size = new System.Drawing.Size(121, 21);
            this.cmbSpecialty.TabIndex = 0;
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(196, 12);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(121, 21);
            this.cmbGroup.TabIndex = 1;
            // 
            // cmbTeacher
            // 
            this.cmbTeacher.FormattingEnabled = true;
            this.cmbTeacher.Location = new System.Drawing.Point(370, 12);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Size = new System.Drawing.Size(121, 21);
            this.cmbTeacher.TabIndex = 2;
            // 
            // numVariants
            // 
            this.numVariants.Location = new System.Drawing.Point(545, 13);
            this.numVariants.Name = "numVariants";
            this.numVariants.Size = new System.Drawing.Size(120, 20);
            this.numVariants.TabIndex = 3;
            // 
            // btnLoadDisciplines
            // 
            this.btnLoadDisciplines.Location = new System.Drawing.Point(12, 72);
            this.btnLoadDisciplines.Name = "btnLoadDisciplines";
            this.btnLoadDisciplines.Size = new System.Drawing.Size(110, 26);
            this.btnLoadDisciplines.TabIndex = 4;
            this.btnLoadDisciplines.Text = "Загрузить план";
            this.btnLoadDisciplines.UseVisualStyleBackColor = true;
            // 
            // btnLoadTeachers
            // 
            this.btnLoadTeachers.Location = new System.Drawing.Point(146, 74);
            this.btnLoadTeachers.Name = "btnLoadTeachers";
            this.btnLoadTeachers.Size = new System.Drawing.Size(112, 24);
            this.btnLoadTeachers.TabIndex = 5;
            this.btnLoadTeachers.Text = "Загрузить препод.";
            this.btnLoadTeachers.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(688, 77);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(100, 24);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Сгенерировать";
            this.btnGenerate.UseVisualStyleBackColor = true;
            // 
            // btnSaveWord
            // 
            this.btnSaveWord.Location = new System.Drawing.Point(275, 74);
            this.btnSaveWord.Name = "btnSaveWord";
            this.btnSaveWord.Size = new System.Drawing.Size(135, 27);
            this.btnSaveWord.TabIndex = 7;
            this.btnSaveWord.Text = "Сохранить в Word";
            this.btnSaveWord.UseVisualStyleBackColor = true;
            // 
            // btnOpenTeacherEditor
            // 
            this.btnOpenTeacherEditor.Location = new System.Drawing.Point(424, 74);
            this.btnOpenTeacherEditor.Name = "btnOpenTeacherEditor";
            this.btnOpenTeacherEditor.Size = new System.Drawing.Size(112, 27);
            this.btnOpenTeacherEditor.TabIndex = 8;
            this.btnOpenTeacherEditor.Text = "редактор препод.";
            this.btnOpenTeacherEditor.UseVisualStyleBackColor = true;
            // 
            // btnOpenGroupEditor
            // 
            this.btnOpenGroupEditor.Location = new System.Drawing.Point(555, 77);
            this.btnOpenGroupEditor.Name = "btnOpenGroupEditor";
            this.btnOpenGroupEditor.Size = new System.Drawing.Size(110, 24);
            this.btnOpenGroupEditor.TabIndex = 9;
            this.btnOpenGroupEditor.Text = "Редактор групп";
            this.btnOpenGroupEditor.UseVisualStyleBackColor = true;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Location = new System.Drawing.Point(12, 126);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.Size = new System.Drawing.Size(776, 216);
            this.dgvSchedule.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(367, 383);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 13);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Готово";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dgvSchedule);
            this.Controls.Add(this.btnOpenGroupEditor);
            this.Controls.Add(this.btnOpenTeacherEditor);
            this.Controls.Add(this.btnSaveWord);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnLoadTeachers);
            this.Controls.Add(this.btnLoadDisciplines);
            this.Controls.Add(this.numVariants);
            this.Controls.Add(this.cmbTeacher);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.cmbSpecialty);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numVariants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSpecialty;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.ComboBox cmbTeacher;
        private System.Windows.Forms.NumericUpDown numVariants;
        private System.Windows.Forms.Button btnLoadDisciplines;
        private System.Windows.Forms.Button btnLoadTeachers;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSaveWord;
        private System.Windows.Forms.Button btnOpenTeacherEditor;
        private System.Windows.Forms.Button btnOpenGroupEditor;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Label lblStatus;
    }
}

