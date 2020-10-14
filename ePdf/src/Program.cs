using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string caminho_gravar = @"C:\inetpub\wwwroot\github\UnificarPDF\ePdf\src\Arquivos\";
            List<string> arquivos = new List<string>();            
            arquivos.Add(@"C:\teste.pdf");            

            if (arquivos.Count > 0)
            {
                List<PdfReader> readerList = new List<PdfReader>();

                foreach (string x in arquivos)
                {
                    if (File.Exists(x))
                    {
                        PdfReader pdfReader = new PdfReader(File.ReadAllBytes(x));                        
                        readerList.Add(pdfReader);                                                                      
                    } else
                    {
                        Console.WriteLine("ARQUIVO: " + x + " NÃO ENCONTRADO.");
                    }                    
                }

                if (readerList.Count > 0)
                {
                    Document document = new Document(PageSize.A4, 0, 0, 0, 0);
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(caminho_gravar, "Arquivo.pdf"), FileMode.Create));
                    document.Open();

                    foreach (PdfReader reader in readerList)
                    {
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            document.Add(iTextSharp.text.Image.GetInstance(page));
                        }
                    }
                    document.Close();
                }
                else
                {
                    Console.WriteLine("NENHUM ARQUIVO ENCONTRADO.");
                }
            }

            Console.WriteLine("FIM DA ROTINA");
            Console.WriteLine("PRESSIONE UMA TECLA PARA SAIR.");
            Console.ReadKey();
        }
    }
}
