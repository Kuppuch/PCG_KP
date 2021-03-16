
namespace Kuppe_Roman_PRI_117_lab_14 {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.загрузитьМодельToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьМодельДляЗагрузкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenderTimer = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AnT
            // 
            this.AnT.AccumBits = ((byte)(0));
            this.AnT.AutoCheckErrors = false;
            this.AnT.AutoFinish = false;
            this.AnT.AutoMakeCurrent = true;
            this.AnT.AutoSwapBuffers = true;
            this.AnT.BackColor = System.Drawing.Color.Black;
            this.AnT.ColorBits = ((byte)(32));
            this.AnT.DepthBits = ((byte)(16));
            this.AnT.Location = new System.Drawing.Point(13, 27);
            this.AnT.Name = "AnT";
            this.AnT.Size = new System.Drawing.Size(745, 559);
            this.AnT.StencilBits = ((byte)(0));
            this.AnT.TabIndex = 0;
            this.AnT.Load += new System.EventHandler(this.AnT_Load);
            this.AnT.Scroll += new System.Windows.Forms.ScrollEventHandler(this.AnT_Scroll);
            this.AnT.Click += new System.EventHandler(this.AnT_Click);
            this.AnT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseDown_1);
            this.AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseMove);
            this.AnT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьМодельToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // загрузитьМодельToolStripMenuItem
            // 
            this.загрузитьМодельToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьМодельДляЗагрузкиToolStripMenuItem,
            this.помощьToolStripMenuItem});
            this.загрузитьМодельToolStripMenuItem.Name = "загрузитьМодельToolStripMenuItem";
            this.загрузитьМодельToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.загрузитьМодельToolStripMenuItem.Text = "Файл";
            this.загрузитьМодельToolStripMenuItem.Click += new System.EventHandler(this.загрузитьМодельToolStripMenuItem_Click);
            // 
            // выбратьМодельДляЗагрузкиToolStripMenuItem
            // 
            this.выбратьМодельДляЗагрузкиToolStripMenuItem.Name = "выбратьМодельДляЗагрузкиToolStripMenuItem";
            this.выбратьМодельДляЗагрузкиToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.выбратьМодельДляЗагрузкиToolStripMenuItem.Text = "Выбрать модель для загрузки";
            this.выбратьМодельДляЗагрузкиToolStripMenuItem.Click += new System.EventHandler(this.выбратьМодельДляЗагрузкиToolStripMenuItem_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // RenderTimer
            // 
            this.RenderTimer.Interval = 30;
            this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(764, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "crop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 598);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AnT);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Kuppe_Roman_PRI-117";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьМодельToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьМодельДляЗагрузкиToolStripMenuItem;
        private System.Windows.Forms.Timer RenderTimer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

