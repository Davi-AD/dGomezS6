using dGomezS6.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace dGomezS6.Views;

public partial class vEstudiante : ContentPage
{
	//Se debe tomar la ip de la maquina desde el cmd con el comando ipconfig y tomando la dirrecion ipv4
	private const string Url = "http://192.168.56.1/ws_uisrael/post.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudiante;

	public vEstudiante()
	{
		InitializeComponent();
		mostrar();
	}
	public async void mostrar() 
	{
		var content = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estudiante = new ObservableCollection<Estudiante>(mostrarEst);
		listaEstudiantes.ItemsSource = _estudiante;

	}
}