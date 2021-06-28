using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class FeeView {
    public FeeView() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {
        this.OneWayBind(ViewModel,
          vm => vm.Taxes,
          v => v.TaxGrid.ItemsSource).DisposeWith(disposable);
      });
    }
  }
}