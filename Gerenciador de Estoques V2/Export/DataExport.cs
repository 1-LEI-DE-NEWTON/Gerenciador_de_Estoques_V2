using System.Diagnostics;
using System.Windows.Forms;

namespace Gerenciador_de_Estoques_V2.Export
{
    public class DataExport
    {
        public static void ExportSqlDataToExcel()
        {
            string pythonPath = @"C:/Users/TKFir/AppData/Local/Microsoft/WindowsApps/python3.10.exe";
            string scriptPath = @"c:/Users/TKFir/source/repos/Gerenciador_de_Estoques_V2/Gerenciador de Estoques V2/Infra/conexaobd.py";

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "cmd.exe";
            start.Arguments = @"C:/Users/TKFir/AppData/Local/Microsoft/WindowsApps/python3.10.exe ""c:/Users/TKFir/source/repos/Gerenciador_de_Estoques_V2/Gerenciador de Estoques V2/Infra/conexaobd.py""";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;

            string cmd = $"{pythonPath} \"{scriptPath}\"";
            Process.Start("cmd.exe", $"/c {cmd}");

            string sourceFilePath = @"C:\Users\TKFir\source\repos\Gerenciador_de_Estoques_V2\Gerenciador de Estoques V2\bin\Debug\net6.0-windows\produtos.xlsx";
            string destFilePath = @"C:\Users\TKFir\source\repos\Gerenciador_de_Estoques_V2\Gerenciador de Estoques V2\Infra\produtos.xlsx";

            // Move the file to the destination directory
            System.IO.File.Move(sourceFilePath, destFilePath);
        }
        
        /*
        private static string GetFolderPathFromUser()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Selecione a pasta para salvar o arquivo";
                dialog.ShowNewFolderButton = true;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
                else
                {
                    return null;
                }
            }
        }
        */
    }    
}
