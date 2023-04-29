using System;
using System.Diagnostics;
using System.Windows;

namespace Gerenciador_de_Estoques_V2.Export
{
    public class DataExport
    {
        public static void ExportSqlDataToExcel()
        {
            string pythonPath = @"C:/Users/TKFir/AppData/Local/Microsoft/WindowsApps/python3.10.exe";
            string scriptPath = @"c:/Users/TKFir/source/repos/Gerenciador_de_Estoques_V2/Gerenciador de Estoques V2/Infra/conexaobd.py";

            string cmd = $"{pythonPath} \"{scriptPath}\"";

            try
            {
                Process.Start("CMD.exe", $"/c {cmd}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            string sourceFilePath = @"C:\Users\TKFir\source\repos\Gerenciador_de_Estoques_V2\Gerenciador de Estoques V2\bin\Debug\net6.0-windows\produtos.xlsx";
            string destFilePath = GetFolderPathFromUser();

            if (destFilePath != null)
            {
                string destFullPath = destFilePath + @"\produtos.xlsx";
                
                try
                {                    
                    System.IO.File.Move(sourceFilePath, destFullPath);

                    MessageBoxResult resultado = MessageBox.Show("Arquivo exportado com sucesso! Gostaria de abri-lo?",
                        "Arquivo exportado", MessageBoxButton.YesNo, MessageBoxImage.Information);

                    if (resultado == MessageBoxResult.Yes)
                    {
                        Process.Start(destFullPath);
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nenhum diretório selecionado", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
                
        private static string GetFolderPathFromUser()
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Selecione a pasta para salvar o arquivo";
                dialog.ShowNewFolderButton = true;
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }
                else
                {
                    return null;
                }
            }
        }        
    }    
}
