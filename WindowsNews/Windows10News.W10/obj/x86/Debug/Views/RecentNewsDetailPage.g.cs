﻿#pragma checksum "G:\Kits\Kit DotNET\Windows 10\Windows 10 Sample Apps\Win10NewsApp\Windows10News.W10\Views\RecentNewsDetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6E2C114D9BA6D0D5995C073519AD073B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Windows10News.Views
{
    partial class RecentNewsDetailPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.root = (global::Windows.UI.Xaml.Controls.Page)(target);
                }
                break;
            case 2:
                {
                    this.topBarBackground = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3:
                {
                    this.appBar = (global::AppStudio.Uwp.Actions.ActionsCommandBar)(target);
                }
                break;
            case 4:
                {
                    this.mainPanel = (global::Windows10News.Layouts.Detail.TextDetailLayout)(target);
                }
                break;
            case 5:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element5 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 46 "..\..\..\Views\RecentNewsDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element5).Click += this.AppBarButton_Click;
                    #line default
                }
                break;
            case 6:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element6 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 51 "..\..\..\Views\RecentNewsDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element6).Click += this.AppBarButton_Click;
                    #line default
                }
                break;
            case 7:
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element7 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 56 "..\..\..\Views\RecentNewsDetailPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element7).Click += this.AppBarButton_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.appTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

