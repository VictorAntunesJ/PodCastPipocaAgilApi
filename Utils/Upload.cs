using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api_SistemaCursosDistancia.Utils
{
    //Singleton ->Static
    public class Upload
    {
        // Upload.
        public static string UploadFile(
            IFormFile escolhaArquivo,
            string[] extencoesPermitidas,
            string diretorio
        )
        {
            try
            {
                // Determinamos onde sera salvo o arquivo
                var pasta = Path.Combine("StaticFiles", diretorio);
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                // Verificamos se existe um arquivo para ser salvo
                if (escolhaArquivo.Length > 0)
                {
                    // Pegamos o nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue
                        .Parse(escolhaArquivo.ContentDisposition)
                        .FileName.Trim('"');

                    // Validamos se a extencao é permitida
                    if (ValidarExensao(extencoesPermitidas, nomeArquivo))
                    {
                        var extencao = RetornarExtencao(nomeArquivo);
                        var novoNome = $"{Guid.NewGuid()}.{extencao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        //salvamos efetivamente o arquivo na aplicação

                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            escolhaArquivo.CopyTo(stream);
                        }
                        return novoNome;
                    }
                }
                return "";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Remover arquivo.
        //Validar Extenções de arquivo.
        public static bool ValidarExensao(string[] extensoesPermitidas, string nomeArquivo)
        {
            string extensao = RetornarExtencao(nomeArquivo);
            foreach (string ext in extensoesPermitidas)
            {
                if (ext == extensao)
                {
                    return true;
                }
            }
            return false;
        }

        // Retornar extenção.
        public static string RetornarExtencao(string nomeArquivo)
        {
            //artuivo.jpeg
            // [0]  [1]  [2]
            // arq.uivo.jpeg = 3
            // lenght(3) - 1 = 2
            string[] dados = nomeArquivo.Split('.');
            return dados[dados.Length - 1];
        }

        internal static string UploadFile(IFormFile escolhaArquivo, object extencoesPermitidas)
        {
            throw new NotImplementedException();
        }
    }
}
