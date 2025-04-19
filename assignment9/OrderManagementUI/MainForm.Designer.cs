namespace OrderManagementUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // 创建控件
            orderDataGridView = new DataGridView();
            detailsDataGridView = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            panel2 = new Panel();
            
            // 设置窗体属性
            this.Text = "订单管理系统";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // 设置SplitContainer
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Orientation = Orientation.Vertical;
            splitContainer1.SplitterDistance = 400;
            
            // 设置Panel1（订单列表）
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(orderDataGridView);
            panel1.Controls.Add(panel2);
            
            // 设置Panel2（搜索和按钮）
            panel2.Dock = DockStyle.Top;
            panel2.Height = 40;
            panel2.Controls.Add(txtSearch);
            panel2.Controls.Add(btnSearch);
            panel2.Controls.Add(btnAdd);
            panel2.Controls.Add(btnEdit);
            panel2.Controls.Add(btnDelete);
            
            // 设置搜索框
            txtSearch.Location = new Point(10, 10);
            txtSearch.Size = new Size(200, 23);
            txtSearch.PlaceholderText = "输入订单号、客户名或电话搜索";
            
            // 设置按钮
            btnSearch.Location = new Point(220, 10);
            btnSearch.Size = new Size(75, 23);
            btnSearch.Text = "搜索";
            btnSearch.Click += btnSearch_Click;
            
            btnAdd.Location = new Point(310, 10);
            btnAdd.Size = new Size(75, 23);
            btnAdd.Text = "添加";
            btnAdd.Click += btnAdd_Click;
            
            btnEdit.Location = new Point(395, 10);
            btnEdit.Size = new Size(75, 23);
            btnEdit.Text = "修改";
            btnEdit.Click += btnEdit_Click;
            
            btnDelete.Location = new Point(480, 10);
            btnDelete.Size = new Size(75, 23);
            btnDelete.Text = "删除";
            btnDelete.Click += btnDelete_Click;
            
            // 设置DataGridView
            orderDataGridView.Dock = DockStyle.Fill;
            orderDataGridView.AllowUserToAddRows = false;
            orderDataGridView.AllowUserToDeleteRows = false;
            orderDataGridView.ReadOnly = true;
            orderDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            orderDataGridView.MultiSelect = false;
            
            detailsDataGridView.Dock = DockStyle.Fill;
            detailsDataGridView.AllowUserToAddRows = false;
            detailsDataGridView.AllowUserToDeleteRows = false;
            detailsDataGridView.ReadOnly = true;
            
            // 添加控件到窗体
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(detailsDataGridView);
            this.Controls.Add(splitContainer1);
            
            // 窗体加载事件
            this.Load += MainForm_Load;
        }

        private DataGridView orderDataGridView;
        private DataGridView detailsDataGridView;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private Panel panel2;
    }
} 