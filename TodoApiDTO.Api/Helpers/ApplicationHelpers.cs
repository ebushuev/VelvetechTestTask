using System.IO;

namespace TodoApiDTO.Api.Helpers
{
    public static class ApplicationHelpers
    {
        #region Static

        public static void PrepareCatalogOfFile(string path)
        {
            var catalog = Path.GetDirectoryName(path);

            if (!Directory.Exists(catalog))
            {
                Directory.CreateDirectory(catalog);
            }
        }

        #endregion
    }
}