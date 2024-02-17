using Microsoft.EntityFrameworkCore;

namespace UNAM.PrimatesApi.Entidades
{
    public class ConservacionDb : DbContext
    {
        public ConservacionDb() { }

        public ConservacionDb(DbContextOptions<ConservacionDb> options) { }
    }
}
