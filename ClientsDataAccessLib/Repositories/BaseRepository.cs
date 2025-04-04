using ClientsAppUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ClientsDataAccessLib.Repositories
{
    /// <summary>
    /// Protected base class for all repositories.
    /// </summary>
    public abstract class BaseRepository
    {
       protected readonly ApplicationDBContext? _dbContext;

        protected BaseRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        protected async Task SaveChangesAsync()
        {
            if (_dbContext is not null)
                await _dbContext.SaveChangesAsync();           
        }
    }
}
