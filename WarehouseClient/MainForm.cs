﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseClient.ProductCategoryManagement;
using WarehouseClient.ProdManagForm;
using WarehouseClient.WWS;
using WarehouseClient.Constants;

namespace WarehouseClient
{
     public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAllStaticInfo();
            switch (loginUser.RoleGroupId)
            {
                case 1: {
                        try
                        {
                            using (var client = new WarehouseServiceClient(ServiceParametor.Parametor))
                            {
                                foreach (User user in client.SelectActiveUsers())
                                {
                                    ApplicationData.Users.Add(user.Id.Value, user);
                                }
                                
                                dataGridView1.DataSource = ApplicationData.Users.Values.ToList();
                            }
                            dataGridView1.Columns[2].Visible = false;
                            dataGridView1.Columns[0].Visible = false;
                        }
                        catch (Exception exeption) {
                            
                            Console.WriteLine(exeption.Message);
                            
                        }
                        break;
                    }
               default:
                        {
                        tabControl1.TabPages.Remove(UserTab);
                        tabControl1.TabPages.Remove(RoleTab);
                        tabControl1.TabPages.Remove(RoleMapTab);
                        break;
                        }
            } 
        }


        private void MainForm_Activated(object sender, EventArgs e)
        {
   
        }



        private static void LoadAllStaticInfo()
        {
            #region Load Role Groups
            try
            {
                using (WarehouseServiceClient wwsClient = new WarehouseServiceClient("HTTP"))
                {
                    var allRoleGroups = wwsClient.GetRoleGroups();

                    Constants.ApplicationData.RoleGroups = new Dictionary<int, RoleGroup>();

                    foreach (var roleGroup in allRoleGroups)
                    {
                        Constants.ApplicationData.RoleGroups.Add(roleGroup.Id, roleGroup);
                    }
                }
            }
            catch
            {

            }

            #endregion

            #region Load Roles
            try
            {
                using (WarehouseServiceClient wwsClient = new WarehouseServiceClient("HTTP"))
                {
                    var allRoles = wwsClient.GetRoles();

                    Constants.ApplicationData.Roles = new Dictionary<int, Role>();

                    foreach (var role in allRoles)
                    {
                        Constants.ApplicationData.Roles.Add(role.Id, role);
                    }
                }
            }
            catch
            {

            }

            #endregion

            #region Load Product Categories

            try
            {
                using (WarehouseServiceClient wwsClient = new WarehouseServiceClient("HTTP"))
                {
                    var allCats = wwsClient.GetAllProductCategories();

                    Constants.ApplicationData.ProductCategory = new Dictionary<int, ProductCategory>();

                    foreach (var product in allCats)
                    {
                        Constants.ApplicationData.ProductCategory.Add(product.Id.Value, product);
                    }
                }
            }
            catch
            {

            }

            

            #endregion

            #region Load Product

            try
            {

                WWS.WarehouseServiceClient productManager = new WWS.WarehouseServiceClient(ServiceParametor.Parametor);

                var allProduct = productManager.GetActiveProduct();

                Constants.ApplicationData.Products = new Dictionary<int, WWS.Product>();

                foreach (var item in allProduct)
                {
                    Constants.ApplicationData.Products.Add(item.Id.Value, item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            #endregion

            #region Load Munit
            try
            {
                WWS.WarehouseServiceClient munitManager = new WWS.WarehouseServiceClient(ServiceParametor.Parametor);

                var allMunit = munitManager.GetMunits();

                Constants.ApplicationData.Munits = new Dictionary<int, WWS.Munit>();

                foreach (var item in allMunit)
                {
                    Constants.ApplicationData.Munits.Add(item.Id, item);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            #endregion

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           UserLabel2.Text=dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

    }
}