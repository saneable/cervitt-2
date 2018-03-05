using AutoMapper;
using AutoMapper.QueryableExtensions;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class SearchController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/SearchCompaniesAndProducts")]
        [Authorize]
        public IEnumerable<CompanyAndProductSearchDTO> SearchCompaniesAndProducts(string query, int maxRecords)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Company> companies = db.Companies.Where(company => company.DisplayName.Contains(query))
                                                                                                         .Include(company => company.Products)
                                                                                                         .OrderBy(company => company.DisplayName)
                                                                                                         .Take(maxRecords);
            IQueryable<Product> products = db.Products.Where(product => product.Name.Contains(query))
                                                                                                         .Include(product => product.Company.Followers)
                                                                                                         .OrderBy(product => product.Name)
                                                                                                         .Take(maxRecords);
            List<CompanyAndProductSearchDTO> companiesAndProducts = new List<CompanyAndProductSearchDTO>(maxRecords * 2);

            foreach(Company company in companies)
            {
                companiesAndProducts.Add(new CompanyAndProductSearchDTO
                {
                    CompanyID = company.ID,
                    Name = company.DisplayName,
                    ProductCount = company.Products.Count,
                    Type = CompanyAndProductSearchType.Company
                });
            }

            foreach (Product product in products)
            {
                companiesAndProducts.Add(new CompanyAndProductSearchDTO
                {
                    ProductID = product.ID,
                    CompanyID = product.CompanyID.HasValue ? product.CompanyID.Value : 0,
                    Name = product.Name,
                    ConnectionCount = product.CompanyID.HasValue ?  product.Company.Followers.Count : 0,
                    Type = CompanyAndProductSearchType.Product
                });
            }

            return companiesAndProducts.OrderBy(companyAndProduct => companyAndProduct.Name).Take(maxRecords);
        }

        [HttpGet]
        [Route("api/SearchProducts")]
        [Authorize]
        public IQueryable<ProductSearchDTO> SearchProducts(string query, int maxRecords)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => product.Name.Contains(query))
                                                                                            .Include(product => product.Company.Followers)
                                                                                            .OrderBy(product => product.Name)
                                                                                            .Take(maxRecords);

            return products.ProjectTo<ProductSearchDTO>();
        }

        [HttpGet]
        [Route("api/SearchCompanies")]
        [Authorize]
        public IQueryable<CompanySearchDTO> SearchCompanies(string query, int maxRecords)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Company> companies = db.Companies.Where(company => company.DisplayName.Contains(query))
                                                                                                         .Include(company => company.Followers)
                                                                                                         .OrderBy(company => company.DisplayName)
                                                                                                         .Take(maxRecords);

            return companies.ProjectTo<CompanySearchDTO>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}