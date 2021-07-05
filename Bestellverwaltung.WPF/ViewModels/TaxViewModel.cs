using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Channels;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using DynamicData;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class TaxViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {

    public ObservableCollection<TaxEntity> Taxes { get; set; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }

    [Reactive] public int NextId { get; set; }
    [Reactive] public decimal Percentage { get; set; }
    public TaxEntity SelectedTax { get; set; }

    public ITaxRepository _TaxRepository { get; set; }
    public TaxViewModel() {
      Activator = new();
      Taxes = new();
      _TaxRepository = Locator.Current.GetService<ITaxRepository>();
      HostScreen = Locator.Current.GetService<IScreen>();

      SaveCommand = ReactiveCommand.CreateFromTask(SaveTaxes);
      DeleteCommand = ReactiveCommand.CreateFromTask(DeleteTax);
      
      Task.Run(LoadTaxes);
    }

    private async Task LoadTaxes() {
      Taxes.Clear();
      await foreach (var taxEntity in _TaxRepository.GetAllTaxes()) {
         Taxes.Add(taxEntity);
      }
      await LoadId();
    }

    private async Task LoadId() {
      var db = new Database();
      await db.Connection.OpenAsync();
      NextId = await db.GetNextId("taxes");
    }

    private async Task SaveTaxes() {
      var tax = new TaxEntity() {
        Percentage = Percentage,
      };
      Taxes.Add(tax);
      await _TaxRepository.SaveOrUpdateTax(tax);
    }

    private async Task DeleteTax() {
      await _TaxRepository.DeleteTax(SelectedTax);
      Taxes.Remove(SelectedTax);
    }
    
    public string? UrlPathSegment => "Taxes";
    public IScreen HostScreen { get; }
    public ViewModelActivator Activator { get; }
  }
}