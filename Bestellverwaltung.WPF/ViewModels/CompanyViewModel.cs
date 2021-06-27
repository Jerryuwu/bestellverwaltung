using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
  public class CompanyViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {

    public CompanyViewModel() {
      Activator = new();
      HostScreen = Locator.Current.GetService<IScreen>();
    }
    public string? UrlPathSegment => "Companies";
    public IScreen HostScreen { get; }
    public ViewModelActivator Activator { get; }
  }
}