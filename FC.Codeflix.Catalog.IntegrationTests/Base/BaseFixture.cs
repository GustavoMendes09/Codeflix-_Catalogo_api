using Bogus;
using FC.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.Codeflix.Catalog.IntegrationTests.Base
{
    public class BaseFixture
    {
        public Faker Faker { get; set; }

        protected BaseFixture() =>
            Faker = new Faker("pt_BR");


        public CodeflixCatalogDbContext CreateDbContext(bool preserveData = false)
        {
            var context = new CodeflixCatalogDbContext(
                    new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                    .UseInMemoryDatabase($"integration-tests-db")
                    .Options
                );

            if(preserveData == false)
                context.Database.EnsureDeleted();
            return context;
        }
    }
}
