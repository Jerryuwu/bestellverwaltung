

using System;
using System.Reactive.Disposables;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class MainViewModel : ReactiveObject, IActivatableViewModel, IScreen {
        public ViewModelActivator Activator { get; }

        public MainViewModel() {
            Activator = new();
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            this.WhenActivated(disposable => {
                this.WhenAnyValue(x => x.Activator)
                    .Subscribe()
                    .DisposeWith(disposable);
                Router.NavigateAndReset.Execute(new ArticleViewModel());
            });
        }

        public RoutingState Router { get; }
    }
}