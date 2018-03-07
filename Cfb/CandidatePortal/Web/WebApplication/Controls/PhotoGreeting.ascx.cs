using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.UI;
using Image = System.Drawing.Image;

namespace Cfb.CandidatePortal.Web.WebApplication.Controls
{
    public partial class PhotoGreeting : UserControl
    {
        /// <summary>
        /// The number of bytes in a megabyte.
        /// </summary>
        private const uint BytesPerMegabyte = 1048576;

        /// <summary>
        /// The maxmium candidate photo width in pixels.
        /// </summary>
        private const byte MaxPhotoWidth = 150;

        /// <summary>
        /// The maximum candidate photo height in pixels.
        /// </summary>
        private const byte MaxPhotoHeight = 150;

        /// <summary>
        /// The maximum acceptable file size for uploaded candidate photos, in megabytes.
        /// </summary>
        private const byte MaxPhotoFileSizeMB = 5;

        /// <summary>
        /// Handles the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(Page);
            if (object.Equals(sm, null))
            {
                sm = new ScriptManager();
                sm.SupportsPartialRendering = true;
                this.Controls.Add(sm);
            }
            base.OnInit(e);
        }

        /// <summary>
        /// Handles the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _candidatePhotoPanel.Visible = _change.Visible = _delete.Visible = ProfilePhotoHandler.CustomPhotoExists(CPProfile.Cid);
            _photoUpload.Attributes.Add("size", "28"); // width of file inputs can only be set via size attribute
            // disable upload button upon first click
            _upload.OnClientClick = "this.disabled=true;" + Page.ClientScript.GetPostBackEventReference(_upload, string.Empty);
            ShowGreeting();
        }

        /// <summary>
        /// Uploads a posted image to the candidate photo repository for the current candidate.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void UploadPhoto(object sender, EventArgs e)
        {
            if (Page.IsValid && this.IsPostBack)
            {
                // upload image
                try
                {
                    if (_photoUpload.HasFile)
                    {
                        try
                        {
                            using (Image i = Image.FromStream(_photoUpload.FileContent, true, true))
                            {
                                if (_photoUpload.FileContent.Length <= MaxPhotoFileSizeMB * BytesPerMegabyte)
                                {
                                    string photoPath = ProfilePhotoHandler.GetPhotoFilePath(CPProfile.Cid);
                                    if (File.Exists(photoPath) && !string.IsNullOrEmpty(CPProfile.Cid))
                                        File.Delete(photoPath);
                                    if ((i.Height > MaxPhotoHeight) || (i.Width > MaxPhotoWidth))
                                    {
                                        using (Image t = MakeThumbnailFrom(i))
                                        {
                                            t.Save(photoPath);
                                        }
                                    }
                                    else
                                    {
                                        i.Save(photoPath);
                                    }
                                    i.Dispose();
                                    ShowGreeting();
                                }
                                else
                                {
                                    // when file is too big
                                    SetError(string.Format("The file provided exceeds the maximum acceptable image size of {0} MB. Please try uploading a smaller file.", MaxPhotoFileSizeMB));
                                }
                            }
                        }
                        catch (ArgumentException)
                        {
                            // when file is valid but not an image
                            SetError("The file provided is not a supported image. Please try uploading a BMP, GIF, JPG, TIF, or PNG file.");
                        }
                    }
                    else
                    {
                        // when no file was supplied
                        SetError("Please first browse to an image for uploading.");
                    }
                }
                catch (ArgumentException)
                {
                    // when an invalid path was supplied
                    SetError("The file specified could not be found. Please try browsing to the file again.");
                }
            }
        }

        /// <summary>
        /// Deletes the current user-supplied photo on file for the candidate, if available.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
        protected void DeletePhoto(object sender, EventArgs e)
        {
            string photoPath = ProfilePhotoHandler.GetPhotoFilePath(CPProfile.Cid);
            if (File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }
            ShowGreeting();
            _updatePanel.Update();
        }

        protected void Change(object sender, EventArgs e)
        {
            ShowUpload();
            _updatePanel.Update();
        }

        protected void Cancel(object sender, EventArgs e)
        {
            ShowGreeting();
            _updatePanel.Update();
        }

        /// <summary>
        /// Makes a thumbnail from an image.
        /// </summary>
        /// <param name="orig">The original source image.</param>
        /// <returns>A thumbnail of the source image.</returns>
        private Image MakeThumbnailFrom(Image orig)
        {
            if (object.Equals(orig, null))
                return null;
            if ((orig.Width <= MaxPhotoWidth) && (orig.Height <= MaxPhotoHeight))
                return orig;
            int width = orig.Width;
            int height = orig.Height;
            if (width > MaxPhotoWidth)
            {
                height = Convert.ToInt32(height * MaxPhotoWidth / width);
                width = MaxPhotoWidth;
            }
            if (height > MaxPhotoHeight)
            {
                width = Convert.ToInt32((width * MaxPhotoHeight / height));
                height = MaxPhotoHeight;
            }
            // fix for error when creating graphics object from indexed format images (e.g. GIF)
            // - derive Bitmap object from Image instance instead of directly
            // Bitmap thumb = new Bitmap(width, height, orig.PixelFormat); //ERROR!
            Bitmap thumb = new Bitmap(orig, width, height);
            Graphics g = Graphics.FromImage(thumb);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle r = new Rectangle(0, 0, thumb.Width, thumb.Height);
            g.DrawImage(orig, r);
            return thumb;
        }

        /// <summary>
        /// Shows the greeting message and hides the photo upload controls.
        /// </summary>
        private void ShowGreeting()
        {
            _uploadPanel.Style[HtmlTextWriterStyle.Display] = "none";
            _greetingPanel.Style.Remove(HtmlTextWriterStyle.Display);
            // refresh the photo image
            if (!string.IsNullOrEmpty(_photo.ImageUrl) && _photo.ImageUrl.Contains("?"))
                _photo.ImageUrl = _photo.ImageUrl.Substring(0, _photo.ImageUrl.IndexOf('?'));
            _photo.ImageUrl = _photo.ImageUrl + "?ver=" + Server.UrlEncode(DateTime.Now.ToLongTimeString());
        }

        /// <summary>
        /// Shows the photo upload controls and hides the greeting message.
        /// </summary>
        private void ShowUpload()
        {
            _uploadMessage.CssClass = string.Empty;
            ShowUpload(Resources.CPResources.uploadPhoto_text);
        }

        /// <summary>
        /// Shows the photo upload controls with a message and hides the greeting message.
        /// </summary>
        /// <param name="message">The message to show.</param>
        private void ShowUpload(string message)
        {
            _greetingPanel.Style[HtmlTextWriterStyle.Display] = "none";
            _uploadPanel.Style.Remove(HtmlTextWriterStyle.Display);
            _uploadMessage.Text = message;
        }

        /// <summary>
        /// Shows an error message in place of the greeting message.
        /// </summary>
        /// <param name="error">The error message to show.</param>
        private void SetError(string error)
        {
            _uploadMessage.CssClass = "error";
            ShowUpload(error);
        }
    }
}