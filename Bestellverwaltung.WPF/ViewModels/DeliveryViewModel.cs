using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bestellverwaltung.WPF.Entities;
using IFAS2Personal.WPF.Database;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class DeliveryViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel{

    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }

    [Reactive] public decimal Amount { get; set; }
    [Reactive] public decimal Bonus { get; set; }
    public DeliveryCostEntity SelectedDelivery { get; set; }
    
    public ObservableCollection<DeliveryCostEntity> Deliveries { get; set; }

    private IDeliveryRepository DeliveryRepository { get; set; }

    public ViewModelActivator Activator { get; }
    public string? UrlPathSegment => "Deliveries";
    public IScreen HostScreen { get; }
    public DeliveryViewModel() {
      Activator = new();
      HostScreen = Locator.Current.GetService<IScreen>();
      DeliveryRepository = Locator.Current.GetService<IDeliveryRepository>();
      
      SaveCommand = ReactiveCommand.CreateFromTask(SaveDelivery);
      DeleteCommand = ReactiveCommand.CreateFromTask(DeleteDelivery);
      Observable.StartAsync(LoadDeliveries);
      
    }

    private async Task LoadDeliveries() {
      Console.WriteLine("eee");
      Deliveries = new();
      await foreach (var deliveryCostEntity in DeliveryRepository.GetAllDeliveryCosts()) {
        Console.WriteLine("eee2");
        Deliveries.Add(deliveryCostEntity);

      }
    }

    private async Task SaveDelivery() {
      var newDelivery = new DeliveryCostEntity() {
        Amount = Amount,
        Bonus = Bonus
      };
      Deliveries.Add(newDelivery);
      await DeliveryRepository.SaveOrUpdateDeliveryCost(newDelivery);
    }

    private async Task DeleteDelivery() {
      await DeliveryRepository.DeleteDeliveryCost(SelectedDelivery);
    }
  }
}