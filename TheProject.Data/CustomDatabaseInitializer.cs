using System.Data.Entity;

namespace TheProject.Data
{
    internal class CustomDatabaseInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        public CustomDatabaseInitializer()
        {
        }

        protected override void Seed(DataContext context)
        {
            base.Seed(context);
        }

    }
}