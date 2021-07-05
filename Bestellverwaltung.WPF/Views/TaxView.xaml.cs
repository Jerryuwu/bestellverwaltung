using System;
using System.Reactive.Disposables;
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
        this.Bind(ViewModel,
          vm => vm.NextId,
          v => v.NextIdText.Text).DisposeWith(disposable);    
        this.Bind(ViewModel,
          vm => vm.Percentage,
          v => v.PercentageText.Text).DisposeWith(disposable);
        this.BindCommand(ViewModel,
            vm => vm.SaveCommand,
            v => v.SaveButton).DisposeWith(disposable);
        this.BindCommand(ViewModel,
          vm => vm.DeleteCommand,
          v => v.DeleteButton);
        this.WhenAnyValue(x => x.TaxGrid.SelectedItem)
          .BindTo(ViewModel, x => x.SelectedTax)
          .DisposeWith(disposable);

        ViewModel.SaveCommand.Subscribe(_ => {
          TaxGrid.Items.Refresh();
        });
      });
    }
  }
}