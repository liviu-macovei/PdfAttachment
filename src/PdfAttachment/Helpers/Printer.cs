using System.Diagnostics;
using System.IO;
using System.Text;

namespace PdfAttachment.Helpers
{
    public static class Printer
    {
        public const string HtmlToPdfExePath = "wkhtmltopdf.exe";

        public static void GeneratePdf(string commandLocation, string html, Stream pdf)
        {
            Process p;
            StreamWriter stdin;
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = Path.Combine(commandLocation, HtmlToPdfExePath);
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);

            // run the conversion utility
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // note: that we tell wkhtmltopdf to be quiet and not run scripts
            psi.Arguments = "-q -n --encoding utf-8 --minimum-font-size 12 --header-spacing 25 --header-html http://restservices.foedevarestyrelsen.dk/Dyreforsoeg/Configuration/Header.html" //--minimum-font-size 7 --page-width 1000 --dpi 300 --print-media-type --page-size A4
                + " --footer-spacing 20 --footer-html http://restservices.foedevarestyrelsen.dk/Dyreforsoeg/Configuration/Footer.html"
                + " " + " - -";

            p = Process.Start(psi);

            try
            {
                stdin = p.StandardInput;

                var utf8Writer = new StreamWriter(stdin.BaseStream, Encoding.UTF8);
                utf8Writer.AutoFlush = true;
                stdin.AutoFlush = true;
                utf8Writer.Write(html);
                utf8Writer.Flush();
                stdin.Flush();
                stdin.Dispose();

                var result = ReadFully(p.StandardOutput.BaseStream);
                pdf.Write(result, 0, result.Length);

                p.StandardOutput.Dispose();
                
                pdf.Position = 0;
                p.WaitForExit(10000);
            }
            catch
            {
            }
            finally
            {
                p.Dispose();
            }
        }

        public static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }
    }
}
