using AutoMapper;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace KaribouAlpha.Controllers
{
    public class ProductFilesController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetProductFilesForEditing")]
        [Authorize]
        public IEnumerable<ProductFileDTO> GetProductFilesForEditing(long productId, long? followerGroupId = null)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.ProductFiles
                                                    .Select(productFile => productFile.GroupsVisibleTo))
                                                .Include(_product => _product.TeamMembers
                                                    .Select(teamMember => teamMember.User)
                                                    .Select(teamMember => teamMember.Company))
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && ( teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value >0) )).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<ProductFile> productFiles;

            if(followerGroupId == null)
            {
                return Mapper.Map<IEnumerable<ProductFileDTO>>(product.ProductFiles);
            }
            
            return Mapper.Map<IEnumerable<ProductFileDTO>>(product.ProductFiles.Where(productFile => 
                                                                                                                                                    (productFile.Privacy == ProductFilePrivacy.Public) ||
                                                                                                                                                    ((productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) &&
                                                                                                                                                    productFile.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.Where(followerGroup => followerGroup.ID == followerGroupId).FirstOrDefault()))));
        }

        [HttpGet]
        [Route("api/GetProductFiles")]
        [Authorize]
        public IEnumerable<ProductFileViewDTO> GetProductFiles(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.ProductFiles
                                                    .Select(productFile => productFile.GroupsVisibleTo))
                                                .Include(_product => _product.GroupsVisibleTo)
                                                .Include(_product => _product.TeamMembers)
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            IEnumerable<ProductFile> productFiles;

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                if (product.Privacy == ProductPrivacy.Private)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                if ((product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && product.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.FirstOrDefault(followerGroup =>
                                                                                                                                              followerGroup.CompanyID == product.CompanyID &&
                                                                                                                                              followerGroup.Followers.Any(follower => follower.UserID == user.Id))) == false)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                productFiles = product.ProductFiles.Where(productFile =>
                                                                                (productFile.Privacy == ProductFilePrivacy.Public) ||
                                                                                ((productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) &&
                                                                                (productFile.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.FirstOrDefault(followerGroup =>
                                                                                followerGroup.CompanyID == product.CompanyID &&
                                                                                followerGroup.Followers.Any(follower => follower.UserID == user.Id))))))
                                                                                .OrderBy(productFile => productFile.Category)
                                                                                .ThenByDescending(productFile => productFile.UploadedAt);
            }
            else
            {
                productFiles = product.ProductFiles.OrderBy(productFile => productFile.Category).ThenByDescending(productFile => productFile.UploadedAt);
            }

            return Mapper.Map<IEnumerable<ProductFileViewDTO>>(productFiles);
        }

        [HttpGet]
        [Route("api/GetMyProductFiles")]
        [Authorize]
        public IEnumerable<ProductFileViewDTO> GetMyProductFiles(long productId, long? followerGroupId = null)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.ProductFiles
                                                    .Select(productFile => productFile.GroupsVisibleTo))
                                                .Include(_product => _product.GroupsVisibleTo)
                                                .Include(_product => _product.TeamMembers)
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && (teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value >0))).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IEnumerable<ProductFile> productFiles;

            if (followerGroupId == null)
            {
                productFiles = product.ProductFiles.OrderBy(productFile => productFile.Category)
                                                                                  .ThenByDescending(productFile => productFile.UploadedAt);
            }
            else
            {
                productFiles = product.ProductFiles.Where(productFile =>
                                                                                   (productFile.Privacy == ProductFilePrivacy.Public) ||
                                                                                   ((productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) && 
                                                                                   productFile.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.FirstOrDefault(followerGroup => followerGroup.ID == followerGroupId))))
                                                                                  .OrderBy(productFile => productFile.Category)
                                                                                  .ThenByDescending(productFile => productFile.UploadedAt);
            }

            return Mapper.Map<IEnumerable<ProductFileViewDTO>>(productFiles);
        }

        [HttpGet]
        [Route("api/CheckMyProductFileIsPublic")]
        [Authorize]
        public IHttpActionResult CheckMyProductFileIsPublic(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            bool isPublic = !db.ProductFiles.Any(file => file.ProductID == productId && file.Privacy != ProductFilePrivacy.Public);

            return Ok(isPublic);
        }


        [HttpGet]
        [Route("api/GetMyProductFilesForFollowerGroup")]
        [Authorize]
        public IEnumerable<ProductFileViewDTO> GetMyProductFilesForFollowerGroup(long productId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.ProductFiles
                                                    .Select(productFile => productFile.GroupsVisibleTo))
                                                .Include(_product => _product.GroupsVisibleTo)
                                                .Include(_product => _product.TeamMembers)
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && ( teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value > 0 ) ) ).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IEnumerable<ProductFile> productFiles = product.ProductFiles.Where(productFile =>
                                                                                                                                (productFile.Privacy == ProductFilePrivacy.Public) || 
                                                                                                                                ((productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) && 
                                                                                                                                productFile.GroupsVisibleTo.Any(followerGroup => followerGroup.ID == followerGroupId)))
                                                                                                                                .OrderBy(productFile => productFile.Category)
                                                                                                                                .ThenByDescending(productFile => productFile.UploadedAt);

            return Mapper.Map<IEnumerable<ProductFileViewDTO>>(productFiles);
        }

        [HttpGet]
        [Route("api/GetMyPublicProductFiles")]
        [Authorize]
        public IEnumerable<ProductFileViewDTO> GetMyPublicProductFiles(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.ProductFiles
                                                    .Select(productFile => productFile.GroupsVisibleTo))
                                                .Include(_product => _product.GroupsVisibleTo)
                                                .Include(_product => _product.TeamMembers)
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && ( teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value > 0))).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IEnumerable<ProductFile> productFiles = product.ProductFiles.Where(productFile => productFile.Privacy == ProductFilePrivacy.Public)
                                                                                                                                .OrderBy(productFile => productFile.Category)
                                                                                                                                .ThenByDescending(productFile => productFile.UploadedAt);

            return Mapper.Map<IEnumerable<ProductFileViewDTO>>(productFiles);
        }

        [HttpPost]
        [Route("api/UploadProductFiles")]
        [Authorize]
        public async Task<IHttpActionResult> UploadProductFiles(NewProductFileDTO[] newProductFiles)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            for (int i = 0; i < newProductFiles.Length; i++)
            {
                long productId = newProductFiles[i].ProductID;

                string visibility = newProductFiles[i].Visibility;

                Product product = db.Products.Find(productId);

                if (product == null)
                {
                    return NotFound();
                }

                if ((product.CompanyID.HasValue && product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                ProductFile productFile = Mapper.Map<NewProductFileDTO, ProductFile>(newProductFiles[i]);
                ProductUpdate productUpdate = new ProductUpdate();
                productFile.Product = product;
                productFile.Type = ProductFileType.Other;
                productFile.UploadedAt = DateTime.Now;
                if(!string.IsNullOrEmpty(visibility) && visibility.ToLower() == "private")
                {
                    productFile.Privacy = ProductFilePrivacy.Private;
                }
                productFile = db.ProductFiles.Add(productFile);
                productUpdate.UserID = user.Id;
                productUpdate.User = user;
                productUpdate.ProductID = product.ID;
                productUpdate.Product = product;
                productUpdate.ProductFileID = productFile.ID;
                productUpdate.ProductFile = productFile;
                productUpdate.DateTime = DateTime.Now;
                productUpdate.UpdateType = UpdateType.ProductFileAdded;
                db.ProductUpdates.Add(productUpdate);
            }

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/GetProductFileImage")]
        public async Task<IHttpActionResult> GetProductFileImage(long productFileId, int? pixelWidth = null, int? pixelHeight = null)
        {
            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == productFileId).AsNoTracking().SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (productFile.Image != null)
            {
                imageBytes = productFile.Image;
                mediaTypeHeader = "image/png";
            }
            else
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/img/home_bg.png");

                imageBytes = File.ReadAllBytes(filePath);
                mediaTypeHeader = "image/png";
            }

            if (pixelWidth != null && pixelHeight != null)
            {
                imageBytes = ResizeImage(imageBytes, (int)pixelWidth, (int)pixelHeight);
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(mediaTypeHeader);

            return ResponseMessage(responseMessage);
        }

        [HttpGet]
        [Route("api/GetFileFromUrl")]
        public async Task<IHttpActionResult> GetFileFromUrl(string url)
        {
            byte[] fileBytes;

            using (WebClient webClient = new WebClient())
            {
                fileBytes = webClient.DownloadData(url);
            }
            
            MemoryStream memoryStream = new MemoryStream(fileBytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return ResponseMessage(responseMessage);
        }

        [HttpGet]
        [Route("api/SetProductFileCategory")]
        [Authorize]
        public async Task<IHttpActionResult> SetProductFileCategory(long id, ProductFileCategory category)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == id)
                                                                                        .Include(_productFile => _productFile.Product)
                                                                                        .Include(_productFile => _productFile.Product.TeamMembers)
                                                                                        .SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if(productFile.Category == category)
            {
                return Ok();
            }

            ProductUpdate productUpdate = new ProductUpdate();

            productFile.Category = category;
            db.Entry(productFile).State = EntityState.Modified;
            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = productFile.ProductID;
            productUpdate.Product = productFile.Product;
            productUpdate.ProductFileID = productFile.ID;
            productUpdate.ProductFile = productFile;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductFileEdited;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/SetProductFilePrivacy")]
        [Authorize]
        public async Task<IHttpActionResult> SetProductFilePrivacy(long id, ProductFilePrivacy privacy, long selectedGroupId = 0)
        {
             
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == id)
                                                                                        .Include(_productFile => _productFile.Product)
                                                                                        .Include(_productFile => _productFile.Product.TeamMembers)
                                                                                        .Include(_productFile => _productFile.GroupsVisibleTo)
                                                                                        .SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (productFile.Privacy == privacy && productFile.Privacy == ProductFilePrivacy.Public)
            {
                return Ok();
            }

            if (privacy != ProductFilePrivacy.VisibleToSelectedGroups && productFile.GroupsVisibleTo.Count() > 0)
            {
                productFile.GroupsVisibleTo.Clear();
            }

            if (privacy == ProductFilePrivacy.VisibleToSelectedGroups)
            {
                CompanyFollowerGroup selectedGroup = db.CompanyFollowerGroups.SingleOrDefault(c => c.ID == selectedGroupId);
                if (selectedGroup != null)
                {
                    if (!productFile.GroupsVisibleTo.Any(c => c.ID == selectedGroup.ID))
                    {
                        productFile.GroupsVisibleTo.Add(selectedGroup);
                        productFile.Privacy = KaribouAlpha.Models.ProductFilePrivacy.VisibleToSelectedGroups;
                    }
                    else
                    {
                        productFile.GroupsVisibleTo.Remove(selectedGroup);

                        if (productFile.GroupsVisibleTo.Count == 0)
                        {
                            productFile.Privacy = KaribouAlpha.Models.ProductFilePrivacy.Public;


                        }

                    }

                }
                
            }
            else
            {
                productFile.Privacy = KaribouAlpha.Models.ProductFilePrivacy.Public;
            }

            ProductUpdate productUpdate = new ProductUpdate();
            //productFile.Privacy = privacy;
            db.Entry(productFile).State = EntityState.Modified;
            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = productFile.ProductID;
            productUpdate.Product = productFile.Product;
            productUpdate.ProductFileID = productFile.ID;
            productUpdate.ProductFile = productFile;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductFileEdited;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/DeleteProductFile")]
        [Authorize]
        public async Task<IHttpActionResult> DeleteProductFile(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == id).SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductUpdate productUpdate = new ProductUpdate();

            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = productFile.ProductID;
            productUpdate.Product = productFile.Product;
            productUpdate.ProductFileID = productFile.ID;
            productUpdate.ProductFile = productFile;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductFileDeleted;
            db.ProductUpdates.Add(productUpdate);
            db.ProductFiles.Remove(productFile);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/DownloadMyProductFile")]
        [Authorize]
        public async Task<IHttpActionResult> DownloadMyProductFile(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == id).AsNoTracking().SingleOrDefault();

            if (productFile == null || productFile.Bytes == null || productFile.Bytes.Length == 0)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            MemoryStream memoryStream = new MemoryStream(productFile.Bytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return ResponseMessage(responseMessage);
        }

        [HttpGet]
        [Route("api/DownloadProductFile")]
        [Authorize]
        public async Task<IHttpActionResult> DownloadProductFile(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == id).AsNoTracking().SingleOrDefault();

            if (productFile == null || productFile.Bytes == null || productFile.Bytes.Length == 0)
            {
                return NotFound();
            }

            if ((productFile.Product.Privacy == ProductPrivacy.Private) ||
                (productFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                productFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if ((productFile.Privacy == ProductFilePrivacy.Private) ||
                (productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups &&
                productFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFileDownload productFileDownload = new ProductFileDownload();
            MemoryStream memoryStream = new MemoryStream(productFile.Bytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            
            productFileDownload.UserID = user.Id;
            productFileDownload.User = user;
            productFileDownload.ProductFileID = productFile.ID;
            productFileDownload.ProductFile = productFile;
            productFileDownload.DateTime = DateTime.Now;
            db.ProductFileDownloads.Add(productFileDownload);
            await db.SaveChangesAsync();
            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return ResponseMessage(responseMessage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductFileExists(long id)
        {
            return db.ProductFiles.Count(e => e.ID == id) > 0;
        }

        private void SetProductFileImageAndThumbnailFromOriginal(Image originalImage, ProductFile productFile)
        {
            byte[] imageBytes;
            byte[] thumbnailImageBytes;

            using (Image image = this.FixImageSizeByWidth(originalImage, 775))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    imageBytes = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(imageBytes, 0, imageBytes.Length);
                }
            }

            using (Image thumbnailImage = this.FixImageSizeByWidth(originalImage, 185))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    thumbnailImage.Save(stream, ImageFormat.Png);
                    thumbnailImageBytes = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(thumbnailImageBytes, 0, thumbnailImageBytes.Length);
                }
            }

            productFile.Image = imageBytes;
            //productFile.Thumbnail = thumbnailImageBytes;
        }

        private byte[] ResizeImage(byte[] imageBytes, int pixelWidth, int pixelHeight)
        {
            byte[] resizedImageBytes = null;

            using (MemoryStream originalImageMemoryStream = new MemoryStream(imageBytes))
            {
                using (Image originalImage = Bitmap.FromStream(originalImageMemoryStream))
                {
                    using (Image resizedImage = this.ResizeImage(originalImage, pixelWidth, pixelHeight))
                    {
                        using (MemoryStream resizedImageMemoryStream = new MemoryStream())
                        {
                            resizedImage.Save(resizedImageMemoryStream, ImageFormat.Png);
                            resizedImageBytes = new byte[resizedImageMemoryStream.Length];
                            resizedImageMemoryStream.Position = 0;
                            resizedImageMemoryStream.Read(resizedImageBytes, 0, resizedImageBytes.Length);
                        }
                    }
                }
            }

            return resizedImageBytes;
        }

        private Image ResizeImage(Image image, int pixelWidth, int pixelHeight)
        {
            int originalPixelWidth = image.Width;
            int originalPixelHeight = image.Height;
            int originalX = 0;
            int originalY = 0;
            int finalX = 0;
            int finalY = 0;
            float scale = 0;
            float widthScale = 0;
            float heightScale = 0;

            widthScale = ((float)pixelWidth / (float)originalPixelWidth);
            heightScale = ((float)pixelHeight / (float)originalPixelHeight);

            if (heightScale < widthScale)
            {
                scale = heightScale;
                finalX = Convert.ToInt16((pixelWidth - (originalPixelWidth * scale)) / 2);
            }
            else
            {
                scale = widthScale;
                finalY = Convert.ToInt16((pixelHeight - (originalPixelHeight * scale)) / 2);
            }

            int finalPixelWidth = (int)(originalPixelWidth * scale);
            int finalPixelHeight = (int)(originalPixelHeight * scale);
            Bitmap bitmap = new Bitmap(pixelWidth, pixelHeight, PixelFormat.Format24bppRgb);

            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Rectangle(finalX, finalY, finalPixelWidth, finalPixelHeight), new Rectangle(originalX, originalY, originalPixelWidth, originalPixelHeight), GraphicsUnit.Pixel);
            }

            return bitmap;
        }

        private Image FixImageSizeByWidth(Image image, int pixelWidth)
        {
            int originalPixelWidth = image.Width;
            int originalPixelHeight = image.Height;
            int pixelHeight;
            int originalX = 0;
            int originalY = 0;
            int finalX = 0;
            int finalY = 0;
            float scale = 0;
            float aspectRatio = ((float)originalPixelWidth / (float)originalPixelHeight);

            pixelHeight = (int)(pixelWidth / aspectRatio);
            scale = ((float)pixelWidth / (float)originalPixelWidth);
            finalY = Convert.ToInt16((pixelHeight - (originalPixelHeight * scale)) / 2);

            int finalPixelWidth = (int)(originalPixelWidth * scale);
            int finalPixelHeight = (int)(originalPixelHeight * scale);
            Bitmap bitmap = new Bitmap(pixelWidth, pixelHeight, PixelFormat.Format24bppRgb);

            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Rectangle(finalX, finalY, finalPixelWidth, finalPixelHeight), new Rectangle(originalX, originalY, originalPixelWidth, originalPixelHeight), GraphicsUnit.Pixel);
            }

            return bitmap;
        }
    }
}