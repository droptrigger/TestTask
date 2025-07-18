using Test.Classes;
using Test.Core.Extensions;
using Test.Core.Extensions.Parsers;
using Test.Core.Repositories.Interfaces;

namespace Test.Core.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий для работы с полями
    /// </summary>
    public class FieldRepository : IFieldRepository
    {
        private readonly FileKmlParser _parser;
        private readonly string _centroidsPath;
        private readonly string _fieldsPath;

        public FieldRepository()
        {
            _parser = new FileKmlParser();

            string rootPath = ProjectRootPath.GetProjectRootPath();
            _centroidsPath = Path.Combine(rootPath, "Points", "centroids.kml");
            _fieldsPath = Path.Combine(rootPath, "Points", "fields.kml");
        }

        public async Task<List<Field>?> GetAllAsync() 
            => await _parser.Parse(_centroidsPath, _fieldsPath);

        public async Task<Field?> GetByIdAsync(int id)
        {
            List<Field>? fields = await GetAllAsync();
            return fields?.FirstOrDefault(field => field.Id == id);
        }
    }
}
