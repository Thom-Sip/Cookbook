using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.XAML
{
    public class ProductListPage : Sitecore.Web.UI.Pages.DialogForm
    {
        protected GridPanel Viewer;
        protected Button btnDelete;
        protected Listview ProductList;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!Context.ClientPage.IsEvent)
            {
                LoadProducts();
                this.OK.Visible = false;
                this.Cancel.Value = "Close";
            }
        }

        private void LoadProducts()
        {
            string ProductsPath ="/sitecore/Content/Home/Products/Balls";
            Item products =
            Factory.GetDatabase("master").GetItem(ProductsPath);
            foreach (Item product in products.Children)
            {
                ListviewItem productItem = new ListviewItem();
                Context.ClientPage.AddControl(ProductList, productItem);
                productItem.ID = Control.GetUniqueID("I");
                productItem.Header = product["Title"];
                productItem.ColumnValues["Id"] = product.ID.ToString();
                productItem.ColumnValues["Title"] = product["Title"];
                // productItem.ColumnValues["Price"] = product["Price"];
            }
        }

        protected void DeleteProducts()
        {
            if(ProductList.SelectedItems.Length > 0)
            {
                foreach(var productItem in ProductList.SelectedItems)
                {
                    Item product = Factory.GetDatabase("master").GetItem(new ID(productItem.ColumnValues["Id"].ToString()));
                    product.Recycle();
                }

                ProductList.Controls.Clear();
                LoadProducts();
                ProductList.Refresh();

                SheerResponse.Alert("Selected products are deleted");
            }
            else
            {
                SheerResponse.Alert("No Products Selected");
            }
        }

        public override void HandleMessage(Message message)
        {
           if(message.Name == "product:delete")
           {
               DeleteProducts();
           }
        }
    }
}