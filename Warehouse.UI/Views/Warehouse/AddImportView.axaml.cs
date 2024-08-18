using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DocumentFormat.OpenXml.Vml;
using Warehouse.Application.Freights.ReceiveFreights;
using Warehouse.UI.ViewModels.Warehouse;
using TextBox = Avalonia.Controls.TextBox;

namespace Warehouse.UI.Views.Warehouse;

public partial class AddImportView : UserControl
{
    private readonly AddImportViewModel _dataContext;
    private readonly MainWindow _mainWindow;

    public AddImportView(MainWindow mainWindow)
    {
        InitializeComponent();
        _mainWindow = mainWindow;
        DataContext = new AddImportViewModel(_mainWindow);
        _dataContext = (AddImportViewModel)DataContext;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        await _dataContext.InitAsync();
    }

    private void DecimalTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textbox = sender as TextBox;
        var input = textbox!.Text;

        if (input.Length == 0)
        {
            textbox.SetValue(TextBox.TextProperty, input);
            return;
        }

        input = string.Join("", input.Where(c => char.IsDigit(c) || c == '.'));

        if (input.Length == 0)
        {
            textbox.SetValue(TextBox.TextProperty, input);
            return;
        }

        input = input[0] == '.' ? "" : input;

        if (input.Length == 0)
        {
            textbox.SetValue(TextBox.TextProperty, input);
            return;
        }

        input = input[^1] == '.' && input.Count(c => c == '.') > 1 ?
            input.Substring(0, input.Length - 1) :
            input;

        textbox.SetValue(TextBox.TextProperty, input);
    }

    private void NameSubmit(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var textbox = sender as TextBox;
        var input = textbox!.Text;

        _dataContext.FreightName = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void TypeSubmit(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var textbox = sender as TextBox;
        var input = textbox!.Text;

        _dataContext.FreightType = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void QuantitySubmit(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var textbox = sender as TextBox;
        var input = textbox!.Text;

        _dataContext.FreightQuantity = decimal.Parse(input);
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void UnitSubmit(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var textbox = sender as TextBox;
        var input = textbox!.Text;

        _dataContext.FreightUnit = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void OnFreightRemove(object? sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var freight = button?.DataContext as FreightCreateModel;

        _dataContext.FreightsInput.Remove(freight);
    }

    private void ButtonSubmitFreightName(object? sender, RoutedEventArgs e)
    {
        var textbox = this.GetControl<TextBox>("FreightName");
        var input = textbox.Text;

        _dataContext.FreightName = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void ButtonSubmitFreightType(object? sender, RoutedEventArgs e)
    {
        var textbox = this.GetControl<TextBox>("FreightType");
        var input = textbox.Text;

        _dataContext.FreightType = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void ButtonSubmitFreightQuantity(object? sender, RoutedEventArgs e)
    {
        var textbox = this.GetControl<TextBox>("FreightQuantity");
        var input = textbox.Text;

        _dataContext.FreightQuantity = decimal.Parse(input);
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }

    private void ButtonSubmitFreightUnit(object? sender, RoutedEventArgs e)
    {
        var textbox = this.GetControl<TextBox>("FreightUnit");
        var input = textbox.Text;

        _dataContext.FreightUnit = input;
        textbox.SetValue(TextBox.TextProperty, string.Empty);
    }
}