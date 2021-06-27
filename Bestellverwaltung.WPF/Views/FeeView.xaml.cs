using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class FeeView {
    public FeeView() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {

      });
    }
  }
}