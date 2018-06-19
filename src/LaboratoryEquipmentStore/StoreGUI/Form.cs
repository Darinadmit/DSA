using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.Facade;

namespace StoreGUI
{
    enum StoreMode
    {
        StartPage = 0,
        ClientPage = 1,
        ManagerPage = 2,
        SupplierPage = 3,
        OrderPage = 4
    }

    public partial class Form : System.Windows.Forms.Form
    {
        //viewers

        internal StoreMode storeMode;
        IFacade model;

        void installStartPage()
        {
            storeMode = StoreMode.StartPage;
            model.SetId(-1);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(230, 100);
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.Text = "LogIn";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Visible = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Visible = false;
            //
            //labelDescr
            //
            this.labelDescr.Text = "Start Page";
            // 
            // labelForPersonData
            // 
            this.labelForPersonData.Location = new System.Drawing.Point(12, 104);
            this.labelForPersonData.Size = new System.Drawing.Size(27, 13);
            this.labelForPersonData.Text = "I am";
            this.labelForPersonData.Visible = true;
            // 
            // buttonChangeStatus
            // 
            this.buttonChangeStatus.Visible = false;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Visible = false;
            // 
            // listBox
            // 
            this.listBox.Items.AddRange(roles);
            this.listBox.Location = new System.Drawing.Point(45, 84);
            this.listBox.Size = new System.Drawing.Size(65, 43);
            this.listBox.Visible = true;
            // 
            // textBox
            // 
            this.textBox.Text = "";
            this.textBox.Location = new System.Drawing.Point(170, 101);
            this.textBox.Size = new System.Drawing.Size(48, 20);
            this.textBox.Visible = true;
            //
            //textBoxCount
            //
            this.textBoxCount.Text = "";
            this.textBoxCount.Visible = false;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(120, 105);
            this.label.Size = new System.Drawing.Size(31, 13);
            this.label.Text = "my id";
            this.label.Visible = true;
            //
            //dataGrid
            //
            this.dataGrid.Visible = false;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = new System.Drawing.Size(375, 200);
        }

