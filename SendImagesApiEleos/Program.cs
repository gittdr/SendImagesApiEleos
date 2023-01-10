using Renci.SshNet;
using SendImagesApiEleos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendImagesApiEleos
{
    public class Program
    {
        public static FacLabControler facLabControler = new FacLabControler();
        public static void Main(string[] args)
        {
            try
            {
                var host = "10.176.167.171";
                var port = 22;
                var username = "pages";
                var password = "single";
                var uploadFile = @"C:\Administración\ApiEleos\Images\ORD_BAJ_127777_UNK_101570258.tif";

                using (var client = new SftpClient(host, port, username, password))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        //Debug.WriteLine("I'm connected to the client");

                        using (var fileStream = new FileStream(uploadFile, FileMode.Open))
                        {

                            client.BufferSize = 4 * 1024; // bypass Payload error large files
                            client.UploadFile(fileStream, Path.GetFileName(uploadFile));
                        }
                    }
                    else
                    {
                        //Debug.WriteLine("I couldn't connect");
                    }
                }
                Program obj = new Program();
                obj.SendImage();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SendImage()
        {
            DirectoryInfo dir = new DirectoryInfo(@"\\10.223.208.41\Users\Administrator\Documents\ImagesEleos");
            //var fImage = @"\\10.223.208.41\Users\Administrator\Documents\ImagesEleos\" + filenamef;
            FileInfo[] files = dir.GetFiles("*.tif");


            int cantidad = files.Length;
            if (cantidad > 0)
            {
                foreach (var itema in files)
                {
                    string SourceFile = @"\\10.223.208.41\Users\Administrator\Documents\ImagesEleos\" + itema.Name;
                    string File_name = itema.Name.Replace(".tif", "");
                    string segmento = File_name;
                    var host = "10.176.167.171";
                    var port = 22;
                    var username = "pages";
                    var password = "single";
                    //var uploadFile = @"C:\Administración\ApiEleos\Images\ORD_BAJ_1237089_UNK_101286898.tif";

                    using (var client = new SftpClient(host, port, username, password))
                    {
                        client.Connect();
                        if (client.IsConnected)
                        {
                            //Debug.WriteLine("I'm connected to the client");

                            using (var fileStream = new FileStream(SourceFile, FileMode.Open))
                            {

                                client.BufferSize = 10 * 1024; // bypass Payload error large files
                                client.UploadFile(fileStream, Path.GetFileName(SourceFile));
                            }
                        }
                        else
                        {
                            //Debug.WriteLine("I couldn't connect");
                        }
                    }
                    //facLabControler.enviarNotificacion(segmento, SourceFile, segmento);
                    itema.Delete();
                }
            }
        }

    }
}
