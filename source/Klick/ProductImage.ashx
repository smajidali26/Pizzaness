<%@ WebHandler Language="C#" Class="ProductImage" %>

using System;
using System.Web;
using System.IO;

public class ProductImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "image/jpg";
        String fileName = context.Request["image"];
        String imageSize = context.Request["size"];
        String path = context.Server.MapPath("~/Products/");
        if (!File.Exists(path + imageSize + "_" + fileName))
        {
            ThumbImage(path + fileName, path + "L_" + fileName, 180, 180);
            ThumbImage(path + fileName, path + "S_" + fileName, 60, 60);
        }
        context.Response.WriteFile("/Products/" + imageSize + "_" + fileName);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private bool ThumbImage(string originalFile, string newFile, int width, int height)
    {
        try
        {

            // Create an Image object from a file. 

            // PhotoTextBox.Text is the full path of your image

            using (System.Drawing.Image photoImg = System.Drawing.Image.FromFile(originalFile))
            {

                // Create a Thumbnail from image with size 50x40.

                // Change 50 and 40 with whatever size you want

                using (System.Drawing.Image thumbPhoto = photoImg.GetThumbnailImage(width, height, null, new System.IntPtr()))
                {

                    // The below code converts an Image object to a byte array

                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        thumbPhoto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imgBytes = ms.ToArray();
                        thumbPhoto.Dispose();
                        photoImg.Dispose();
                        return ByteArrayToFile(newFile, imgBytes);

                    }
                }

            }

        }
        catch (Exception exp)
        {
            return false;

        }
    }

    public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
    {
        try
        {
            // Open file for reading
            System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

            // close file stream
            _FileStream.Close();

            return true;
        }
        catch (Exception _Exception)
        {

        }

        // error occured, return false
        return false;
    }

}