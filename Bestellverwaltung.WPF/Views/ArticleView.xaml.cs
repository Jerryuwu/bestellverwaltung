using System.Reactive.Disposables;
using System.Windows.Controls;
using ReactiveUI;

namespace Bestellverwaltung.WPF.Views {
    public partial class ArticleView {
        public ArticleView() {
            InitializeComponent();
            ViewModel = new();
            this.WhenActivated(disposable => {
                this.OneWayBind(ViewModel,
                        vm => vm.Articles,
                        v => v.ArticleGrid.ItemsSource)
                   .DisposeWith(disposable);
            });
        }
    }
}