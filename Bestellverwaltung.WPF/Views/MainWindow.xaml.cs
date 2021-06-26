﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bestellverwaltung.WPF.ViewModels;
using ReactiveUI;
using Splat;

namespace Bestellverwaltung.WPF {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : IEnableLogger {
    public MainWindow() {
      InitializeComponent();
      ViewModel = new();
      this.WhenActivated(disposable => {
        this.OneWayBind(ViewModel,
            vm => vm.Router,
            v => v.RoutedViewHost.Router)
         .DisposeWith(disposable);
        this.WhenAnyValue(x => x.MenuToggleButton.IsChecked)
         .Select(x => !x ?? false)
         .BindTo(this, x => x.MenuToggleButton.Visibility, new BooleanToVisibilityTypeConverter())
         .DisposeWith(disposable);
        Articles.Events().Selected
         .Do(_ => ViewModel.Router.Navigate.Execute(new ArticleViewModel()))
         .Log(this)
         .Subscribe()
         .DisposeWith(disposable);
        this.OneWayBind(ViewModel,
            vm => vm.Headline,
            v => v.Headline.Text)
         .DisposeWith(disposable);
      });
    }
  }
}