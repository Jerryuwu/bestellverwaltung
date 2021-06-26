

using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class MainViewModel : ReactiveObject, IActivatableViewModel, IScreen {
        public ViewModelActivator Activator { get; }
        [ObservableAsProperty] public string Headline { get; set; }
        public MainViewModel() {
            Activator = new();
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            this.WhenActivated(disposable => {
                this.WhenAnyValue(x => x.Activator)
                    .Subscribe()
                    .DisposeWith(disposable);
                Router.CurrentViewModel
                   .WhereNotNull()
                   .Select(x => x.UrlPathSegment)
                   .WhereNotNull()
                   .ToPropertyEx(this, x => x.Headline).DisposeWith(disposable);
                //Router.NavigateAndReset.Execute(new ArticleViewModel());
            });
        }
        public RoutingState Router { get; }
    }
}