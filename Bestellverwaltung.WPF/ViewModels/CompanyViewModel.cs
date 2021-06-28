using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class CompanyViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {

    public ObservableCollection<CompanyEntity> Companies { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NewCommand { get; set; }

    public CompanyEntity SelectedCompany { get; set; }

    public ICompanyRepository _CompanyRepository { get; set; }
    public CompanyViewModel() {
      Activator = new();
      Companies = new();
      _CompanyRepository = Locator.Current.GetService<ICompanyRepository>();
      HostScreen = Locator.Current.GetService<IScreen>();

      SaveCommand = ReactiveCommand.CreateFromTask(SaveCompany);
      NewCommand = ReactiveCommand.CreateFromTask(NewCompany);
      DeleteCommand = ReactiveCommand.CreateFromTask(DeleteCompany);
      
      Task.Run(LoadCompanies);
    }

    private async Task LoadCompanies() {
      await foreach (var companyEntity in _CompanyRepository.GetAllCompanies()) {
        Companies.Add(companyEntity);
      }
    }
    private async Task DeleteCompany() {
      if (SelectedCompany is null) return;
      await _CompanyRepository.DeleteCompany(SelectedCompany);
      Companies.Remove(SelectedCompany);
    }

    private async Task SaveCompany() {
      Console.WriteLine("eee");
      foreach (var article in Companies) {
        _ = _CompanyRepository.SaveOrUpdateCompany(article);
      }
    }

    private async Task NewCompany() {
      var newCompany = new CompanyEntity();
      Companies.Insert(0, newCompany);
      SelectedCompany = newCompany;
    }
    public string? UrlPathSegment => "Companies";
    public IScreen HostScreen { get; }
    public ViewModelActivator Activator { get; }
  }
}