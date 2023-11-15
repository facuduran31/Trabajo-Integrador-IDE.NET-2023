using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace WindowsForm
{
    public partial class Informe : Form
    {
        DataTable dtInforme = new DataTable();
        DataColumn[] dcInforme = new DataColumn[]
        {
            new DataColumn("Fecha", typeof(string)),
            new DataColumn("Recaudado", typeof(string))
        };

        public Informe()
        {
            dtInforme.Columns.AddRange(dcInforme);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
        }

        private async void btnEmitir_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value.CompareTo(dtpHasta.Value) < 0)
            {
                dtInforme.Rows.Clear();

                Negocio.Informe inf = new Negocio.Informe(dtpDesde.Value, dtpHasta.Value);

                var tmp = await inf.EmitirInforme();
                tmp.ForEach(e =>
                {
                    dtInforme.Rows.Add(
                        e.Fecha.ToString("dd/MM/yyyy"),
                        "$" + e.Recaudacion.ToString("0.00")
                        );
                });

                dgvInforme.DataSource = dtInforme;
            }
            else
            {
                MessageBox.Show("Ingrese un intervalo de fechas valido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));
            string PaginaHTML_Texto = Properties.Resources.Plantilla.ToString();

            string filas = string.Empty;
            decimal total = 0;
            foreach (DataGridViewRow row in dgvInforme.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells[0].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells[1].Value.ToString() + "</td>";
                filas += "</tr>";
                total += decimal.Parse(row.Cells[1].Value.ToString().TrimStart('$'));
            }
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@FILAS", filas);
            PaginaHTML_Texto = PaginaHTML_Texto.Replace("@TOTAL", total.ToString());

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    //pdfDoc.Add(new Phrase("Hola"));
                    using (StringReader reader = new StringReader(PaginaHTML_Texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                    }
                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }
    }
}
