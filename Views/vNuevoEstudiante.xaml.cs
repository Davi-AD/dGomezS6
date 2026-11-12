using System.Net;

namespace dGomezS6.Views;

public partial class vNuevoEstudiante : ContentPage
{
	public vNuevoEstudiante()
	{
		InitializeComponent();
	}

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlert("Alerta", "Complete todos los campos", "OK");
                return;
            }

            WebClient cliente = new WebClient();

            var parametros = new System.Collections.Specialized.NameValueCollection();

            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            cliente.UploadValues("http://192.168.56.1/ws_uisrael/post.php", "POST", parametros);

            await DisplayAlertAsync("Éxito", "Estudiante agregado correctamente", "OK");

            await Navigation.PushAsync(new vEstudiante());
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}