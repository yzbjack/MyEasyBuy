﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyEasyBuy.crm
{
    public partial class LoginBak : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.txtUserName.Text = Request.Cookies["UserName"] == null ? "" : Request.Cookies["UserName"].Value;
               this.txtPwd.Attributes.Add("value",Request.Cookies["Pwd"] == null ? "" : Request.Cookies["Pwd"].Value);
                
            }
        }

        //前台登陆
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Model.eb_customer model = new BLL.eb_customer().CheckLogin(this.txtUserName.Text.Trim(), this.txtPwd.Text.Trim());
            if (model!=null)
            {
                //登陆成功
                
                Session["CrmUserInfo"] = model;
                if (this.saveme.Checked)
                {
                    //记住我
                    HttpCookie cookieUserName = new HttpCookie("UserName", this.txtUserName.Text);
                    HttpCookie cookiePwd = new HttpCookie("Pwd", this.txtPwd.Text);
                    cookieUserName.Expires = DateTime.Now.AddDays(7);
                    cookiePwd.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookieUserName);
                    Response.Cookies.Add(cookiePwd);
                }
                Response.Redirect("index.aspx");
            }
            else
            {
                //登陆失败
                Maticsoft.Common.MessageBox.Show(this.Page, "用户名或密码错误，请重试");
            }
        }
    }
}