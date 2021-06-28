using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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
                this.WhenAnyValue(x => x.ArticleGrid.SelectedItem)
                    .BindTo(this, x => x.ViewModel.SelectedArticle)
                    .DisposeWith(disposable);
            });
        }
    }
}