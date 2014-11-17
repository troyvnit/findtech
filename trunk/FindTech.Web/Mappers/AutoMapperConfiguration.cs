using AutoMapper;
using FindTech.Entities.Models;
using FindTech.Web.Areas.BO.Models;
using FindTech.Web.Models;

namespace FindTech.Web.Mappers
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }

        public class ViewModelToDomainMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "ViewModelToDomainMappings"; }
            }

            protected override void Configure()
            {
                Mapper.CreateMap<ArticleBOViewModel, Article>();
            }
        }

        public class DomainToViewModelMappingProfile : Profile
        {
            public override string ProfileName
            {
                get { return "DomainToViewModelMappings"; }
            }

            protected override void Configure()
            {
                Mapper.CreateMap<Article, ArticleBOViewModel>();
                Mapper.CreateMap<Article, ArticleViewModel>()
                    .ForMember(a => a.SourceName, o => o.MapFrom(x => x.Source.SourceName))
                    .ForMember(a => a.SourceLogo, o => o.MapFrom(x => x.Source.Logo));
            }
        }
    }
}