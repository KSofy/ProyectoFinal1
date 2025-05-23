using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Proyecto_De_investigacion.Servicios
{
    public class GeneradorPowerPoint
    {
        public void Crear(string contenido, string rutaCompleta)
        {
            var app = new PowerPoint.Application();
            var pres = app.Presentations.Add();

            string[] parrafos = contenido.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            int i = 1;
            foreach (var parrafo in parrafos)
            {
                var slide = pres.Slides.Add(i, PowerPoint.PpSlideLayout.ppLayoutText);
                slide.Shapes[1].TextFrame.TextRange.Text = $" {i}";
                slide.Shapes[2].TextFrame.TextRange.Text = parrafo.Trim();
                i++;
            }

            pres.SaveAs(rutaCompleta);
            pres.Close();
            app.Quit();
        }

    }
}
