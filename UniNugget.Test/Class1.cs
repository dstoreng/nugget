using System.Linq;
using AutoMapper;
using Nuget.Server.AzureStorage;
using Nuget.Server.AzureStorage.Domain.Services;
using Nuget.Server.AzureStorage.Doman.Entities;
using NuGet;
using NuGet.Server;
using NuGet.Server.Infrastructure;
using NUnit.Framework;

namespace UniNugget.Test
{
    [TestFixture]
    public class Class1
    {
        private AzureServerPackageRepository _azureRepo;

        public Class1()
        {
            IPackageLocator packageLocator = new AzurePackageLocator();
            IAzurePackageSerializer packageSerializer = new AzurePackageSerializer();

            _azureRepo = new AzureServerPackageRepository(packageLocator, packageSerializer);
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            Mapper.CreateMap<IPackage, AzurePackage>();
            Mapper.CreateMap<PackageDependencySet, AzurePackageDependencySet>()
                .ForMember(x => x.SeriazlizableDependencies, opt => opt.Ignore())
                .ForMember(x => x.SeriazlizableSupportedFrameworks, opt => opt.Ignore())
                .ForMember(x => x.SeriazlizableTargetFramework, opt => opt.Ignore());
        }

        [Test]
        public void test_get_package()
        {
            var packages = _azureRepo.GetPackages();
            Assert.IsNotNull(packages);
        }

        [Test]
        public void tes_get_packages_in_repo()
        {
            var packages = _azureRepo.GetPackages("nugetazure-unipluss-sql-provider").ToList();
            Assert.IsNotNull(packages);
        }

    }
}
