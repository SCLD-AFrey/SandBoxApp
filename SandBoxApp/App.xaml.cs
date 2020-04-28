using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using SandBox.Data;


namespace SandBoxApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration AppConfiguration { get; set; }
        public App()
        {
            AppConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionHelper.Connect(AppConfiguration, autoCreateOption:AutoCreateOption.DatabaseAndSchema);
        }

    }

}
