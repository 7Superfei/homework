namespace OrderManagementUI
{
    partial class OrderEditForm
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
            txtOrderId = new TextBox();
            txtCustomerName = new TextBox();
            txtCustomerPhone = new TextBox();
            txtProductName = new TextBox();
            txtProductPrice = new TextBox();
            txtQuantity = new TextBox();
            detailsDataGridView = new DataGridView();
            btnAddDetail = new Button();
            btnRemoveDetail = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            
            // 设置窗体属性
            this.Text = "编辑订单";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // 创建布局面板
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(10)
            };
            
            // 订单信息面板
            var orderInfoPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3
            };
            
            // 添加订单信息控件
            orderInfoPanel.Controls.Add(new Label { Text = "订单号：" }, 0, 0);
            orderInfoPanel.Controls.Add(txtOrderId, 1, 0);
            orderInfoPanel.Controls.Add(new Label { Text = "客户姓名：" }, 0, 1);
            orderInfoPanel.Controls.Add(txtCustomerName, 1, 1);
            orderInfoPanel.Controls.Add(new Label { Text = "客户电话：" }, 0, 2);
            orderInfoPanel.Controls.Add(txtCustomerPhone, 1, 2);
            
            // 订单明细输入面板
            var detailInputPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2
            };
            
            // 添加明细输入控件
            detailInputPanel.Controls.Add(new Label { Text = "商品名称：" }, 0, 0);
            detailInputPanel.Controls.Add(txtProductName, 1, 0);
            detailInputPanel.Controls.Add(new Label { Text = "价格：" }, 2, 0);
            detailInputPanel.Controls.Add(txtProductPrice, 3, 0);
            detailInputPanel.Controls.Add(new Label { Text = "数量：" }, 0, 1);
            detailInputPanel.Controls.Add(txtQuantity, 1, 1);
            detailInputPanel.Controls.Add(btnAddDetail, 2, 1);
            detailInputPanel.Controls.Add(btnRemoveDetail, 3, 1);
            
            // 设置明细数据网格
            detailsDataGridView.Dock = DockStyle.Fill;
            detailsDataGridView.AllowUserToAddRows = false;
            detailsDataGridView.AllowUserToDeleteRows = false;
            detailsDataGridView.ReadOnly = true;
            detailsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            detailsDataGridView.MultiSelect = false;
            
            // 设置按钮
            btnAddDetail.Text = "添加明细";
            btnAddDetail.Click += btnAddDetail_Click;
            
            btnRemoveDetail.Text = "删除明细";
            btnRemoveDetail.Click += btnRemoveDetail_Click;
            
            btnSave.Text = "保存";
            btnSave.DialogResult = DialogResult.OK;
            btnSave.Click += btnSave_Click;
            
            btnCancel.Text = "取消";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Click += btnCancel_Click;
            
            // 添加控件到主面板
            mainPanel.Controls.Add(orderInfoPanel, 0, 0);
            mainPanel.SetColumnSpan(orderInfoPanel, 2);
            
            mainPanel.Controls.Add(detailInputPanel, 0, 1);
            mainPanel.SetColumnSpan(detailInputPanel, 2);
            
            mainPanel.Controls.Add(detailsDataGridView, 0, 2);
            mainPanel.SetColumnSpan(detailsDataGridView, 2);
            
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft
            };
            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnSave);
            
            mainPanel.Controls.Add(buttonPanel, 0, 3);
            mainPanel.SetColumnSpan(buttonPanel, 2);
            
            // 添加主面板到窗体
            this.Controls.Add(mainPanel);
        }

        private TextBox txtOrderId;
        private TextBox txtCustomerName;
        private TextBox txtCustomerPhone;
        private TextBox txtProductName;
        private TextBox txtProductPrice;
        private TextBox txtQuantity;
        private DataGridView detailsDataGridView;
        private Button btnAddDetail;
        private Button btnRemoveDetail;
        private Button btnSave;
        private Button btnCancel;
    }
} 