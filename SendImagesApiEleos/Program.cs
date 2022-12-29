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
                    facLabControler.enviarNotificacion(segmento, SourceFile, segmento);
                    itema.Delete();
                }
            }
        }

    }
}
