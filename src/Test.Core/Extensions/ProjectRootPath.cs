namespace Test.Core.Extensions
{
    /// <summary>
    /// Статический класс для полчения root папки проекта
    /// </summary>
    public static class ProjectRootPath
    {
        /// <summary>
        /// Получение root папки проекта
        /// </summary>
        /// <returns>Путь до root папки</returns>
        /// <exception cref="DirectoryNotFoundException">Ошибка, если папка не найдена</exception>
        public static string GetProjectRootPath()
        {
            var dir = AppContext.BaseDirectory;
            while (dir != null && !Directory.Exists(Path.Combine(dir, "Test.Core")))
                dir = Directory.GetParent(dir)?.FullName;

            return dir ?? throw new DirectoryNotFoundException("Не удалось найти директорию проекта.");
        }
    }
}
