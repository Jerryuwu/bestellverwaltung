using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
    public partial class ArticleView {
        public ArticleView() {
            InitializeComponent();
            ViewModel = new();
            this.WhenActivated(disposable => {

            });
        }
    }
}