using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Projeto001_ConsultarCep.Servico.Modelo;
using Projeto001_ConsultarCep.Servico;

namespace Projeto001_ConsultarCep
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //PRIMEIRO CONFIGURAR O BOTAO POIS TODA A AÇÃO INICIA APÓS CLICAR NELE.
            //USEI O EVENTO DE CLICK, concatenei e atribui um metodo que vai executar uma ação a partir do click
            BOTAO.Clicked += BuscarCep;
            
        }
        //o metodo para atender os padroes de um event render, é necessario ter dois parametros um objeto e um event args.
        private void BuscarCep(object sender, EventArgs args)
        {

            //validações
            string cep = CEP.Text.Trim();//método trim para remover todos os espaços caaso o usuario tenha digitado com espaço
            if (IsValidCep(cep))
            {
                //Tratamento de exceções
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCep(cep); //atribui a uma variavel endereco para salvar o retorno do metodo que seria o endereço
                                                                            //mostrar o resultado na tela, pra isso foi criada a variavel RESULTADO
                                                                            //tratar o endereço nulo
                    if (end!= null)
                    {
                    RESULTADO.Text = ($"Endereço: {end.logradouro}, {end.bairro}, {end.localidade}, {end.uf}");
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O ENDEREÇO NÃO FOI ENCONTRADO PARA O CEP INFORMADO: " + cep, "OK");
                    }

                    

                }
                catch (Exception e)
                {
                    DisplayAlert("Erro Crítico", e.Message, "OK");
                }

            }
        }
        //Método para verificar os caracteres q o usuario digitou
        private bool IsValidCep(String cep)
        {
            //variavel boolean para dizer se foi true
            Boolean valido = true;

            //Verificar se possui oito caracteres
            if (cep.Length != 8)
            {
                //erro
                DisplayAlert("ERRO", "CEP INVÁLIDO, O CEP DEVE CONTER 8 CARACTERES.", "OK");
                valido = false;
            }
            Int32 NovoCep = 0;
            //Se não ocorrer a conversão apresar o erro
            if (!Int32.TryParse(cep, out NovoCep))//tryparse e passado o valor digitado e o resultado caso o valor seja convertido
            {
                //erro
                DisplayAlert("ERRO", "CEP INVÁLIDO, O CEP DEVE SER COMPOSTO APENAS POR NÚMEROS.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
