using System;
using System.Windows.Forms;
using OrderManagement;

namespace OrderManagementUI
{
    public partial class MainForm : Form
    {
        private readonly OrderService orderService;
        private readonly BindingSource orderBindingSource;
        private readonly BindingSource detailsBindingSource;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                orderService = new OrderService();
                
                // 初始化数据绑定
                orderBindingSource = new BindingSource();
                detailsBindingSource = new BindingSource();
                
                // 设置数据源
                var orders = orderService.QueryOrders(o => true);
                orderBindingSource.DataSource = orders;
                detailsBindingSource.DataSource = orderBindingSource;
                detailsBindingSource.DataMember = "Details";
                
                // 设置控件数据绑定
                orderDataGridView.DataSource = orderBindingSource;
                detailsDataGridView.DataSource = detailsBindingSource;
                
                // 设置列宽自适应
                orderDataGridView.AutoResizeColumns();
                detailsDataGridView.AutoResizeColumns();

                MessageBox.Show("窗体初始化成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化错误：{ex.Message}\n{ex.StackTrace}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using var editForm = new OrderEditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    orderService.AddOrder(editForm.Order);
                    RefreshOrderList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加订单错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (orderDataGridView.CurrentRow?.DataBoundItem is Order selectedOrder)
                {
                    using var editForm = new OrderEditForm(selectedOrder);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        orderService.UpdateOrder(selectedOrder.OrderId, editForm.Order);
                        RefreshOrderList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改订单错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (orderDataGridView.CurrentRow?.DataBoundItem is Order selectedOrder)
                {
                    if (MessageBox.Show("确定要删除这个订单吗？", "确认删除", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        orderService.RemoveOrder(selectedOrder.OrderId);
                        RefreshOrderList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除订单错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    RefreshOrderList();
                    return;
                }

                var orders = orderService.QueryOrders(o => 
                    o.OrderId.ToString().Contains(searchText) ||
                    o.Customer.Name.Contains(searchText) ||
                    o.Customer.Phone.Contains(searchText));
                
                orderBindingSource.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索订单错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshOrderList()
        {
            try
            {
                var orders = orderService.QueryOrders(o => true);
                orderBindingSource.DataSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新订单列表错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载订单列表错误：{ex.Message}", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 