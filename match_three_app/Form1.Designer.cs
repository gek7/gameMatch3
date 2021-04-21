namespace match_three_app
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
            this.button1 = new System.Windows.Forms.Button();
            this.game_match_three1 = new match_three.Game_match_three();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(542, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 100);
            this.button1.TabIndex = 1;
            this.button1.Text = "НОВАЯ ИГРА";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // game_match_three1
            // 
            this.game_match_three1.CellCount = 10;
            this.game_match_three1.Location = new System.Drawing.Point(12, 1);
            this.game_match_three1.Name = "game_match_three1";
            this.game_match_three1.Score = 0;
            this.game_match_three1.Size = new System.Drawing.Size(500, 500);
            this.game_match_three1.TabIndex = 0;
            this.game_match_three1.Text = "game_match_three1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(597, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "Счёт: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 715);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.game_match_three1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private match_three.Game_match_three game_match_three1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

