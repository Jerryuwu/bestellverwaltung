using System.Windows.Controls;
using System.Windows.Forms;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
  public partial class CompanyView  {
    public CompanyView() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {

      });
    }
  }
}