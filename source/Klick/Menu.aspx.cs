using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.Configuration;
using System.Transactions;
using System.Data;
using Telerik.Web.UI;
using BusinessService;

public partial class Menu : BasePage
{
    #region Private Members

    private KlickEntities entities = new KlickEntities();

    private ProductCategoryManager productCategoryManager = new ProductCategoryManager();

    private int PreviousRandomNumber { get; set; }

    #endregion

    public override void Dispose()
    {
        entities.Dispose();
        base.Dispose();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PreviousRandomNumber = 0;
            GetProductCategories();
        }  
    }


    #region private methods

    private void GetProductCategories()
    {
        rptMenu1.DataSource = productCategoryManager.GetAllProductCategories(BranchId,true);
        rptMenu1.DataBind();
    }

    public String GetColorCss()
    {
        bool flag = true;
        Random rnd = new Random();
        while (flag)
        {
            int currentNumber = rnd.Next(1, 4);
            if(PreviousRandomNumber != currentNumber)
            {
                PreviousRandomNumber = currentNumber;
                flag = false;
            }
        }
        switch (PreviousRandomNumber)
        {
            case 1:
                return "green-color";
            case 2:
                return "yellow-color";
            case 3:
                return "red-color";
        }
        return "green-color";
    }
    #endregion
}