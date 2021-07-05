using System.Net.Mail;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF.ViewModels {
    public class OrderViewModel : ReactiveObject, IRoutableViewModel, IActivatableViewModel {
        public string? UrlPathSegment => "Orders";
        public IScreen HostScreen { get; }
        public ViewModelActivator Activator { get; }

        public OrderViewModel() {
            Activator = new();
            HostScreen = Locator.Current.GetService<IScreen>();
        }
    }
}