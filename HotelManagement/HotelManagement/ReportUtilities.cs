using CsvHelper;
using HotelManagement.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.Text;

namespace HotelManagement
{
	public class ReportUtilities
	{
		public void ExportToExcel<T>(List<T> data, string fileName)
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

            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path + @"\" + fileName + ".csv")
            {
                UseShellExecute = true
            };
            p.Start();

            new MessageBoxCustom("Export sucessfully", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
        }

    }
}
