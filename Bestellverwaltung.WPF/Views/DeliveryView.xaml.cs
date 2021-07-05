using System;
using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class DeliveryView {
    public DeliveryView() {
      InitializeComponent();
      this.WhenActivated(disposable => {
        this.OneWayBind(ViewModel,
          vm => vm.Deliveries,
          v => v.DeliveryGrid.ItemsSource).DisposeWith(disposable);

        this.Bind(ViewModel,
          vm => vm.Amount,
          v => v.NewAmount.Text).DisposeWith(disposable);
        this.Bind(ViewModel,
          vm => vm.Bonus,
          v => v.NewBonus.Text).DisposeWith(disposable);

        this.BindCommand(ViewModel,
          vm => vm.SaveCommand,
          v => v.SaveButton).DisposeWith(disposable);
        this.BindCommand(ViewModel,
          vm => vm.DeleteCommand,
          v => v.DeleteButton).DisposeWith(disposable);

        ViewModel!.SaveCommand.Subscribe(_ => {
          DeliveryGrid.Items.Refresh();
        });
      });
    }
  }
}