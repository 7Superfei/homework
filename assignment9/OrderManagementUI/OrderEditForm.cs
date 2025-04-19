using System;
using System.Windows.Forms;
using OrderManagement;

namespace OrderManagementUI
{
    public partial class OrderEditForm : Form
    {
        public Order Order { get; private set; }
        private readonly BindingSource detailsBindingSource;

        public OrderEditForm(Order? order = null)
        {
            InitializeComponent();
            
            detailsBindingSource = new BindingSource();
            detailsDataGridView.DataSource = detailsBindingSource;
            
            if (order != null)
            {
                // 编辑模式
                Order = order;
                txtOrderId.Text = order.OrderId.ToString();
                txtCustomerName.Text = order.Customer.Name;
                txtCustomerPhone.Text = order.Customer.Phone;
                detailsBindingSource.DataSource = order.Details;
            }
            else
            {
                // 新建模式
                Order = new Order
                {
                    OrderId = 0,
                    Customer = new Customer { Name = "", Phone = "" },
                    Details = new List<OrderDetails>()
                };
                detailsBindingSource.DataSource = Order.Details;
            }
        }

        private void btnAddDetail_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                Name = txtProductName.Text,
                Price = decimal.Parse(txtProductPrice.Text)
            };

            var detail = new OrderDetails
            {
                Product = product,
                Quantity = int.Parse(txtQuantity.Text)
            };

            Order.Details.Add(detail);
            detailsBindingSource.ResetBindings(false);
            ClearDetailInputs();
        }

        private void btnRemoveDetail_Click(object sender, EventArgs e)
        {
            if (detailsDataGridView.CurrentRow?.DataBoundItem is OrderDetails detail)
            {
                Order.Details.Remove(detail);
                detailsBindingSource.ResetBindings(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                Order.OrderId = int.Parse(txtOrderId.Text);
                Order.Customer.Name = txtCustomerName.Text;
                Order.Customer.Phone = txtCustomerPhone.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtOrderId.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerPhone.Text))
            {
                MessageBox.Show("请填写所有必填字段。", "验证错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtOrderId.Text, out _))
            {
                MessageBox.Show("订单号必须是有效的数字。", "验证错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Order.Details.Count == 0)
            {
                MessageBox.Show("请至少添加一个订单明细。", "验证错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearDetailInputs()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtQuantity.Clear();
        }
    }
} 