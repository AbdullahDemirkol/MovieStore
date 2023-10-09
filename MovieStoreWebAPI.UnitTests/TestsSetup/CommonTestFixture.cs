using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieStoreWebAPI.DataAccess.Abstract;
using MovieStoreWebAPI.DataAccess.Concrete;
using MovieStoreWebAPI.UnitTests.Extensions;
using MovieStoreWebAPI.Utilities.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStoreWebAPI.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public MovieStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }
        public CommonTestFixture()
        {
            var inMemorySettings= new Dictionary<string, string>
            {
                {"Token:Issuer", "www.MovieStore.com"},
                {"Token:Audience", "www.MovieStore.com"},
                {"Token:SecurityKey", "This is my private secret key that I use for authentication in the moviestore"},
            };
            
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDb").Options;
            Context=new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.Initialize();

            Mapper = new MapperConfiguration(configure: cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
        }
    }
}
