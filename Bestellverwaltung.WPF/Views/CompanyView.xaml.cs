using System.Reactive.Disposables;
using System.Windows.Controls;
using System.Windows.Forms;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class CompanyView  {
    public CompanyView() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {
        this.OneWayBind(ViewModel,
            vm => vm.Companies,
            v => v.CompanyGrid.ItemsSource)
          .DisposeWith(disposable);

        this.BindCommand(ViewModel,
          vm => vm.SaveCommand,
          v => v.SaveButton).DisposeWith(disposable);
        this.BindCommand(ViewModel,
          vm => vm.NewCommand,
          v => v.NewButton).DisposeWith(disposable);
        this.BindCommand(ViewModel,
          vm => vm.DeleteCommand,
          v => v.DeleteButton).DisposeWith(disposable);
        
        this.WhenAnyValue(x => x.CompanyGrid.SelectedItem)
          .BindTo(this, x => x.ViewModel.SelectedCompany)
          .DisposeWith(disposable);
      });
    }
  }
}