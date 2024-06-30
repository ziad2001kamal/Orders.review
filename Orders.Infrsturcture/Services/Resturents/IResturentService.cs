using Orders.Core.Dtos;
using Orders.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Resturents
{
    public interface IResturentService
    {
          Task<List<ResturentViewModel>> GetAll(string searchKey);
        Task<int> Create(CreateResturentDto dto);

    }
}
