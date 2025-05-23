using Proyecto_De_investigacion.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_De_investigacion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnInvestigar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrompt.Text))
            {
                MessageBox.Show("Ingresa un tema para investigar.");
                return;
            }

            btnInvestigar.Enabled = false;
            richResultado.Text = "Consultando inteligencia artificial...";

            try
            {
                var openai = new OpenAIService();
                string resultado = await openai.ConsultarAsync(txtPrompt.Text);
                richResultado.Text = resultado;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la IA: " + ex.Message);
                richResultado.Text = "";
            }
            finally
            {
                btnInvestigar.Enabled = true;
            }
        }


        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string prompt = txtPrompt.Text;
            string resultado = richResultado.Text;

            if (string.IsNullOrWhiteSpace(resultado))
            {
                MessageBox.Show("No hay contenido para guardar. Realiza una investigación primero.");
                return;
            }

            try
            {
                var db = new BaseDeDatosService();
                db.Guardar(prompt, resultado);

                // Guardar Word
                using (var saveWord = new SaveFileDialog())
                {
                    saveWord.Title = "Guardar documento Word";
                    saveWord.Filter = "Documento Word (*.docx)|*.docx";
                    saveWord.InitialDirectory = @"C:\Investigaciones";
                    saveWord.FileName = "Investigacion.docx";
                    if (saveWord.ShowDialog() == DialogResult.OK)
                    {
                        var word = new GeneradorWord();
                        word.Crear(resultado, saveWord.FileName);
                    }
                }

                // Guardar PowerPoint
                using (var savePpt = new SaveFileDialog())
                {
                    savePpt.Title = "Guardar presentación PowerPoint";
                    savePpt.Filter = "Presentación PowerPoint (*.pptx)|*.pptx";
                    savePpt.InitialDirectory = @"C:\Investigaciones";
                    savePpt.FileName = "Presentacion.pptx";
                    if (savePpt.ShowDialog() == DialogResult.OK)
                    {
                        var ppt = new GeneradorPowerPoint();
                        ppt.Crear(resultado, savePpt.FileName);
                    }
                }

                MessageBox.Show("Documentos generados y guardados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar o generar documentos: " + ex.Message);
            }
        }


    }
}