        void installManagerPage()
        {
            storeMode = StoreMode.ManagerPage;
            if (textBox.Text != "")
                model.SetId(Convert.ToInt32(textBox.Text));

            this.Size = new System.Drawing.Size(375, 400);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(206, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            this.buttonCreate.Text = "New orders";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Visible = true;
            //
            //buttonBack
            //
            this.buttonBack.Visible = true;
            this.buttonCreate.Location = new System.Drawing.Point(246, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            // 
            // buttonChangeStatus
            // 
            this.buttonChangeStatus.Visible = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.Visible = true;
            //
            //labelDescr
            //
            this.labelDescr.Text = "Manager Page";
            // 
            // labelForPersonData
            // 
            this.labelForPersonData.Location = new System.Drawing.Point(12, 84);
            this.labelForPersonData.Size = new System.Drawing.Size(27, 13);
            this.labelForPersonData.Text = "Name " + model.GetManager().Name + "\nId " + model.GetManager().Id;
            this.labelForPersonData.Visible = true;
            // 
            // listBox
            // 
            this.listBox.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(60, 139);
            this.textBox.Size = new System.Drawing.Size(48, 20);
            this.textBox.Text = "";
            this.textBox.Visible = true;
            //
            //textBoxCount
            //
            this.textBoxCount.Text = "";
            this.textBoxCount.Visible = false;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(12, 139);
            this.label.Size = new System.Drawing.Size(48, 20);
            this.label.Text = "Order id";
            this.textBox.Visible = true;
            //
            //dataGrid
            //
            this.dataGrid.Visible = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Location = new System.Drawing.Point(12, 177);
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id", HeaderText = "Id", Width = 20 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Product", HeaderText = "Product", Width = 50 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Count", HeaderText = "Count", Width = 40 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "In stock", HeaderText = "In stock", Width = 40 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status", Width = 103 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Client", HeaderText = "Client", Width = 75 });
            List<Store.BusinessLogic.Order> orders = model.GetListOfOrders();
            foreach (Store.BusinessLogic.Order order in orders)
            {
                this.dataGrid.Rows.Add(order.Id.ToString(), order.OrderProduct.Name, order.Count.ToString(), order.OrderProduct.Count.ToString(), order.OrderStatus.ToString(), order.OrderClient.Name);
            }
        }

        void installClientPage()
        {
            storeMode = StoreMode.ClientPage;
            if (textBox.Text != "")
                model.SetId(Convert.ToInt32(textBox.Text));

            this.Size = new System.Drawing.Size(375, 400);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(206, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            this.buttonCreate.Text = "New order";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Visible = true;
            //
            //buttonBack
            //
            this.buttonBack.Visible = true;
            this.buttonCreate.Location = new System.Drawing.Point(246, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            // 
            // buttonChangeStatus
            // 
            this.buttonChangeStatus.Visible = true;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.Visible = true;
            //
            //labelDescr
            //
            this.labelDescr.Text = "Client Page";
            // 
            // labelForPersonData
            // 
            this.labelForPersonData.Location = new System.Drawing.Point(12, 84);
            this.labelForPersonData.Size = new System.Drawing.Size(27, 13);
            this.labelForPersonData.Text = "Name " + model.GetClient().Name + "\nId " + model.GetClient().Id + "\nAddress " + model.GetClient().Address;
            this.labelForPersonData.Visible = true;
            // 
            // listBox
            // 
            this.listBox.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(60, 139);
            this.textBox.Size = new System.Drawing.Size(48, 20);
            this.textBox.Text = "";
            this.textBox.Visible = true;
            //
            //textBoxCount
            //
            this.textBoxCount.Text = "";
            this.textBoxCount.Visible = false;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(12, 139);
            this.label.Size = new System.Drawing.Size(48, 20);
            this.label.Text = "Order id";
            this.textBox.Visible = true;
            //
            //dataGrid
            //
            this.dataGrid.Visible = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Location = new System.Drawing.Point(12, 177);
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id", HeaderText = "Id", Width = 20 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Product", HeaderText = "Product", Width = 50 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Count", HeaderText = "Count", Width = 40 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Price", HeaderText = "Price", Width = 40 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status", Width = 103 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Manager", HeaderText = "Manager", Width = 75 });
            List<Store.BusinessLogic.Order> orders = model.GetListOfOrdersByClient();
            foreach (Store.BusinessLogic.Order order in orders)
            {
                this.dataGrid.Rows.Add(order.Id.ToString(), order.OrderProduct.Name, order.Count.ToString(), order.Price.ToString(), order.OrderStatus.ToString(), order.OrderManager.Name);
            }
        }

        void installSupplierPage()
        {
            storeMode = StoreMode.SupplierPage;
            model.SetId(Convert.ToInt32(textBox.Text));

            this.Size = new System.Drawing.Size(375, 400);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Visible = false;
            //
            //buttonBack
            //
            this.buttonBack.Visible = true;
            this.buttonCreate.Location = new System.Drawing.Point(246, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            // 
            // buttonChangeStatus
            // 
            this.buttonChangeStatus.Visible = false;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Text = "Send product";
            this.buttonRemove.Visible = true;
            //
            //labelDescr
            //
            this.labelDescr.Text = "Supplier Page";
            // 
            // labelForPersonData
            // 
            this.labelForPersonData.Location = new System.Drawing.Point(12, 84);
            this.labelForPersonData.Size = new System.Drawing.Size(27, 13);
            this.labelForPersonData.Text = "Name " + model.GetSupplier().Name + "\nId " + model.GetClient().Id + "\nAddress " + model.GetClient().Address;
            this.labelForPersonData.Visible = true;
            // 
            // listBox
            // 
            this.listBox.Visible = false;
            // 
            // textBox
            // 
            this.textBox.BringToFront();
            this.textBox.Location = new System.Drawing.Point(70, 139);
            this.textBox.Size = new System.Drawing.Size(40, 20);
            this.textBox.Text = "";
            this.textBox.Visible = true;
            //
            //textBoxCount
            //
            this.textBoxCount.Text = "";
            this.textBoxCount.Location = new System.Drawing.Point(155, 139);
            this.textBoxCount.Size = new System.Drawing.Size(30, 20);
            this.textBoxCount.Visible = true;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(12, 139);
            this.label.Size = new System.Drawing.Size(48, 20);
            this.label.Text = "Product id                  Count";
            this.textBox.Visible = true;
            //
            //dataGrid
            //
            this.dataGrid.Visible = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Location = new System.Drawing.Point(12, 177);
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id", HeaderText = "Id", Width = 30 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Name", HeaderText = "Name", Width = 150 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Price", HeaderText = "Price", Width = 148 });
            List<Store.BusinessLogic.Product> orders = model.GetSupplier().GetListOfProducts();
            foreach (Store.BusinessLogic.Product product in orders)
            {
                this.dataGrid.Rows.Add(product.Id.ToString(), product.Name, product.Price.ToString());
            }
        }

        void installOrderPage()
        {
            storeMode = StoreMode.OrderPage;
            //model.SetId(Convert.ToInt32(textBox.Text));

            this.Size = new System.Drawing.Size(375, 400);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Text = "Create order";
            this.buttonCreate.Visible = true;
            //
            //buttonBack
            //
            this.buttonBack.Visible = true;
            this.buttonCreate.Location = new System.Drawing.Point(246, 100);
            this.buttonCreate.Size = new System.Drawing.Size(100, 23);
            // 
            // buttonChangeStatus
            // 
            this.buttonChangeStatus.Visible = false;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Visible = false;
            //
            //labelDescr
            //
            this.labelDescr.Text = "Order Page";
            // 
            // labelForPersonData
            // 
            this.labelForPersonData.Visible = false;
            // 
            // listBox
            // 
            this.listBox.Visible = false;
            // 
            // textBox
            // 
            this.textBox.BringToFront();
            this.textBox.Location = new System.Drawing.Point(70, 139);
            this.textBox.Size = new System.Drawing.Size(40, 20);
            this.textBox.Text = "";
            this.textBox.Visible = true;
            //
            //textBoxCount
            //
            this.textBoxCount.Text = "";
            this.textBoxCount.Location = new System.Drawing.Point(155, 139);
            this.textBoxCount.Size = new System.Drawing.Size(30, 20);
            this.textBoxCount.Visible = true;
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(12, 139);
            this.label.Size = new System.Drawing.Size(48, 20);
            this.label.Text = "Product id                  Count";
            this.textBox.Visible = true;
            //
            //dataGrid
            //
            this.dataGrid.Visible = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.Location = new System.Drawing.Point(12, 177);
            this.dataGrid.Columns.Clear();
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id", HeaderText = "Id", Width = 30 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Name", HeaderText = "Name", Width = 100 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Count", HeaderText = "Count", Width = 100 });
            this.dataGrid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Price", HeaderText = "Price", Width = 98 });
            List<Store.BusinessLogic.Product> orders = model.GetAllProducts();
            foreach (Store.BusinessLogic.Product product in orders)
            {
                this.dataGrid.Rows.Add(product.Id.ToString(), product.Name, product.Count.ToString(), product.Price.ToString());
            }
        }

        public Form()
        {
            model = new Facade();
            InitializeComponent();
            installStartPage();
        }

        object[] roles = new object[] {
            "Client",
            "Manager",
            "Suplier"};

        //controllers

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                switch (storeMode)
                {
                    case StoreMode.ClientPage:
                        installOrderPage();
                        break;
                    case StoreMode.ManagerPage:
                        model.CreateNewOrders();
                        this.textBox.Text = "";
                        installManagerPage();
                        break;
                    case StoreMode.OrderPage:
                        model.RequestNewOrder(model.GetProduct(Convert.ToInt32(this.textBox.Text)), Convert.ToInt32(this.textBoxCount.Text));
                        this.textBox.Text = "";
                        installClientPage();
                        break;
                    case StoreMode.StartPage:
                        switch (listBox.SelectedIndex)
                        {
                            case 0:
                                installClientPage();
                                break;
                            case 1:
                                installManagerPage();
                                break;
                            case 2:
                                installSupplierPage();
                                break;
                        }
                        break;
                }
            }
            catch
            {
                installStartPage();
                MessageBox.Show("Incorrect action! Сheck the data, please.");
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            try
            {
                switch (storeMode)
                {
                    case StoreMode.ManagerPage:
                        installStartPage();
                        break;
                    case StoreMode.OrderPage:
                        installClientPage();
                        break;
                    case StoreMode.ClientPage:
                        installStartPage();
                        break;
                    case StoreMode.SupplierPage:
                        installStartPage();
                        break;
                }
            }
            catch
            {
                installStartPage();
                MessageBox.Show("Incorrect action! Сheck the data, please.");
            }
        }

        private void buttonChangeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                switch (storeMode)
                {
                    case StoreMode.ManagerPage:
                        model.NextStatusOfOrderForManager(Convert.ToInt32(this.textBox.Text));
                        this.textBox.Text = "";
                        installManagerPage();
                        break;
                    case StoreMode.ClientPage:
                        model.NextStatusOfOrderForClient(Convert.ToInt32(this.textBox.Text));
                        this.textBox.Text = "";
                        installClientPage();
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Incorrect action! Сheck the data, please.");
                //installStartPage();
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                switch (storeMode)
                {
                    case StoreMode.ManagerPage:
                        model.DeleteOrder(model.GetOrder(Convert.ToInt32(this.textBox.Text)));
                        this.textBox.Text = "";
                        installManagerPage();
                        break;
                    case StoreMode.ClientPage:
                        model.DeleteOrder(model.GetOrder(Convert.ToInt32(this.textBox.Text)));
                        this.textBox.Text = "";
                        installClientPage();
                        break;
                    case StoreMode.SupplierPage:
                        model.GetSupplier().SendProduct(model.GetProduct(Convert.ToInt32(this.textBox.Text)), Convert.ToInt32(this.textBoxCount.Text));
                        break;
                }
            }
            catch
            {
                //installStartPage();
                MessageBox.Show("Incorrect action! Сheck the data, please.");
            }
        }
    }
}
