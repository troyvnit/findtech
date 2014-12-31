using System;
using FindTech.Entities;
using FindTech.Entities.Models;
using FindTech.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace FindTech.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container
                .RegisterType<IDataContextAsync, FindTechContext>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryProvider, RepositoryProvider>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(new object[] {new RepositoryFactories()})
                )
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryAsync<Source>, Repository<Source>>()
                .RegisterType<ISourceService, SourceService>()
                .RegisterType<IRepositoryAsync<Xpath>, Repository<Xpath>>()
                .RegisterType<IXpathService, XpathService>()
                .RegisterType<IRepositoryAsync<Article>, Repository<Article>>()
                .RegisterType<IArticleService, ArticleService>()
                .RegisterType<IRepositoryAsync<ArticleCategory>, Repository<ArticleCategory>>()
                .RegisterType<IArticleCategoryService, ArticleCategoryService>()
                .RegisterType<IRepositoryAsync<ContentSection>, Repository<ContentSection>>()
                .RegisterType<IContentSectionService, ContentSectionService>()
                .RegisterType<IRepositoryAsync<Brand>, Repository<Brand>>()
                .RegisterType<IBrandService, BrandService>()
                .RegisterType<IRepositoryAsync<SpecGroup>, Repository<SpecGroup>>()
                .RegisterType<ISpecGroupService, SpecGroupService>()
                .RegisterType<IRepositoryAsync<Spec>, Repository<Spec>>()
                .RegisterType<ISpecService, SpecService>()
                .RegisterType<IRepositoryAsync<BenchmarkGroup>, Repository<BenchmarkGroup>>()
                .RegisterType<IBenchmarkGroupService, BenchmarkGroupService>()
                .RegisterType<IRepositoryAsync<Image>, Repository<Image>>()
                .RegisterType<IImageService, ImageService>();
        }
    }
}