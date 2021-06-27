using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class DeliveryView {
    public DeliveryView() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {

      });
    }
  }
}