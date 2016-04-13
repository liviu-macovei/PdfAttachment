using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfAttachment.Helpers
{
    public class PdfAttacher
    {
        public const string PDF_TOOL_TO_ATTACH_FILES = "pdftk.exe";
        public void AddAttachments(string commandLocation,string pdfLocation)
        {
            Process p;
            StreamWriter stdin;
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = Path.Combine(commandLocation, PDF_TOOL_TO_ATTACH_FILES);
            psi.WorkingDirectory = Path.GetDirectoryName(psi.FileName);

            // run the conversion utility
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // note: that we tell wkhtmltopdf to be quiet and not run scripts
            psi.Arguments = "1.pdf attach_files 2.pdf 3.docx 4.jpg to_page end output 1withAttachments.pdf";
            p = Process.Start(psi);
            try
            {
              

                var result = ReadFully(p.StandardOutput.BaseStream);                
                var ceva= p.StandardOutput.ReadToEnd().ToString();
                p.StandardOutput.Dispose();
                
                p.WaitForExit(10000);
            }
            catch(Exception ex)
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
