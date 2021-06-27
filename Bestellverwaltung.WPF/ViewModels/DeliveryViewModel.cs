using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class DeliveryViewModel : ReactiveObject, IActivatableViewModel, IRoutableViewModel{

    public DeliveryViewModel() {
      Activator = new();
      HostScreen = Locator.Current.GetService<IScreen>();
    }
    public ViewModelActivator Activator { get; }
    public string? UrlPathSegment => "Deliveries";
    public IScreen HostScreen { get; }
  }
}