using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations();

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = txtCustomerBalance.Text,
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
            };
            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri Ekleme işlemi Başarılı.", "Uyarı", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = customerOperations.GetAllCustomers();
            dataGridView1.DataSource = customers;
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            customerOperations.DeleteCustomer(customerId);
            MessageBox.Show("Müşteri Silme işlemi Başarılı.");
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            var updateCustomer = new Customer()
            {
                CustomerId = customerId,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = txtCustomerBalance.Text,
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
            };
            customerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("Müşteri Güncelleme işlemi Başarılı.");
        }

        private void btnGetByCustomerId_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            Customer customers = customerOperations.GetCustomerById(id);
            dataGridView1.DataSource = new List<Customer>
            {
                customers
            };
        }
    }
}

