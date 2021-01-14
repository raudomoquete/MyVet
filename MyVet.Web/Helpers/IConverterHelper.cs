using MyVet.Web.Data.Entities;
using MyVet.Web.Models;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Pet> ToPetAsync(PetViewModel model, string path, bool isNew);

        PetViewModel ToPetViewModel(Pet pet);

        //metodo que convierte de HistoryViewModel a History
        Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew);

        //metodo que convierte de History a HistoryViewModel
        HistoryViewModel ToHistoryViewModel(History history);
    }
}