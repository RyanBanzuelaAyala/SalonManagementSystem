using eApp.Web.Admin.ADO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace eApp.Web.Admin.Resources.LibClass
{
    public class ImageFunc
    {
        public bool RemoveProductPic(string barcode)
        {            
            var imgQQ = barcode + ".jpg";

            var absolutePath = HostingEnvironment.MapPath("~/Uploadpic/" + imgQQ);
                        
            if (File.Exists(absolutePath))
            {
                // Trash Garbage Collection
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(absolutePath);
                
                var absolutePathThumb = HostingEnvironment.MapPath("~/Uploadpic/thmb" + imgQQ);

                if (File.Exists(absolutePathThumb))
                {
                    // Trash Garbage Collection
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(absolutePathThumb);
                    
                }

                return true;

            }
            else
            {
                return false;
            }
        }

        public string UploadProductPic(HttpFileCollectionBase Files, string imgcode)
        {
            dbsmappEntities db = new dbsmappEntities();

            var filepath = "";

            if (Files == null)
            {
                filepath = "";
            }
            
            for (int i = 0; i < Files.Count; i++)
            {
                var file = Files[i].FileName;
                
                var imgQQ = imgcode + ".jpg";
                
                var absolutePath = HostingEnvironment.MapPath("~/Uploadpic/" + imgQQ);

                // Trash Garbage Collection
                if (File.Exists(absolutePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(absolutePath);
                }

                filepath = HostingEnvironment.MapPath("~/Uploadpic/") + imgQQ;

                // Save Original Image
                Files[i].SaveAs(filepath);
                
                // Thumbnail
                ImagePro(filepath, imgQQ);
                
                // Save Pic Information
                db.ysysphotoes.Add(new ysysphoto()
                {
                    imgcode = imgcode,
                    picthmb = "",
                    remarks = "",
                    status = "",
                });

                db.SaveChanges();
            }

            return filepath;

        }

        private void ImagePro(string path, string code)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);

            // Compute thumbnail size.
            Size thumbnailSize = GetThumbnailSize(image);

            using (System.Drawing.Image thumbnail = image.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    thumbnail.Save(memoryStream, ImageFormat.Jpeg);
                    Byte[] bytes = new Byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    //var newImg = "data:image/png;base64," + base64String;

                    string pathX = HostingEnvironment.MapPath("~/Uploadpic/");

                    string fileNameWitPath = pathX + "thmb" + code;

                    using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(base64String);

                            bw.Write(data);

                            bw.Close();
                        }

                    }

                }
            }
        }
        
        private static Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 120;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
        
        public bool ThumbnailCallback()
        {
            return false;
        }

    }
}