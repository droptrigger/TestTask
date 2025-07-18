namespace Test.Core.Extensions
{
    public static class ProjectRootPath
    {
        public static string GetProjectRootPath()
        {
            var dir = AppContext.BaseDirectory;
            while (dir != null && !Directory.Exists(Path.Combine(dir, "Test.Core")))
                dir = Directory.GetParent(dir)?.FullName;

            return dir ?? throw new DirectoryNotFoundException("Не удалось найти директорию проекта.");
        }
    }
}
