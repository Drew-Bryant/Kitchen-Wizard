using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IHistoryHelper
    {
        public Task Add(Recipe recipe);
        public Task Remove(Recipe recipe);
        public Task<List<Recipe>> LoadHistory();
    }
}
