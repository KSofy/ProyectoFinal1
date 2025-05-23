using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

namespace Proyecto_De_investigacion.Servicios
{
    public class GeneradorWord
    {
        public void Crear(string contenido, string rutaCompleta)
        {
            var app = new Word.Application();
            var doc = app.Documents.Add();
            doc.Content.Text = contenido;
            doc.SaveAs2(rutaCompleta);
            doc.Close();
            app.Quit();
        }
    }
}
