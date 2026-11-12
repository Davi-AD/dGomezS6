
using dGomezS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace dGomezS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string Url = "http://192.168.56.1/ws_uisrael/post.php";

    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiante> _estudiante;

    public vEstudiante()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await mostrar();
    }

    public async Task mostrar()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);

            List<Estudiante> mostrarEst =
                JsonConvert.DeserializeObject<List<Estudiante>>(content);

            _estudiante = new ObservableCollection<Estudiante>(mostrarEst);

            listaEstudiantes.ItemsSource = _estudiante;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void listaEstudiantes_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Estudiante estudiante)
        {
            listaEstudiantes.SelectedItem = null;

            await Navigation.PushAsync(
                new vActualizarEliminar(estudiante));
        }
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vNuevoEstudiante());
    }
}
