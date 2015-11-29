using RenanBarbosa.Fipe.NET.Common;
using RenanBarbosa.Fipe.NET.Tipos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

/// <summary>
/// Projeto para obter todos os dados relacionados a veículos
/// fornecidos de forma aberta na própria 
/// página da FIPE (http://www.fipe.org.br/pt-br/indices/veiculos/)
/// </summary>
/// <autor>Renan Barbosa</autor>
/// <email>rnn2@live.com</email>
/// <data>2015-11-28</data>
namespace RenanBarbosa.Fipe.NET
{
    /// <summary>
    /// Projeto para obter todos os dados relacionados a veículos
    /// fornecidos de forma aberta na própria 
    /// página da FIPE (http://www.fipe.org.br/pt-br/indices/veiculos/)
    /// </summary>
    /// <autor>Renan Barbosa</autor>
    /// <email>rnn2@live.com</email>
    /// <data>2015-11-28</data>
    class Program
    {
        // Carros e utilitários
        static int _codigoTipoVeiculo = 1;

        // Novembro de 2015
        static int _codigoTabelaReferencia = 185;

        static void Main(string[] args)
        {
            // Obter todas as Marcas
            List<Marca> marcas = ObterMarcas();

            foreach (Marca item in marcas)
            {
                // Obter veiculos da Marca
                List<Modelo> modelos = ObterModelosDaMarca(item.Value);

                // TODO: Inserir a Marca e a lista de veículos na base de dados.
            }
           
            Console.WriteLine("Pressione qualquer tecla para encerrar.");
            Console.ReadKey();
        }

        static List<Marca> ObterMarcas()
        {
            string urlServidor = "http://www.fipe.org.br/IndicesConsulta-ConsultarMarcas/";
            string postData = string.Format("codigoTabelaReferencia={0}&codigoTipoVeiculo={1}", _codigoTabelaReferencia, _codigoTipoVeiculo);
            return Utilildades.DeserializarMarcas(ExecutarRequisicaoWeb(urlServidor, postData));
        }

        static List<Modelo> ObterModelosDaMarca(int codigoMarca)
        {
            string urlServidor = "http://www.fipe.org.br/IndicesConsulta-ConsultarModelos/";
            string postData = string.Format("codigoTipoVeiculo={0}&codigoTabelaReferencia={1}&codigoModelo=&codigoMarca={2}&ano=&codigoTipoCombustivel=&anoModelo=&modeloCodigoExterno=", _codigoTipoVeiculo, _codigoTabelaReferencia, codigoMarca);
            return Utilildades.DesrializarModelos(ExecutarRequisicaoWeb(urlServidor, postData));
        }

        /// <summary>
        /// Executa uma requisição no site da Tabela Fipe com base 
        /// na url do servidor informada e os dados do post.
        /// </summary>
        /// <param name="urlServidor"></param>
        /// <param name="postData"></param>
        /// <returns>Resposta com dados solicitados</returns>
        static string ExecutarRequisicaoWeb(string urlServidor, string postData)
        {
            string ret = string.Empty;

            // Criar cabeçalho para realizar requisição
            HttpWebRequest http = WebRequest.Create(urlServidor) as HttpWebRequest;
            http.Method = "POST";
            http.Host = "www.fipe.org.br";
            http.ContentLength = postData.Length;
            http.Accept = "*/*";
            http.Headers.Add("Origin", "http://www.fipe.org.br");
            http.Headers.Add("X-Requested-With", "XMLHttpRequest");
            http.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36";
            http.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            http.Referer = "http://www.fipe.org.br/pt-br/indices/veiculos";
            http.Headers.Add("Accept-Encoding", "gzip, deflate");
            http.Headers.Add("Accept-Language", "pt-BR,pt;q=0.8,en-US;q=0.6,en;q=0.4");

            // Incluir os dados de post da requisição
            StreamWriter requestWriter = new StreamWriter(http.GetRequestStream());
            requestWriter.Write(postData);
            requestWriter.Close();

            // Realizar a leitura da resposta
            StreamReader responseReader = new StreamReader(http.GetResponse().GetResponseStream());
            ret  = responseReader.ReadToEnd();
            responseReader.Close();
            http.GetResponse().Close();

            return ret;
        }
    }
}
