using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            AttachConsole(-1);
            ConfigureDependencies();

        }

        private void ConfigureDependencies() {
            Locator.CurrentMutable.InitializeReactiveUI();
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            
            Locator.CurrentMutable.Register(() => new ArticleRepository(), typeof(IArticleRepository));
            Locator.CurrentMutable.Register(() => new CompanyRepository(), typeof(ICompanyRepository));
            Locator.CurrentMutable.Register(() => new TaxRepository(), typeof(ITaxRepository));
            Locator.CurrentMutable.Register(() => new DeliveryRepository(), typeof(IDeliveryRepository));
            
      
            Locator.CurrentMutable.RegisterConstant(new ConsoleLogger(), typeof(ILogger));
        }

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
    }
}