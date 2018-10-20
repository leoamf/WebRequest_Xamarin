using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebRequest
{
    public partial class MainPage : ContentPage
    {

        System.Net.WebRequest request;
        public MainPage()
        {
            InitializeComponent();
            status.Text = "carregando.";
            Uri uri = new Uri("https://developer.xamarin.com/demo/stock.json");
            request = System.Net.WebRequest.Create(uri);
            request.BeginGetResponse(HandleAsyncCallback,null);
        } 

        void HandleAsyncCallback(IAsyncResult ar)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Stream stream = request.EndGetResponse(ar).GetResponseStream();
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(ListagemFotos));
                ListagemFotos listagem = (ListagemFotos)serializer.ReadObject(stream);


                listImagens.ItemsSource = listagem.Fotos;
                status.IsVisible = false;

            });
        }

    }
}
