namespace AfhaForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.功能 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.失效状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.飞行阶段 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.失效影响 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.影响等级 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.安全性目标 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设计目标 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.验证方法 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.证明材料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(824, 426);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 29);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "保 存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(743, 426);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 29);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "读 取";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonCalculate
            // 
            this.buttonCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCalculate.Location = new System.Drawing.Point(905, 426);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(75, 29);
            this.buttonCalculate.TabIndex = 2;
            this.buttonCalculate.Text = "计 算";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编号,
            this.功能,
            this.失效状态,
            this.飞行阶段,
            this.失效影响,
            this.影响等级,
            this.安全性目标,
            this.设计目标,
            this.验证方法,
            this.证明材料,
            this.备注});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(968, 408);
            this.dataGridView1.TabIndex = 3;
            // 
            // 编号
            // 
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.Width = 60;
            // 
            // 功能
            // 
            this.功能.HeaderText = "功能";
            this.功能.Name = "功能";
            this.功能.Width = 80;
            // 
            // 失效状态
            // 
            this.失效状态.HeaderText = "失效状态";
            this.失效状态.Name = "失效状态";
            this.失效状态.Width = 80;
            // 
            // 飞行阶段
            // 
            this.飞行阶段.HeaderText = "飞行阶段";
            this.飞行阶段.Name = "飞行阶段";
            this.飞行阶段.Width = 80;
            // 
            // 失效影响
            // 
            this.失效影响.HeaderText = "失效影响";
            this.失效影响.Name = "失效影响";
            this.失效影响.Width = 80;
            // 
            // 影响等级
            // 
            this.影响等级.HeaderText = "影响等级";
            this.影响等级.Name = "影响等级";
            this.影响等级.Width = 80;
            // 
            // 安全性目标
            // 
            this.安全性目标.HeaderText = "安全性目标";
            this.安全性目标.Name = "安全性目标";
            // 
            // 设计目标
            // 
            this.设计目标.HeaderText = "设计目标";
            this.设计目标.Name = "设计目标";
            this.设计目标.Width = 80;
            // 
            // 验证方法
            // 
            this.验证方法.HeaderText = "验证方法";
            this.验证方法.Name = "验证方法";
            this.验证方法.Width = 80;
            // 
            // 证明材料
            // 
            this.证明材料.HeaderText = "证明材料";
            this.证明材料.Name = "证明材料";
            // 
            // 备注
            // 
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 467);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonCalculate);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PSSA";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 功能;
        private System.Windows.Forms.DataGridViewTextBoxColumn 失效状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 飞行阶段;
        private System.Windows.Forms.DataGridViewTextBoxColumn 失效影响;
        private System.Windows.Forms.DataGridViewTextBoxColumn 影响等级;
        private System.Windows.Forms.DataGridViewTextBoxColumn 安全性目标;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设计目标;
        private System.Windows.Forms.DataGridViewTextBoxColumn 验证方法;
        private System.Windows.Forms.DataGridViewTextBoxColumn 证明材料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
    }
}

