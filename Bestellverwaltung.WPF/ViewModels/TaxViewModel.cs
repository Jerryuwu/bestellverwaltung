using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class TaxViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {

    public TaxViewModel() {
      Activator = new();
      HostScreen = Locator.Current.GetService<IScreen>();
    }
    public string? UrlPathSegment => "Fees";
    public IScreen HostScreen { get; }
    public ViewModelActivator Activator { get; }
  }
}