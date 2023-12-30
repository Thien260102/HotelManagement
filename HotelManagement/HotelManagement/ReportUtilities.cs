using CsvHelper;
using HotelManagement.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using Spire.DataExport.RTF;
using System.Drawing;

namespace HotelManagement
{
    public class ReportUtilities
    {
        public static void ExportToExcel<T>(List<T> data, string fileName)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            dlg.ShowNewFolderButton = true;

            string path = "";
            if (dlg.ShowDialog() == true)
            {
                path = dlg.SelectedPath;
            }

            using (var writer = new StreamWriter(path + @"\" + fileName + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }

            new MessageBoxCustom("Export sucessfully", MessageType.Success, MessageButtons.Ok).ShowDialog();

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path + @"\" + fileName + ".csv")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public static void ExportToExcel(DataTable data, string fileName)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();
            dlg.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            dlg.ShowNewFolderButton = true;

            string path = "";
            if (dlg.ShowDialog() == true)
            {
                path = dlg.SelectedPath;
            }

            using (var writer = new StreamWriter(path + @"\" + fileName + ".csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                //csv.WriteRecords(data.Rows);

                // Write columns
                foreach (DataColumn column in data.Columns)
                {
                    csv.WriteField(column.ColumnName);
                }
                csv.NextRecord();

                // Write row values
                foreach (DataRow row in data.Rows)
                {
                    for (var i = 0; i < data.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]);
                    }
                    csv.NextRecord();
                }
            }

            new MessageBoxCustom("Export sucessfully", MessageType.Success, MessageButtons.Ok).ShowDialog();

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path + @"\" + fileName + ".csv")
            {
                UseShellExecute = true
            };
            p.Start();
        }

        public static void ExportToPDF(DataTable data, string fileName)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (*.pdf)|*.pdf";
            sfd.FileName = fileName + ".pdf";
            bool fileError = false;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    try
                    {
                        File.Delete(sfd.FileName);
                    }
                    catch (IOException ex)
                    {
                        fileError = true;
                        new MessageBoxCustom("It wasn't possible to write the data to the disk." + ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
                    }
                }
                if (!fileError)
                {
                    try
                    {
                        PdfPTable pdfTable = new PdfPTable(data.Columns.Count);
                        pdfTable.DefaultCell.Padding = 3;
                        pdfTable.WidthPercentage = 100;
                        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                        foreach (DataColumn column in data.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase((string)column.ColumnName));
                            pdfTable.AddCell(cell);
                        }

                        foreach (DataRow row in data.Rows)
                        {
                            for (int i = 0; i < row.ItemArray.Length; i++)
                            {
                                pdfTable.AddCell(row.ItemArray[i].ToString());
                            }
                        }

                        using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            var pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 20f, 20f, 10f);
                            PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                            pdfDoc.Add(pdfTable);
                            pdfDoc.Close();
                            stream.Close();
                        }
                        new MessageBoxCustom("Data Exported Successfully !!!", MessageType.Success, MessageButtons.Ok).ShowDialog();

                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo(sfd.FileName)
                        {
                            UseShellExecute = true
                        };
                        p.Start();
                    }
                    catch (Exception ex)
                    {
                        new MessageBoxCustom("Error:" + ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
                    }
                }
            }

        }

        public static void ExportToWord(DataTable data, string filename)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Word Documents (*.doc)|*.doc";

            sfd.FileName = filename + ".doc";

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Create a new file
            using (StreamWriter sw = File.CreateText(sfd.FileName))
            {
                sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                sw.WriteLine("Author: Mahesh Chand");
                sw.WriteLine("Add one more line ");
                sw.WriteLine("Add one more line ");
                sw.WriteLine("Done! ");
            }

            Spire.DataExport.RTF.RTFExport rtfExport = new Spire.DataExport.RTF.RTFExport();
            rtfExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
            rtfExport.DataTable = data;

            rtfExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;

            RTFStyle rtfStyle = new RTFStyle();

            rtfStyle.FontColor = Color.Blue;

            rtfStyle.BackgroundColor = Color.LightGreen;

            rtfExport.RTFOptions.DataStyle = rtfStyle;

            rtfExport.FileName = sfd.FileName;

            rtfExport.SaveToFile();

            new MessageBoxCustom("Export sucessfully", MessageType.Success, MessageButtons.Ok).ShowDialog();

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(sfd.FileName)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    
        
    }
}
