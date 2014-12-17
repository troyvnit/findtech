using FindTech.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace FindTech.Services
{
    public interface IContentSectionService : IService<ContentSection>
    {
    }

    public class ContentSectionService : Service<ContentSection>, IContentSectionService
    {
        public ContentSectionService(IRepositoryAsync<ContentSection> contentSectionRepository)
            : base(contentSectionRepository)
        {
        }
    }
}
