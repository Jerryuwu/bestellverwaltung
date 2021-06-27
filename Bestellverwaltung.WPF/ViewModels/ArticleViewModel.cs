using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Bestellverwaltung.WPF.Entities;
using DynamicData;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class ArticleViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel{

        [Reactive] public ObservableCollection<ArticleEntity> Articles { get; set; }
        private IArticleRepository _ArticleRepository { get; }
        public ArticleViewModel() {
            Activator = new();
            Articles = new();
            HostScreen = Locator.Current.GetService<IScreen>();
            _ArticleRepository = Locator.Current.GetService<IArticleRepository>();
            var t1 = new ArticleEntity() {
                Id = 1,
                Name = "Some nice stuff",
                Price = (decimal) 3.4,
                Stock = 4
            };
            var t2 = new ArticleEntity() {
                Id = 2,
                Name = "Some other nice stuff",
                Price = (decimal) 3.4,
                Stock = 4
            };
            Articles.Add(t1);
            Articles.Add(t2);
        }
        public ViewModelActivator Activator { get; }
        public string? UrlPathSegment => "Articles";
        public IScreen HostScreen { get; }
    }
}