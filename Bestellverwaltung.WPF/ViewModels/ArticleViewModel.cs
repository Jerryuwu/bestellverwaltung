using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using DynamicData;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class ArticleViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel{

        [Reactive] public ObservableCollection<ArticleEntity> Articles { get; set; }
        [Reactive] public ArticleEntity SelectedArticle { get; set; }
        public ReactiveCommand<Unit, Unit> NewCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
        private IArticleRepository _ArticleRepository { get; }
        public ArticleViewModel() {
            Activator = new();
            Articles = new();
            HostScreen = Locator.Current.GetService<IScreen>();
            _ArticleRepository = Locator.Current.GetService<IArticleRepository>();

            NewCommand = ReactiveCommand.CreateFromTask(NewArticle);
            SaveCommand = ReactiveCommand.CreateFromTask(SaveArticles);
            DeleteCommand = ReactiveCommand.CreateFromTask(DeleteArticle);

            Task.Run(LoadArticles);
        }

        private async Task LoadArticles() {
            await foreach (var article in _ArticleRepository.GetAllArticles()) {
                Articles.Add(article);
            }
        }

        private async Task DeleteArticle() {
            if (SelectedArticle is null) return;
            await _ArticleRepository.DeleteArticle(SelectedArticle);
            Articles.Remove(SelectedArticle);
        }

        private async Task SaveArticles() {
            foreach (var article in Articles) {
                _ = _ArticleRepository.SaveOrUpdateArticle(article);
            }
        }

        private async Task NewArticle() {
            var newArticle = new ArticleEntity();
            Articles.Insert(0, newArticle);
            SelectedArticle = newArticle;
        }
        public ViewModelActivator Activator { get; }
        public string? UrlPathSegment => "Articles";
        public IScreen HostScreen { get; }
    }
}