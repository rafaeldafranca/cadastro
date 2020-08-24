using Microsoft.EntityFrameworkCore;

namespace Cadastro.Core.Mappings
{
    public static partial class Mappings
    {
        public static void Maps(this ModelBuilder modelBuilder)
        {
            modelBuilder.UserMap();
            modelBuilder.PhoneMap();
        }
    }
}


