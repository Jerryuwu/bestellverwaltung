using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;

namespace Bestellverwaltung.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            ViewModel = new();
            this.WhenActivated(disposable => {
                this.OneWayBind(ViewModel,
                        vm => vm.Router,
                        v => v.RoutedViewHost.Router)
                    .DisposeWith(disposable);
                this.WhenAnyValue(x => x.MenuToggleButton.IsChecked)
                         .Select(x => !x ?? false)
                         .BindTo(this, x => x.MenuToggleButton.Visibility, new BooleanToVisibilityTypeConverter())
                         .DisposeWith(disposable);
            });
        }
    }
}