using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Projeto001_ConsultarCep.Servico.Modelo;
using Newtonsoft.Json;

namespace Projeto001_ConsultarCep.Servico
{
    public class ViaCepServico
    {
        private static String EnderecoUrl = "http://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCep(String cep)
        {
            String NovoEndereçoUrl = String.Format(EnderecoUrl, cep);
            //Busca na internet
            WebClient wc = new WebClient();

            String conteudo = wc.DownloadString(NovoEndereçoUrl);

            //Deserializar conteudo, ou seja converter string em objeto
            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo); //converti o conteudo em Endereco
            if (end.cep == null) return null;
            
            return end;


        }
    }
}
