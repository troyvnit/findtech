﻿using AutoMapper;
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
                Mapper.CreateMap<ArticleBOViewModel, Article>().ForMember(a => a.IsActived, o => o.MapFrom(x => x.IsActived == "on"));
                Mapper.CreateMap<ArticleCategoryBOViewModel, ArticleCategory>();
                Mapper.CreateMap<SourceBOViewModel, Source>();
                Mapper.CreateMap<XpathBOViewModel, Xpath>();
                Mapper.CreateMap<BrandBOViewModel, Brand>();
                Mapper.CreateMap<SpecGroupBOViewModel, SpecGroup>();
                Mapper.CreateMap<SpecBOViewModel, Spec>();
                Mapper.CreateMap<BenchmarkGroupBOViewModel, BenchmarkGroup>();
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
                    .ForMember(a => a.ArticleCategoryColor, o => o.MapFrom(x => x.ArticleCategory.Color))
                    .ForMember(a => a.ArticleCategoryName, o => o.MapFrom(x => x.ArticleCategory.ArticleCategoryName))
                    .ForMember(a => a.SourceName, o => o.MapFrom(x => x.Source.SourceName))
                    .ForMember(a => a.SourceLogo, o => o.MapFrom(x => x.Source.Logo));
                Mapper.CreateMap<ContentSection, ContentSectionBOViewModel>();
                Mapper.CreateMap<ArticleCategory, ArticleCategoryBOViewModel>();
                Mapper.CreateMap<Source, SourceBOViewModel>();
                Mapper.CreateMap<Xpath, XpathBOViewModel>();
                Mapper.CreateMap<Brand, BrandBOViewModel>();
                Mapper.CreateMap<SpecGroup, SpecGroupBOViewModel>();
                Mapper.CreateMap<Spec, SpecBOViewModel>();
                Mapper.CreateMap<BenchmarkGroup, BenchmarkGroupBOViewModel>()
                    .ForMember(a => a.Parent, o => o.ResolveUsing(x => Mapper.Map<BenchmarkGroupBOViewModel>(x.Parent) ?? new BenchmarkGroupBOViewModel { BenchmarkGroupId = 0, BenchmarkGroupName = "Root" }));
            }
        }
    }
}