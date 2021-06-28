using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class TaxViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {

    public ObservableCollection<TaxEntity> Taxes { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NewCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }

    public ITaxRepository _TaxRepository { get; set; }
    public TaxViewModel() {
      Activator = new();
      Taxes = new();
      _TaxRepository = Locator.Current.GetService<TaxRepository>();
      HostScreen = Locator.Current.GetService<IScreen>();

      Task.Run(LoadTaxes);
    }

    private async Task LoadTaxes() {
      Console.WriteLine("EEE");
      await foreach (var taxEntity in _TaxRepository.GetAllTaxes()) {
        Taxes.Add(taxEntity);
      }
    }
    
    public string? UrlPathSegment => "Taxes";
    public IScreen HostScreen { get; }
    public ViewModelActivator Activator { get; }
  }
}