using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.dbDTO;

namespace DAL.dbContext
{
    public interface IDataService
    {

        Post FindPost(int id);


    }
}
