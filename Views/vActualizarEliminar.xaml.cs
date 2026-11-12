
using dGomezS6.Models;

namespace dGomezS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.56.1/ws_uisrael/post.php";

    private readonly HttpClient cliente = new HttpClient();

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    // ACTUALIZAR ESTUDIANTE
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))
            {
                await DisplayAlert("Alerta", "Complete todos los campos", "OK");
                return;
            }

            var parametros = new Dictionary<string, string>
            {
                { "codigo", txtCodigo.Text },
                { "nombre", txtNombre.Text },
                { "apellido", txtApellido.Text },
                { "edad", txtEdad.Text }
            };

            var query = await new FormUrlEncodedContent(parametros)
                .ReadAsStringAsync();

            var respuesta = await cliente.PutAsync(
                Url + "?" + query,
                null);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert(
                    "Éxito",
                    "Estudiante actualizado correctamente",
                    "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Error",
                    "No se pudo actualizar el estudiante",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    // ELIMINAR ESTUDIANTE
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlert(
                "Confirmar eliminación",
                "¿Está seguro de eliminar este estudiante?",
                "Sí",
                "No");

            if (!confirmar)
            {
                return;
            }

            var respuesta = await cliente.DeleteAsync(
                Url + "?codigo=" + txtCodigo.Text);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert(
                    "Éxito",
                    "Estudiante eliminado correctamente",
                    "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Error",
                    "No se pudo eliminar el estudiante",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
