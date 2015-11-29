using RenanBarbosa.Fipe.NET.Tipos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Projeto para obter todos os dados relacionados a veículos
/// fornecidos de forma aberta na própria 
/// página da FIPE (http://www.fipe.org.br/pt-br/indices/veiculos/)
/// </summary>
/// <autor>Renan Barbosa</autor>
/// <email>rnn2@live.com</email>
/// <data>2015-11-28</data>
namespace RenanBarbosa.Fipe.NET.Common
{
    /// <summary>
    /// Classe com métodos mais utilizados 
    /// </summary>
    public static class Utilildades
    {
        /// <summary>
        /// Recebe um objeto Json do tipo string a ser deserializado
        /// </summary>
        /// <param name="json">Resultado da requisição</param>
        /// <returns>Coleção de marcas de veículos</returns>
        public static List<Marca> DeserializarMarcas(string json)
        {
            return JsonConvert.DeserializeObject<List<Marca>>(json);
        }

        /// <summary>
        /// Recebe um objeto Json do tipo string a ser deserializado
        /// </summary>
        /// <param name="json">Resultado da requisição</param>
        /// <returns>Coleção de veículos</returns>
        public static List<Modelo> DesrializarModelos(string json)
        {
            List<Modelo> ret = new List<Modelo>();

            // Transformar json recebido em JObject
            JObject objeto = JObject.Parse(json);

            // Obter os modelos e inserir em uma lista
            IList<JToken> colecaoResultados = objeto["Modelos"].ToList();

            foreach (JToken resultado in colecaoResultados)
            {
                // Obter objeto do tipo modelo
                Modelo modelo = JsonConvert.DeserializeObject<Modelo>(resultado.ToString());

                // Verificar se é nulo e adicionar a lista de retorno.
                if (modelo != null)
                    ret.Add(modelo);
            }

            return ret;
        }
    }
}
