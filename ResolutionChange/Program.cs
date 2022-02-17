using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ResolutionChange
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] arg)
        {
            List<string> argument = new();
            foreach (var item in arg)
                argument.Add(item);

            if (argument.Contains("--gr"))
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                string sms = String.Format("La Resolucion es: {0}x{1}\nWidth: {0}\nHeight: {1}",
                    width,
                    height);
                MessageBox.Show(sms,
                    "Resolucion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else if (argument.Contains("--rc"))//Muestra las resoluciones compatibles
            {
                List<string> CompatibleRes = ResolutionGet.GetResolution();
                string res = "";
                int cont = 1;
                foreach (var item in CompatibleRes)
                {
                    if(cont<10)
                        res += "0"+cont + " - " + item + "\n";
                    else
                        res += cont + " - " + item + "\n";
                    cont++;
                }
                MessageBox.Show(res,
                    "Resolucion Compatible",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            else if (argument.Contains("--help") || argument.Contains("-h"))
            {
                string sms = "AYUDA:\n" +
                    "-R (Cambia la resolucion)\n" +
                    "ScreenChange WidthxHeight\nEjemplo: ScreenChange 1600x900\n\n" +
                    "--gr (Get Resolution)\nObtiene la resolucion de la pantalla actual\n\n" +
                    "--rc (Resolution Compatible)\nObtiene todas las resoluciones compatibles para el monitor.\n\n" +
                    "-h, --help (Ayuda)\tObtiene la ayuda del software.";
                MessageBox.Show(sms);
                return;
            }
            else if (argument.Contains("-R"))
            {
                int pos = argument.LastIndexOf("-R") + 1;
                List<string> CompatibleRes = ResolutionGet.GetResolution();
                if (arg.Length > 0)
                {
                    string[] split = argument[pos].Split("x");
                    int Width = Convert.ToInt32(split[0]);
                    int Height = Convert.ToInt32(split[1]);
                    if (split.Length == 2)
                    {
                        if (Regex.IsMatch(Width.ToString(), "\\d+") &&
                            Regex.IsMatch(Height.ToString(), "\\d+"))
                        {
                            int y = Convert.ToInt32(CompatibleRes[CompatibleRes.Count - 1].Split("x")[0]);
                            int x = Convert.ToInt32(CompatibleRes[0].Split("x")[1]);
                            if (Width > x && Height < y)
                            {
                                if (CompatibleRes.Contains(argument[pos]))
                                {
                                    DialogResult change = MessageBox.Show("Usted esta apunto de Cambiar la resolucion de la pantalla\nDesea prosegir?", "Cambiando..",
                                        MessageBoxButtons.OKCancel,
                                        MessageBoxIcon.Question);
                                    if (change == DialogResult.OK)
                                        Resolution.CResolution.ChangeResolution(Width, Height);
                                    else
                                        MessageBox.Show(
                                            "Se ha Cancelado, el Proceso.",
                                            "Cancelado",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Stop);
                                }
                                else//la resolucion ingresada no es compatible con el monitor
                                {
                                    MessageBox.Show("Resolucion no compatible");
                                }
                            }
                            else //la resolucion esta fuera del rango
                            {
                                MessageBox.Show("Fuera de rango");
                            }
                        }
                        else //si el valor de width o height no es un numero
                        {
                            MessageBox.Show("Error No es Numerico");
                        }
                    }
                }
                else//si no se a pasado ningun argumento
                {
                    string[] resolution = CompatibleRes[CompatibleRes.Count - 1].Split("x");
                    int width = Convert.ToInt32(resolution[0]);
                    int height = Convert.ToInt32(resolution[1]);
                    Resolution.CResolution.ChangeResolution(width, height);
                }
            }
            else
            {
                string sms = "AYUDA:\n" +
                    "-R (Cambia la resolucion)\n" +
                    "ScreenChange WidthxHeight\nEjemplo: ScreenChange 1600x900\n\n" +
                    "--gr (Get Resolution)\nObtiene la resolucion de la pantalla actual\n\n" +
                    "--rc (Resolution Compatible)\nObtiene todas las resoluciones compatibles para el monitor.\n\n" +
                    "-h, --help (Ayuda)\tObtiene la ayuda del software.";
                MessageBox.Show("NO SE HA RECONOCIDO EL COMANDO\n" + sms,
                    "[ERROR]",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Hand);
            }
        }
    }
}