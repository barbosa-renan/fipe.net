
/// <summary>
/// Projeto para obter todos os dados relacionados a veículos
/// fornecidos de forma aberta na própria 
/// página da FIPE (http://www.fipe.org.br/pt-br/indices/veiculos/)
/// </summary>
/// <autor>Renan Barbosa</autor>
/// <email>rnn2@live.com</email>
/// <data>2015-11-28</data>
namespace RenanBarbosa.Fipe.NET.Tipos
{
    /// <summary>
    /// Classe Tipo Modelo para armazenar uma marca de veículo
    /// </summary>
    public class Marca
    {
        /// <summary>
        /// Propriedades
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
