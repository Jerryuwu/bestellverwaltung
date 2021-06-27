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
                        v => v.ArticleGrid.ItemsSource).DisposeWith(disposable);
                this.BindCommand(ViewModel,
                    vm => vm.SaveCommand,
                    v => v.SaveButton).DisposeWith(disposable);
                this.BindCommand(ViewModel,
                    vm => vm.NewCommand,
                    v => v.NewButton).DisposeWith(disposable);
                this.BindCommand(ViewModel,
                    vm => vm.DeleteCommand,
                    v => v.DeleteButton).DisposeWith(disposable);

                PricePerArticle.Binding.StringFormat = "####.## €";
                ArticleInStock.Binding.StringFormat = "######## in stock";
                
            });
        }
    }
}