using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LAB2
{
    partial class VideoMonitoringForm
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
            this.videoPictureBox = new System.Windows.Forms.PictureBox();
            this.StartMonitoringButton = new System.Windows.Forms.Button();
            this.StopMonitoringButton = new System.Windows.Forms.Button();
            this.observationTextBox = new System.Windows.Forms.TextBox();
            this.importanceComboBox = new System.Windows.Forms.ComboBox();
            this.recordedByTextBox = new System.Windows.Forms.TextBox();
            this.saveObservationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.videoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // videoPictureBox
            // 
            this.videoPictureBox.BackColor = System.Drawing.Color.LightGray;
            this.videoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.videoPictureBox.Name = "videoPictureBox";
            this.videoPictureBox.Size = new System.Drawing.Size(760, 400);
            this.videoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.videoPictureBox.TabIndex = 0;
            this.videoPictureBox.TabStop = false;
            // 
            // StartMonitoringButton
            // 
            this.StartMonitoringButton.BackColor = System.Drawing.Color.LightGreen;
            this.StartMonitoringButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartMonitoringButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StartMonitoringButton.Location = new System.Drawing.Point(12, 430);
            this.StartMonitoringButton.Name = "StartMonitoringButton";
            this.StartMonitoringButton.Size = new System.Drawing.Size(180, 40);
            this.StartMonitoringButton.TabIndex = 1;
            this.StartMonitoringButton.Text = "Почати моніторинг";
            this.StartMonitoringButton.UseVisualStyleBackColor = false;
            this.StartMonitoringButton.Click += new System.EventHandler(this.StartMonitoringButton_Click);
            // 
            // StopMonitoringButton
            // 
            this.StopMonitoringButton.BackColor = System.Drawing.Color.LightCoral;
            this.StopMonitoringButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopMonitoringButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.StopMonitoringButton.Location = new System.Drawing.Point(592, 430);
            this.StopMonitoringButton.Name = "StopMonitoringButton";
            this.StopMonitoringButton.Size = new System.Drawing.Size(180, 40);
            this.StopMonitoringButton.TabIndex = 2;
            this.StopMonitoringButton.Text = "Зупинити моніторинг";
            this.StopMonitoringButton.UseVisualStyleBackColor = false;
            this.StopMonitoringButton.Click += new System.EventHandler(this.StopMonitoringButton_Click);
            // 
            // observationTextBox
            // 
            this.observationTextBox.Location = new System.Drawing.Point(12, 480);
            this.observationTextBox.Name = "observationTextBox";
            this.observationTextBox.Size = new System.Drawing.Size(300, 20);
            this.observationTextBox.TabIndex = 3;
            // 
            // importanceComboBox
            // 
            this.importanceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.importanceComboBox.FormattingEnabled = true;
            this.importanceComboBox.Items.AddRange(new object[] {
            "Критичні",
            "Помірні",
            "Інформаційні"});
            this.importanceComboBox.Location = new System.Drawing.Point(12, 510);
            this.importanceComboBox.Name = "importanceComboBox";
            this.importanceComboBox.Size = new System.Drawing.Size(300, 20);
            this.importanceComboBox.TabIndex = 4;
            // 
            // recordedByTextBox
            // 
            this.recordedByTextBox.Location = new System.Drawing.Point(12, 540);
            this.recordedByTextBox.Name = "recordedByTextBox";
            this.recordedByTextBox.Size = new System.Drawing.Size(300, 20);
            this.recordedByTextBox.TabIndex = 5;
            // 
            // saveObservationButton
            // 
            this.saveObservationButton.Location = new System.Drawing.Point(12, 570);
            this.saveObservationButton.Name = "saveObservationButton";
            this.saveObservationButton.Size = new System.Drawing.Size(100, 30);
            this.saveObservationButton.TabIndex = 6;
            this.saveObservationButton.Text = "Зберегти";
            this.saveObservationButton.UseVisualStyleBackColor = true;
            this.saveObservationButton.Click += new System.EventHandler(this.SaveObservationButton_Click);
            // 
            // VideoMonitoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(784, 620);
            this.Controls.Add(this.saveObservationButton);
            this.Controls.Add(this.recordedByTextBox);
            this.Controls.Add(this.importanceComboBox);
            this.Controls.Add(this.observationTextBox);
            this.Controls.Add(this.StopMonitoringButton);
            this.Controls.Add(this.StartMonitoringButton);
            this.Controls.Add(this.videoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VideoMonitoringForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Відео Моніторинг";
            ((System.ComponentModel.ISupportInitialize)(this.videoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox videoPictureBox;
        private System.Windows.Forms.Button StartMonitoringButton;
        private System.Windows.Forms.Button StopMonitoringButton;
        private System.Windows.Forms.TextBox observationTextBox;
        private System.Windows.Forms.ComboBox importanceComboBox;
        private System.Windows.Forms.TextBox recordedByTextBox;
        private System.Windows.Forms.Button saveObservationButton;

        private void SaveScreenshotButton_Click(object sender, EventArgs e)
        {
            if (videoPictureBox.Image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = "screenshot";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        videoPictureBox.Image.Save(saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Немає зображення для збереження!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
