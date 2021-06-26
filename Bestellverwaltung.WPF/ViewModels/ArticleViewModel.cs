using System.Runtime.InteropServices;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class ArticleViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel{

        private IArticleRepository _ArticleRepository { get; }
        public ArticleViewModel() {
            Activator = new();
            HostScreen = Locator.Current.GetService<IScreen>();
            _ArticleRepository = Locator.Current.GetService<IArticleRepository>();
        }
        public ViewModelActivator Activator { get; }
        public string? UrlPathSegment => "Articles";
        public IScreen HostScreen { get; }
    }
}